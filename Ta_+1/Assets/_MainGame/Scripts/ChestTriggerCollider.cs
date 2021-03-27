using System.Collections;
using System.Collections.Generic;
using Tut.Scripts;
using UnityEngine;

public class ChestTriggerCollider : MonoBehaviour
{
    [SerializeField]
    private float timeoutForDestroy;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<Player>();

        if (player != null)
        {
            Debug.Log("OPENING LECIMY");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            StartCoroutine(WaitToDestroy());
        }
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(timeoutForDestroy);

        Destroy(gameObject);
    }
}
