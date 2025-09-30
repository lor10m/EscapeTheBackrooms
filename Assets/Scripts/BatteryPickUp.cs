using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BatteryPickUp : MonoBehaviour
{
    public GameObject cubeWithImage;
    public Camera playerCamera;

    public TextMeshProUGUI promptText;
    public bool displayOnLeftSide = true;
    public KeyCode changeVideo;
    public int maxDistance;

    private bool isLookingAtCube = false;
    private bool batteriesPickedUp;
    private bool alreadyChanged = false;

    public TvRemote tvRemote1;
    public TvRemote tvRemote2;
    private void Start()
    {
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
                if (Input.GetKeyDown(changeVideo) & !alreadyChanged)
                {
                    alreadyChanged = true;
                    tvRemote1.BatteriesFound();
                    tvRemote2.BatteriesFound();
                    batteriesPickedUp = true;
                    gameObject.SetActive(false); // Deactivate the object to make it invisible
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
        if (batteriesPickedUp)
        {
            promptText.text = "";
        }
        else
        {
            promptText.text = "[" + changeVideo + "] " + "Pick Up";
        }
    }
}
