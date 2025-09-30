using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject character;
    public GameObject cameraCenter;
    public float yOffset;
     
    public float sensitivity;  //speed of rotation of camera (bigger it is, more sensitive camera is to input)   
    public Camera cam;

    public string horizontalMovement;
    public string verticalMovement;

    private RaycastHit _camHit;
    public Vector3 camDist;
    float collisionSensitivity = 2.5f;


    void Start()
    {
        camDist = cam.transform.localPosition;
    }

    void Update()
    {
        // The CameraCenter (empty gameobject) follows always the character's position:
        var position1 = character.transform.position;
        cameraCenter.transform.position = new Vector3(position1.x, position1.y + yOffset, position1.z);

        // Rotation of CameraCenter, and thus the camera, depending on Mouse Input:
        var rotation = cameraCenter.transform.rotation;
        rotation = Quaternion.Euler(rotation.eulerAngles.x - Input.GetAxis(verticalMovement) * sensitivity / 2, rotation.eulerAngles.y + Input.GetAxis(horizontalMovement) * sensitivity, rotation.eulerAngles.z);
        cameraCenter.transform.rotation = rotation;

        // Apply calculated camera position
        var transform2 = cam.transform;
        transform2.localPosition = camDist;

        // Check and handle Collision
        GameObject obj = new GameObject();
        obj.transform.SetParent(transform2.parent);
        var position = cam.transform.localPosition;
        obj.transform.localPosition = new Vector3(position.x, position.y, position.z - collisionSensitivity);


        if (Physics.Linecast(cameraCenter.transform.position, obj.transform.position, out _camHit))
        {
            //This gets executed if there's any collider in the way
            var transform1 = cam.transform;
            transform1.position = _camHit.point;
            var localPosition = transform1.localPosition;
            localPosition = new Vector3(localPosition.x, localPosition.y, localPosition.z + collisionSensitivity);
            transform1.localPosition = localPosition;
        }
        // Clean up
        Destroy(obj);

        // Make sure camera can't clip into player because of collision
        if (cam.transform.localPosition.z > -1f)
        {
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -1f);
        }
    }
}