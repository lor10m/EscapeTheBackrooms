using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreviewCam : MonoBehaviour
{
    public Transform bodyTransform;
    public float rotationSpeed = 75.0f;
    public string rotateLeft;
    public string rotateRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(rotateLeft))
        {
            bodyTransform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(rotateRight))
        {
            bodyTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
