using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private SpriteRenderer _mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        _mySpriteRenderer.enabled = !_mySpriteRenderer.enabled;
    }
}
