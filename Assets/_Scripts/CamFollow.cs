using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform target;

    private Vector3 distance;
    private float smoothness = 2f;

    private void Start()
    {
        distance = target.position - transform.position;
    }

    private void Update()
    {
        // Check if player has not fallen
        if (target.position.y < 0)
            return;

        Follow();
    }

    private void Follow()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.position - distance;

        transform.position =  Vector3.Lerp(currentPos, targetPos, smoothness * Time.deltaTime);
    }
}
