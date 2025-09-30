using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public GameObject obj;
    public Camera playerCamera;
    public int maxDistance;

    [SerializeField] Rigidbody dragableObject;
    [SerializeField] string dragKey;
    [SerializeField] GameObject promptText;

    public Transform objectTransform;
    public string horizontalMovement;
    public string verticalMovement;
    public int movementSpeed;

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
            isLookingAtCube = hit.collider.gameObject == obj;
        }
        else
        {
            isLookingAtCube = false;
        }

        // Enable/disable the prompt text based on raycast result and distance
        if (isLookingAtCube && Vector3.Distance(playerCamera.transform.position, hit.point) <= maxDistance && playerCamera.gameObject.activeInHierarchy)
        {
            if (!promptText.activeSelf && !Input.GetKeyDown(dragKey))
            {
                promptText.SetActive(true);
            }
        }
        else if (promptText.activeSelf)
        {
            promptText.SetActive(false);
        }


        if (promptText.activeSelf && Input.GetKeyDown(dragKey))
        {
            promptText.SetActive(false);
            //drag object
            Drag();
        }
    }

    void Drag()
    {
        float horizontalInput = Input.GetAxis(horizontalMovement);
        float verticalInput = Input.GetAxis(verticalMovement);

        Vector3 movementDirection = objectTransform.forward * verticalInput + objectTransform.right * horizontalInput;
        dragableObject.velocity = new Vector3(movementDirection.x * movementSpeed, dragableObject.velocity.y, movementDirection.z * movementSpeed);
    }
}
