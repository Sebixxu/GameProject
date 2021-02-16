using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] 
    private float projectileSpeed = 3f;

    public float ProjectileSpeed => projectileSpeed;

    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float attackCooldown = 0.5f;

    private Monster _target;
    public Monster Target => _target;

    private Queue<Monster> _monsterQueue = new Queue<Monster>();
    private SpriteRenderer _mySpriteRenderer;
    
    private float attackTimer;
    private bool canAttack = true;


    // Start is called before the first frame update
    void Start()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Debug.Log(_target);
    }

    public void Select()
    {
        _mySpriteRenderer.enabled = !_mySpriteRenderer.enabled;
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

        if (_target == null && _monsterQueue.Any())
        {
            _target = _monsterQueue.Dequeue();
        }

        if (_target != null && _target.IsActive)
        {
            if(canAttack)
                Shoot();

            canAttack = false;
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
}
