using System.Collections;
using System.Collections.Generic;
using Tut.Scripts.Towers;
using UnityEngine;

public class FireTower : Tower
{
    [SerializeField] private float tickTime;
    public float TickTime => tickTime;

    [SerializeField] private float tickDamage;
    public float TickDamage => tickDamage;

    private void Start()
    {
        ElementType = ElementType.Fire;
    }

    public override Debuff GetDebuff()
    {
        return new FireDebuff(tickDamage, tickTime, DebuffDuration, Target);
    }
}
