using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    public bool lockedDoor;
    public GameObject door;
    public Camera playerCamera;
    public TextMeshProUGUI promptText;
    public KeyCode openDoor;
    public int maxDistance;
    public AudioClip openAudioClip;
    public ScoreBoard scoreboardScript;

    private bool riddleSolved = false;
    private bool isOpen = false; // Flag to track if the door is open
    private bool isRotating = false;
    private float rotationSpeed = 1f; // Degrees per second
    private bool lastRiddleSolved;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        promptText.gameObject.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Cast a ray from the player's camera position and direction
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        bool isLookingAtDoor = false;
        bool isCloseEnough = false;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.gameObject == door)
            {
                isLookingAtDoor = true;
                isCloseEnough = Vector3.Distance(playerCamera.transform.position, hit.point) <= maxDistance;
            }
        }

        promptText.gameObject.SetActive(isLookingAtDoor && isCloseEnough);

        if (isLookingAtDoor && isCloseEnough)
        {
            if (lockedDoor)
            {
                if (Input.GetKeyDown(openDoor) && riddleSolved)
                {
                    audioManager.PlaySFX(openAudioClip);
                    OpenTheDoor();
                }
            }
            else
            {
                if (Input.GetKeyDown(openDoor))
                {
                    audioManager.PlaySFX(openAudioClip);
                    OpenTheDoor();
                }
            }
        }

        if (lockedDoor)
        {
            CheckRiddle();
        }
        else
        {
            if (isOpen)
            {
                promptText.text = "[" + openDoor.ToString() + "] " + "Close";
            }
            else
            {
                promptText.text = "[" + openDoor.ToString() + "] " + "Open";
            }
        }
    }

    private void OpenTheDoor()
    {
        if (lastRiddleSolved)
        {
            if (!isRotating)
            {
                StartCoroutine(RotateDoor());
            }
            scoreboardScript.WonGame();
        }
        else
        {
            if (!isRotating)
            {
                StartCoroutine(RotateDoor());
            }
        }
        
    }

    private IEnumerator RotateDoor()
    {
        isRotating = true;

        // Determine the target rotation based on the current rotation
        Quaternion startRotation = door.transform.rotation;
        Quaternion targetRotation;

        if (lockedDoor)
        {
            if (isOpen)
            {
                targetRotation = startRotation * Quaternion.Euler(0f, -90f, 0f);
            }
            else
            {
                targetRotation = startRotation * Quaternion.Euler(0f, 90f, 0f);
            }
        }
        else
        {
            if (isOpen)
            {
                targetRotation = startRotation * Quaternion.Euler(0f, -90f, 0f);
            }
            else
            {
                targetRotation = startRotation * Quaternion.Euler(0f, 90f, 0f);
            }
        }

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed;

            // Use Quaternion.Lerp to interpolate between the start and target rotations
            door.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }

        isOpen = !isOpen;
        isRotating = false;
    }


    public void ActivateDoor()
    {
        riddleSolved = true;
        UnityEngine.Debug.Log("Activated door");
    }

    private void CheckRiddle()
    {
        if (riddleSolved)
        {
            if (isOpen)
            {
                promptText.text = "[ " + openDoor.ToString() + " ]" + "Close";
            }
            else
            {
                promptText.text = "[ " + openDoor.ToString() + " ]" + "Open";
            }
        }
        else
        {
            promptText.text = "Locked";
        }
    }
    public void LastRiddleSolved()
    {
        lastRiddleSolved = true;
    }
       
}
