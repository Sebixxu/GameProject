﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _rangeSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rangeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        if (!_spriteRenderer.enabled)
            return;

        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = new Vector3(position.x, position.y, 0);

        transform.position = position;
    }

    public void ActivateHover(Sprite sprite)
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.sprite = sprite;

        _rangeSpriteRenderer.enabled = true;
    }

    public void DeactivateHover()
    {
        GameManager.Instance.ClickedTowerButton = null;

        _spriteRenderer.sprite = null;
        _spriteRenderer.enabled = false;

        _rangeSpriteRenderer.enabled = false;
    }
}
