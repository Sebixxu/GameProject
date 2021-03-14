using System.Collections;
using System.Collections.Generic;
using Tut.Scripts.Towers;
using UnityEngine;

public class FireDebuff : Debuff
{
    private float _tickTime;
    private float _timeSinceTick;
    private float _tickDamage;

    public FireDebuff(float tickDamage, float tickTime, float duration, Monster target) : base(target, duration)
    {
        _tickDamage = tickDamage;
        _tickTime = tickTime;
    }

    public override void Update()
    {
        if (_target != null)
        {
            _timeSinceTick += Time.deltaTime;

            if (_timeSinceTick >= _tickTime)
            {
                _timeSinceTick = 0;

                _target.TakeDamage(_tickDamage, TowerType.Fire);
            }
        }


        base.Update();
    }

}
