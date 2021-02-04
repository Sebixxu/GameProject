using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Point GridPosition { get; set; }

    [SerializeField]
    private float movementSpeed;

    private Stack<Node> path;
    private Vector3 destination;

    private void Update()
    {
        Move();
    }

    public void Spawn()
    {
        transform.position = LevelManager.Instance.BluePortal.transform.position;

        SetPath(LevelManager.Instance.Path);
    }

    private void Move()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);

        if (transform.position == destination)
        {
            if (path != null && path.Count > 0)
            {
                GridPosition = path.Peek().GridPosition;
                destination = path.Pop().WorldPosition;
            }
        }
    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            path = newPath;

            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }
}
