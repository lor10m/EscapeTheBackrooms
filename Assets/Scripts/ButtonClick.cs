using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonClick : MonoBehaviour
{
   
    public GameObject cubeWithImage;
    public Camera playerCamera;

    public TextMeshProUGUI promptText;
    public bool displayOnLeftSide = true;
    public KeyCode pressButton;
    public ButtonSequenceManager sequenceManager;
    public string buttonIdentifier;
    public int maxDistance;

    private bool isLookingAtCube = false;

    private void Start()
    {
        promptText.text = "[" + pressButton + "] Press";
        promptText.gameObject.SetActive(false); // Disable the prompt text initially
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

                // Check for key press to activate sound effect
                if (Input.GetKeyDown(pressButton))
                {
                    
                    sequenceManager.AddToSequence(buttonIdentifier);
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

        // Enable/disable the prompt text based on raycast result and distance
        promptText.gameObject.SetActive(isLookingAtCube && Vector3.Distance(playerCamera.transform.position, hit.point) <= maxDistance && playerCamera.gameObject.activeInHierarchy);
    }
}
