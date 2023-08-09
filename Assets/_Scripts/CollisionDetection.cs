using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if player has touched the collider
        if (other.gameObject.tag == "Player")
        {
            /* 
             * Invoke the player fallen method,
             * this will start coroutine to reload level 
             * after some given time.
             */
            GameManager.instance.PlayerFallen();
        }

        // Check if gems has touched the collider
        if (other.gameObject.tag == "Collectable")
        {
            // Generate New Pos
            Vector3 newPos = other.GetComponent<Collectables>().GenerateNewPos();
            // Change its position to new position
            other.gameObject.transform.position = newPos;
        }
    }
}
