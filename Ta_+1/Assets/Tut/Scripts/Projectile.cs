using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Monster _target;
    private Tower _parent;

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
}
