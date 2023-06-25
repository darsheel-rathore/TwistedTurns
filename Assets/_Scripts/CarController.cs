using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private const float _ANGLE_OF_ROTATION = 90f;
    private float delta;
    private bool movingLeft = true;

    public bool firstTap = true;
    public bool isPlayerFallen;

    void Update()
    {
        // Check if Game has started
        if (!GameManager.instance.gameStarted)
            return;

        // Start movement
        VehicleMovement();

        /* 
         * After game has started
         * this will check for the 
         * first input and ignore that
         * because we wont want player to 
         * change direction as soon as the game starts
         */
        if (firstTap)
        {
            firstTap = false;
            return;
        }

        // Direction change
        ChangeDirectionFromMouse();
    }

    private void ChangeDirectionFromKeyBoard()
    {
        delta = Input.GetAxisRaw("Horizontal");

        if (delta != 0)
        {
            if (delta == 1)
            {
                // Turn the car right
                float angleOfRotation = delta * _ANGLE_OF_ROTATION;
                Quaternion newRotation = Quaternion.Euler(0f, angleOfRotation, 0f);

                transform.rotation = newRotation;
            }
            else
            {
                // Make car rotation to initial value
                transform.rotation = Quaternion.identity;
            }
        }
    }

    private void VehicleMovement()
    {
        float vehicleSpeed = moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.forward * vehicleSpeed;

        transform.position += newPosition;
    }

    private void ChangeDirectionFromMouse()
    {
        // Check if mouse is pressed down
        if (!Input.GetMouseButtonDown(0))
            return;

        if (movingLeft)
        {
            movingLeft = false;

            Quaternion newRotation = Quaternion.Euler(0f, _ANGLE_OF_ROTATION, 0f);
            transform.rotation = newRotation;
        }
        else
        {
            movingLeft = true;

            Quaternion newRotation = Quaternion.identity;
            transform.rotation = newRotation;
        }
    }
}
