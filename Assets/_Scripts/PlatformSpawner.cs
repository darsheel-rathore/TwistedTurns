using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Transform lastPlatform;
    [SerializeField] private Transform parentPlatform;

    public Vector3 newPos;
    public static PlatformSpawner instance;

    private uint noOfPlatformInScene = 30;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = lastPlatform.position;

        // Instantiate 30 prefabs
        for (int i = 0; i < noOfPlatformInScene; i++)
        {
            GeneratePosition(); // Generate a new position for the next platform
            InstantiatePlatform(); // Instantiate the platform prefab at the new position
        }
    }

    // Generates a new position for the next platform to be spawned
    public Vector3 GeneratePosition()
    {
        int value = Random.Range(0, 2);

        newPos = lastPosition;

        if (value > 0)
        {
            newPos.x += 2f; // Increase the x-axis position
        }
        else
        {
            newPos.z += 2f; // Increase the z-axis position
        }

        return newPos; // Return the new position
    }

    // Updates the last position with the current new position
    public void UpdateLastPosition() => lastPosition = newPos;

    // Instantiates a platform prefab at the new position and updates the last position
    private void InstantiatePlatform()
    {
        Instantiate(platformPrefab, newPos, Quaternion.identity, parentPlatform.transform);

        UpdateLastPosition(); // Update the last position for the next platform
    }
}
