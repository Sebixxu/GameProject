using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Tut.Scripts.Towers;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float damage = 2f;
    public float Damage => damage;

    [SerializeField]
    private float projectileSpeed = 3f;
    public float ProjectileSpeed => projectileSpeed;

    [SerializeField]
    private float attackCooldown = 0.5f;

    [SerializeField]
    private float chanceForDebuff;
    public float ChanceForDebuff => chanceForDebuff;

    [SerializeField]
    private float debuffDuration;
    public float DebuffDuration
    {
        get => debuffDuration;
        set => debuffDuration = value;
    }

    [SerializeField]
    private int cost;

    public int Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    [SerializeField]
    private float range = 2.5f;

    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    public TowerType TowerType { get; protected set; }
    public Monster Target => _target;
    public Sprite Icon => _mainSpriteRenderer.sprite;

    private Queue<Monster> _monsterQueue = new Queue<Monster>();
    private SpriteRenderer _mainSpriteRenderer;
    private SpriteRenderer _rangeSpriteRenderer;
    private Monster _target;

    private float attackTimer;
    private bool canAttack = true;

    private void Awake()
    {
        var rangeChild = transform.GetChild(0);

        _mainSpriteRenderer = transform.GetComponent<SpriteRenderer>();

        rangeChild.transform.localScale = new Vector3(range, range, 1);
        _rangeSpriteRenderer = rangeChild.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        //Debug.Log(_target);
    }

    public void Select()
    {
        _rangeSpriteRenderer.enabled = !_rangeSpriteRenderer.enabled;
    }

    private void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }

        if (_target == null && _monsterQueue.Any() && _monsterQueue.Peek().IsActive)
        {
            _target = _monsterQueue.Dequeue();
        }

        if (_target != null && _target.IsActive)
        {
            if (canAttack)
                Shoot();

            canAttack = false;
        }

        if (_target != null && !_target.IsAlive || _target != null && !_target.IsActive)
        {
            _target = null;
        }
    }

    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.ObjectPool.GetObject(projectileType).GetComponent<Projectile>();

        projectile.transform.position = transform.position;
        projectile.Initialize(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            _monsterQueue.Enqueue(other.GetComponent<Monster>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            _target = null;
        }
    }

    public abstract Debuff GetDebuff();
}
