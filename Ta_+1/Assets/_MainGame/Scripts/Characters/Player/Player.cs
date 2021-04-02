using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public ExtendedStatistics Statistics => statistics;
    public float MovementInWater => _movementInWater;

    public Movement Movement
    {
        get { return _movement; }
        set { _movement = value; }
    }

    public static Player Instance;
    public float PlayerRange
    {
        get { return playerRange; }
        set { playerRange = value; }
    }

    [SerializeField]
    private float playerRange; //Defines transform.scale in each direction 

    private SpriteRenderer _spriteRenderer;
    private Movement _movement;
    private Inventory _inventory;
    private float _movementInWater;
    private bool _isInWater;
    private new void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _inventory = new Inventory();

        #region Singleton

        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of OreStorage!");
            return;
        }

        Instance = this;
        #endregion
    }

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _movement.MovementSpeed = statistics.movementSpeed;
        _movementInWater = statistics.movementSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivatePlayerRange()
    {
        var rangeGameObject = transform.Find("Range");
        rangeGameObject.GetComponent<SpriteRenderer>().enabled = true;
        rangeGameObject.transform.localScale = new Vector3(playerRange * 2, playerRange * 2, 1);
    }
}
