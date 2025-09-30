using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Essentials
    public Transform cam;
    public Transform firstPersonCam;
    CharacterController controller;
    float turnSmoothTime = 0.5f;
    float turnSmoothVelocity;
    Animator anim;

    // Movement
    public string horizontalMovement;
    public string verticalMovement;
    Vector2 movement;
    public float moveSpeed;

    // Jumping
    public float jumpHeight;
    public float gravity;
    bool isGrounded;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(transform.position, 1f, 1);
        anim.SetBool("isGrounded", isGrounded);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1;
        }

        anim.transform.localPosition = Vector3.zero;
        anim.transform.localEulerAngles = Vector3.zero;
        movement = new Vector2(Input.GetAxis(horizontalMovement), Input.GetAxis(verticalMovement));
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;
   
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        if (Input.GetButtonDown("Jump1") && isGrounded)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
        }

        if(velocity.y > -20)
        {
            velocity.y += (gravity * 10) * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);

        firstPersonCam.transform.position = new Vector3(controller.transform.position.x + 0.0f, controller.transform.position.y + 2.0f, controller.transform.position.z + 0.3f);
        firstPersonCam.transform.rotation = controller.transform.rotation;
    }
}
