using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    private Vector2 _movement;

    private Animator animator;

    private static readonly int IsInMovement = Animator.StringToHash("IsInMovement");

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            animator.SetBool(IsInMovement, true);
        }
        else
        {
            _movement.x = 0;
            _movement.y = 0;

            animator.SetBool(IsInMovement, false);
        }

    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + movementSpeed * Time.fixedDeltaTime * _movement);
    }

    private void ProcessInput()
    {

    }
}
