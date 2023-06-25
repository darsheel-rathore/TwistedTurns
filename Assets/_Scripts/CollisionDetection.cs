using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if player has touched the collider
        if(other.gameObject.tag == "Player")
        {
            /* 
             * Invoke the player fallen method,
             * this will start coroutine to reload level 
             * after some given time.
             */
            GameManager.instance.PlayerFallen();
        }
    }
}
