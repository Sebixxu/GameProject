using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Point GridPosition { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsAlive => health.CurrentValue > 0;

    [SerializeField]
    private Stat health;

    [SerializeField]
    private float maxHealthValue;

    public void Awake()
    {
        health.Initialize();

        health.CurrentValue = health.MaxVal = maxHealthValue;
    }
}
