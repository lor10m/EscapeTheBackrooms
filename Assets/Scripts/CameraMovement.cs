using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform bodyTransform; // Reference to the character's body transform
    public string horizontalMovement;
    public string verticalMovement;

    private float rotationSpeed = 75.0f;
    //private float movementSpeed = 5.0f;

    void Update()
    {
        float horizontalInput = Input.GetAxis(horizontalMovement);
        float verticalInput = Input.GetAxis(verticalMovement);

        // Rotate the body left or right
        bodyTransform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // Move the camera vertically
        float rotationX = transform.localEulerAngles.x - verticalInput * rotationSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);

        // Move the body forward or backward based on horizontal input
        //Vector3 movement = bodyTransform.forward * verticalInput * movementSpeed * Time.deltaTime;
        //bodyTransform.position += movement;
    }
}
