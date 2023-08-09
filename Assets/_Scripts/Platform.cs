using System.Collections;
using UnityEngine;

public class Plaltform : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 newPosition;
    private PlatformSpawner pSpawner;
    private float platformFallTime = 3f;

    private void Start()
    {
        // Init
        pSpawner = FindAnyObjectByType<PlatformSpawner>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag != "Player")
            return;

        StartCoroutine(RepositionPlatform());
    }

    /*
     * This coroutine will reposition the platform
     * by changing its transform value to the last position 
     * which has been generated by the platform spawner script.
     */
    IEnumerator RepositionPlatform()
    {
        // this will make platform fall
        rb.isKinematic = false;

        // Wait for the platform to fall off the screen
        yield return new WaitForSeconds(platformFallTime);

        // Set platform back to isKinematic
        rb.isKinematic = true;


        // New position for this platform
        newPosition = pSpawner.GeneratePosition();

        // Set this newPosition to current GameObject
        this.transform.position = newPosition;
        this.transform.rotation = Quaternion.identity;

        // Update last position
        pSpawner.UpdateLastPosition();
    }

}