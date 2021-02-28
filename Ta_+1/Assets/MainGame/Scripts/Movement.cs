using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    private Vector2 _movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + movementSpeed * Time.fixedDeltaTime * _movement);
    }

    private void ProcessInput()
    {

    }
}
