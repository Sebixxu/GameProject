using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

    private Player _player;
    private SpriteRenderer _parentSpriteRenderer;
    private bool _shouldNotProcess;
    // Start is called before the first frame update
    void Awake()
    {
        _player = parent.GetComponent<Player>();
        _parentSpriteRenderer = parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tile"))
        {
            var yTileValue = other.gameObject.GetComponent<TileScript>().GridPosition.Y;
            _parentSpriteRenderer.sortingOrder = ++yTileValue;

            _shouldNotProcess = false;
        }

        if (other.CompareTag("Water"))
        {
            _shouldNotProcess = true;
            _player.Movement.MovementSpeed = _player.MovementInWater;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water") && !_shouldNotProcess)
        {
            _player.Movement.MovementSpeed = _player.Statistics.movementSpeed;
        }
    }
}
