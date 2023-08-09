using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject collectablePrefab;

    private GameObject[] platforms;
    private int noOfCollectableInScene = 3;
    private int[] randomPositions;

    private void Start()
    {
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        randomPositions = new int[noOfCollectableInScene];

        // Spawn the collectables
        SpawnCollectable();
    }

    private void SpawnCollectable()
    {
        for (int i = 0; i < noOfCollectableInScene; i++)
        {
            int rand = Random.Range(7, platforms.Length);
            randomPositions[i] = rand;

            if (i > 0)
            {
                while (randomPositions[i] == randomPositions[i - 1])
                {
                    rand = Random.Range(7, platforms.Length);
                    randomPositions[i] = rand;
                }
            }
            Vector3 newPos = platforms[randomPositions[i]].transform.position +
                collectablePrefab.transform.position;

            Instantiate(collectablePrefab, newPos, collectablePrefab.transform.rotation);
        }
    }
}
