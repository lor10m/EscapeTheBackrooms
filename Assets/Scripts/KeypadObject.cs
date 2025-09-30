using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class KeypadObject : MonoBehaviour
{
    public GameObject cubeWithImage;
    public Camera playerCamera;
    public int maxDistance;

    [SerializeField] KeyCode inspectKey;
    [SerializeField] GameObject promptText;
    [SerializeField] private GameObject keypad;

    private bool isLookingAtCube = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Cast a ray from the player's camera position and direction
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance)) // Replace maxDistance with your desired maximum distance
        {
            isLookingAtCube = hit.collider.gameObject == cubeWithImage;
        }
        else
        {
            isLookingAtCube = false;
        }

        // Enable/disable the prompt text based on raycast result and distance
        if (isLookingAtCube && Vector3.Distance(playerCamera.transform.position, hit.point) <= maxDistance && playerCamera.gameObject.activeInHierarchy)
        {
            if (!promptText.activeSelf && !keypad.activeSelf)
            {

                promptText.SetActive(true);
            }
        }
        else if (promptText.activeSelf)
        {
            promptText.SetActive(false);
        }


        if (promptText.activeSelf && Input.GetKeyDown(inspectKey) && !keypad.activeSelf)
        {
            promptText.SetActive(false);
            Cursor.visible = true;
            keypad.SetActive(true);
        }
        else if (Input.GetKeyDown(inspectKey) && keypad.activeSelf)
        {
            promptText.SetActive(true);
            Cursor.visible = false;
            keypad.SetActive(false);
        }
        
    }
}
