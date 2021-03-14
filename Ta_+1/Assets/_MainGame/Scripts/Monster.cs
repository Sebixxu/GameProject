using System.Collections.Generic;
using Tut.Scripts.Towers;
using UnityEngine;
using UnityEngine.Serialization;

public class Monster : MonoBehaviour
{
    public Point GridPosition { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsAlive => health.CurrentValue > 0;

    [SerializeField]
    private float movementSpeed;

    [FormerlySerializedAs("elementType")] [SerializeField]
    private TowerType towerType;

    public TowerType TowerType => towerType;

    [SerializeField]
    private Stat health;

    private SpriteRenderer _spriteRenderer;
    private Stack<Node> path;
    private Vector3 destination;

    private List<Debuff> _debuffs = new List<Debuff>();
    private List<Debuff> _debuffsToRemove = new List<Debuff>();
    private List<Debuff> _newDebuffs = new List<Debuff>();

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        health.Initialize();
    }

    private void Update()
    {
        HandleDebuffs();
        Move();
    }

    public void Spawn(float monsterHealth)
    {
        _debuffs.Clear();

        health.Bar.Reset();

        health.CurrentValue = health.MaxVal = monsterHealth;
        transform.position = LevelManager.Instance.BluePortal.transform.position;

        IsActive = true;

        SetPath(LevelManager.Instance.Path);
    }

    private void Move()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);

        if (transform.position == destination)
        {
            if (path != null && path.Count > 0)
            {
                GridPosition = path.Peek().GridPosition;
                destination = path.Pop().WorldPosition;
            }
        }
    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            path = newPath;

            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }

    private void Release()
    {
        _debuffs.Clear();

        GameManager.Instance.ObjectPool.ReleaseObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RedPortal"))
        {
            Debug.Log("Collided");

            gameObject.SetActive(false);
            IsActive = false;
        }

        if (other.CompareTag("Tile"))
        {
            _spriteRenderer.sortingOrder = other.GetComponent<TileScript>().GridPosition.Y;
        }
    }

    public void TakeDamage(float damage, TowerType damageSourceTowerType)
    {
        if(IsActive)
        {
            if (damageSourceTowerType == towerType)
                damage /= 2;

            health.CurrentValue -= damage;

            if (health.CurrentValue <= 0)
            {
                //GameManager.Instance.Currency += 2;

                IsActive = false;
                //GetComponent<SpriteRenderer>().sortingOrder--;
                GameManager.Instance.ObjectPool.ReleaseObject(gameObject);
                
                Debug.Log("Dead body");
            }
        }
    }

    public void AddDebuff(Debuff debuff)
    {
        if(!_debuffs.Exists( x=> x.GetType() == debuff.GetType()))
            _newDebuffs.Add(debuff);
    }

    public void RemoveDebuff(Debuff debuff)
    {
        _debuffsToRemove.Add(debuff);
    }

    private void HandleDebuffs()
    {
        if (_newDebuffs.Count > 0)
        {
            _debuffs.AddRange(_newDebuffs);

            _newDebuffs.Clear();
        }

        foreach (var debuffToRemove in _debuffsToRemove)
        {
            _debuffs.Remove(debuffToRemove);
        }

        _debuffsToRemove.Clear();

        foreach (var debuff in _debuffs)
        {
            debuff.Update();
        }
    }
}
