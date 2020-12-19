using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 0;

    private float xMax;
    private float yMin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(cameraSpeed * Time.deltaTime * Vector3.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(cameraSpeed * Time.deltaTime * Vector3.left);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(cameraSpeed * Time.deltaTime * Vector3.down);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(cameraSpeed * Time.deltaTime * Vector3.right);
        }

        var position = transform.position;
        position = new Vector3(Mathf.Clamp(position.x, 0, xMax), Mathf.Clamp(position.y, yMin, 0), -10);
        transform.position = position;
    }

    public void SetLimits(Vector3 lastTile)
    {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)); //Right bottom corner of screen

        xMax = lastTile.x - wp.x;
        yMin = lastTile.y - wp.y;
    }
}
