using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TvRemote : MonoBehaviour
{
    public GameObject cubeWithImage;
    public Camera playerCamera;

    public TextMeshProUGUI promptText;
    public bool displayOnLeftSide = true;
    public KeyCode changeVideo;
    public int maxDistance;
    public VideoPlayerController videoPlayerController;
    public SwitchVideo switchVideo;

    private bool isLookingAtCube = false;
    private bool batteriesPickedUp;
    private bool alreadyChanged = false;



    private void Start()
    {
        promptText.gameObject.SetActive(false); // Disable the prompt text initially
    }

    private void Update()
    {

        // Cast a ray from the player's camera position and direction
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        //UnityEngine.Debug.Log("xremote!");
        if (Physics.Raycast(ray, out hit, maxDistance)) // Replace maxDistance with your desired maximum distance
        {
            //UnityEngine.Debug.Log("damnremote!");
            if (hit.collider.gameObject == cubeWithImage)
            {
                //UnityEngine.Debug.Log("videoremote");
                isLookingAtCube = true;

                // Check for key press to activate sound effect
                if (Input.GetKeyDown(changeVideo) & !alreadyChanged & batteriesPickedUp)
                {
                    alreadyChanged = true;
                    switchVideo.TvOn();
                    videoPlayerController.SwitchVideo();
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
            if (alreadyChanged)
            {
                promptText.text = "";
            }
            else
            {
                promptText.text = "" + changeVideo.ToString() + " to start";
            }
            
        }
        else
        {
            promptText.text = "No Batteries";
        }
    }
    public void BatteriesFound()
    {
        batteriesPickedUp = true;
    }






}
