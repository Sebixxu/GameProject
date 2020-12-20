using System.Collections;
using System.Collections.Generic;
using Tut.Scripts;
using UnityEngine;

public class ChestTriggerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        IPlayer player = collider.GetComponent<IPlayer>();

        if (player != null)
        {
            Debug.Log("OPENING LECIMY");
        }
    }
}
