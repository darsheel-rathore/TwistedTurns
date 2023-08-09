using System;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;
    [SerializeField] AudioClip collectableSFX;
    [SerializeField] float volumeSFX = 0.01f;

    private GameObject player;
    private int collectablePoints = 20;
    private MeshRenderer meshRenderer;
    private Vector3 newPos;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        // Turn off mesh renderer
        meshRenderer.enabled = false;

        // Show particle effects
        Instantiate(particleEffect, transform.position, particleEffect.transform.rotation);

        // Play SFX
        //AudioSource.PlayClipAtPoint(collectableSFX, Camera.main.transform.position, volumeSFX);
        AudioManager.instance.GetAudioSource().PlayOneShot(collectableSFX, volumeSFX);

        // Add value to the score
        GameManager.instance.score += collectablePoints;

        // Generate New position
        GenerateNewPos();

        // Change its position to new position
        transform.position = newPos;

        // Turn on mesh renderer
        meshRenderer.enabled = true;
    }

    public Vector3 GenerateNewPos()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");

        CreateNewVector(platforms);

        // Check if newPos is equal to any of the collectables
        foreach (var collectableGameObject in collectables)
        {
            while (collectableGameObject.transform.position == newPos)
            {
                // Again generate the position
                CreateNewVector(platforms);
            }

            while (PlayerIsNear())
            {
                CreateNewVector(platforms);
            }
        }
        return newPos;
    }

    private void GenerateNewPosWithAI()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");

        bool validPosition = false;

        while (!validPosition)
        {
            CreateNewVector(platforms);

            bool collectableOverlap = false;
            foreach (var collectableGameObject in collectables)
            {
                if (collectableGameObject.transform.position == newPos)
                {
                    collectableOverlap = true;
                    break;
                }
            }

            bool playerNearby = PlayerIsNear();

            validPosition = !collectableOverlap && !playerNearby;
        }
    }

    private void CreateNewVector(GameObject[] platforms)
    {
        do
        {
            int rand = UnityEngine.Random.Range(0, platforms.Length);
            newPos = platforms[rand].transform.position +
                        new Vector3(0, 1, 0);
        }
        // Check if collectable not taking ref position from the falling platform
        while (newPos.y < 0);
    }

    private bool PlayerIsNear()
    {
        float distance = Vector3.Distance(newPos, player.transform.position);
        if (distance < 15)
        {
            return true;
        }
        return false;
    }
}
