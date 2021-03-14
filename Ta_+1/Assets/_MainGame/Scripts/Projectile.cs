using System.Collections;
using System.Collections.Generic;
using Tut.Scripts.Towers;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Projectile : MonoBehaviour
{
    private Monster _target;
    private Tower _parent;

    private TowerType _towerType;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    public void Initialize(Tower parent)
    {
        _target = parent.Target;
        _parent = parent;
        _towerType = parent.TowerType;
    }

    private void MoveToTarget()
    {
        if (_target != null && _target.IsActive)
        {
            var targetPosition = _target.transform.position;
            var projectilePosition = transform.position;

            Vector2 dir = targetPosition - projectilePosition;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.position = Vector3.MoveTowards(projectilePosition, targetPosition,
                Time.deltaTime * _parent.ProjectileSpeed);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (!_target.IsActive)
        {
            GameManager.Instance.ObjectPool.ReleaseObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            if (_target.gameObject == other.gameObject)
            {
                _target.TakeDamage(_parent.Damage, _towerType);
                ApplyDebuff();

                GameManager.Instance.ObjectPool.ReleaseObject(gameObject);
            }
        }
    }

    public void ApplyDebuff()
    {
        if (_target.TowerType != _towerType)
        {
            float roll = UnityEngine.Random.Range(0, 100);

            if (roll <= _parent.ChanceForDebuff)
                _target.AddDebuff(_parent.GetDebuff());
        }

    }
}
