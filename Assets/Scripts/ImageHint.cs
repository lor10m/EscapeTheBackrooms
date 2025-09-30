using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ImageHint : MonoBehaviour
{
    public GameObject cubeWithImage;
    public Camera playerCamera;
    public Camera zoomedCamera; // Reference to the new camera for the zoomed-in view
    public Text promptText;
    public bool displayOnLeftSide = true;
    public PlayerMovement playerMovement;
    public CameraMovement playerCameraMovement;
    public KeyCode fullscreenKey;
    public string horizontal;
    public string vertical;
    public int maxDistance;

    private bool isLookingAtCube = false;
    private bool isFullscreen = false;

    private Vector3 originalScale;
    private Vector3 initialCameraPosition;
    private Vector3 zoomedCameraStartPosition;
    private Vector2 originalPromptPosition;
    private Vector2 originalPromptAnchorMin;
    private Vector2 originalPromptAnchorMax;
    private Vector2 originalPromptPivot;

    private Vector3 lastValidMoveDirection = Vector3.zero; // Stores the last valid movement direction

    


    private void Start()
    {
        promptText.enabled = false; // Disable the prompt text initially

        originalScale = cubeWithImage.transform.localScale;
        initialCameraPosition = playerCamera.transform.position; // Store the initial position of the player's camera
        zoomedCameraStartPosition = zoomedCamera.transform.position; // Store the initial position of the zoomed-in camera
        originalPromptPosition = promptText.rectTransform.anchoredPosition;
        originalPromptAnchorMin = promptText.rectTransform.anchorMin;
        originalPromptAnchorMax = promptText.rectTransform.anchorMax;
        originalPromptPivot = promptText.rectTransform.pivot;
    }

    private void Update()
    {
        // Cast a ray from the player's camera position and direction
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit, maxDistance)) // Replace maxDistance with your desired maximum distance
        {
            if (hit.collider.gameObject == cubeWithImage)
            {
                isLookingAtCube = true;
                HandleZoomedCameraMovement();



                // Check for key press to activate fullscreen
                if (Input.GetKeyDown(fullscreenKey))
                {
                    ToggleFullscreen();
                }
            }
            else
            {
                isLookingAtCube = false;
            }
        }
        else
        {
            isLookingAtCube = false;

          
        }
        if(isFullscreen && isLookingAtCube == false)
        {
            ToggleFullscreen();
        }

        // Enable/disable the prompt text based on raycast result and distance
        //promptText.enabled = isLookingAtCube && playerCamera.isActiveAndEnabled;
        promptText.enabled = isLookingAtCube && Vector3.Distance(playerCamera.transform.position, hit.point) <= maxDistance && playerCamera.isActiveAndEnabled;
    }

    private void HandleZoomedCameraMovement()
    {
        float moveSpeed = 3f;

        // Get the input values for horizontal and vertical axes
        float horizontalInput = Input.GetAxis(horizontal);
        float verticalInput = Input.GetAxis(vertical);

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f);

        // Normalize the move direction to ensure consistent movement speed
        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();
        }

        Ray ray = new Ray(zoomedCamera.transform.position, zoomedCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == cubeWithImage)
            {
                // Store the current move direction as the last valid move direction
                lastValidMoveDirection = moveDirection;

                // Apply movement to the zoomed-in camera
                zoomedCamera.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            }
            else
            {
                // If the camera is not looking at the object, restrict movement to the opposite direction
                zoomedCamera.transform.Translate(-lastValidMoveDirection * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void ToggleFullscreen()
    {
        isFullscreen = !isFullscreen;
        zoomedCamera.gameObject.SetActive(isFullscreen);

        if (isFullscreen)
        {
            
            zoomedCamera.transform.position = zoomedCameraStartPosition; // Reset the position of the zoomed-in camera
            zoomedCamera.transform.LookAt(cubeWithImage.transform); // Point the zoomed camera towards the cube

            playerMovement.enabled = false; // Disable the PlayerMovement script
            playerCameraMovement.enabled = false; // Disable the CameraMovement script

            // Change the prompt text to "to exit" state
            promptText.text = "Press " + fullscreenKey.ToString() + " to exit";

            // Position the prompt text in the corner
            if (displayOnLeftSide)
            {
                promptText.rectTransform.anchorMin = new Vector2(0f, 0f);
                promptText.rectTransform.anchorMax = new Vector2(0f, 0f);
                promptText.rectTransform.pivot = new Vector2(0f, 0f);
                promptText.rectTransform.anchoredPosition = new Vector2(10f, 10f);
            }
            else
            {
                promptText.rectTransform.anchorMin = new Vector2(0f, 0f);
                promptText.rectTransform.anchorMax = new Vector2(0f, 0f);
                promptText.rectTransform.pivot = new Vector2(0f, 0f);
                promptText.rectTransform.anchoredPosition = new Vector2(650f, 10f);
            }
            
        }
        else
        {
            playerMovement.enabled = true; // Enable the PlayerMovement script
            playerCameraMovement.enabled = true; // Enable the CameraMovement script
            initialCameraPosition = playerCamera.transform.position; // Reset the position of the player's camera

            // Change the prompt text to normal state
            promptText.text = "Press " + fullscreenKey.ToString();

            // Restore the original prompt text position
            promptText.rectTransform.anchoredPosition = originalPromptPosition;
            promptText.rectTransform.anchorMin = originalPromptAnchorMin;
            promptText.rectTransform.anchorMax = originalPromptAnchorMax;
            promptText.rectTransform.pivot = originalPromptPivot;
        }

        // Adjust the viewport rect of the zoomed-in camera
        if (displayOnLeftSide)
        {
            zoomedCamera.rect = new Rect(0f, 0f, 0.5f, 1f); // Display on the left side
        }
        else
        {
            zoomedCamera.rect = new Rect(0.5f, 0f, 0.5f, 1f); // Display on the right side
        }
    }
}
