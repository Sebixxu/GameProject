using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff : MonoBehaviour
{
    protected Monster _target;
    private float _duration;
    private float _elapsed;

    protected Debuff(Monster target, float duration)
    {
        _target = target;
        _duration = duration;
    }

    public virtual void Update()
    {
        _elapsed += Time.deltaTime;

        if(_elapsed >= _duration)
            Remove();
    }

    public virtual void Remove()
    {
        if (_target != null)
        {
            _target.RemoveDebuff(this);
        }
    }
}
