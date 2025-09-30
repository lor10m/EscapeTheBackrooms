using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform bodyTransform; // Reference to the character's body transform
    public Animator anim;
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 75.0f;
    public float jumpForce = 5.0f;
    public Transform groundCheck;
    public LayerMask ground;
    public string horizontalMovement;
    public string verticalMovement;
    public string rotateLeft;
    public string rotateRight;
    public string jump;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis(horizontalMovement);
        float verticalInput = Input.GetAxis(verticalMovement);

        anim.SetBool("isGrounded", isGrounded);

        // Rotate the body left or right
        if (Input.GetKey(rotateLeft))
        {
            bodyTransform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(rotateRight))
        {
            bodyTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if(horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        anim.SetInteger("Direction", (int)horizontalInput);
        float x = (int)horizontalInput;
        //UnityEngine.Debug.Log(x.ToString());
        // Update movement direction based on the body's rotation
        Vector3 movementDirection = bodyTransform.forward * verticalInput + bodyTransform.right * horizontalInput;

        // Apply movement force to the character
        rb.velocity = new Vector3(movementDirection.x * movementSpeed, rb.velocity.y, movementDirection.z * movementSpeed);

        if (Input.GetButtonDown(jump) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }
}
