using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController = null;
    [SerializeField]
    private Transform groundCheck = null;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float movementSpeed = 12.0f;
    [SerializeField]
    private float jumpHeight = 3.0f;
    [SerializeField]
    private float gravity = -9.81f;

    private Vector3 velocity = Vector3.zero;

    private bool isGrounded = false;

    private void LateUpdate()
    {
        //GroundCheck();
        Movement();
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            //might be that CheckSphere registers before player actually on ground
            //so set y to small -value to force player on the ground
            velocity.y = -2.0f;
        }
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 motion = transform.right * x + transform.forward * z;

        characterController.Move(motion * movementSpeed * Time.deltaTime);
    }

    private void OldMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 motion = transform.right * x + transform.forward * z;

        characterController.Move(motion * movementSpeed * Time.deltaTime);

        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //    velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        //}

        //velocity.y += gravity * Time.deltaTime;

        //characterController.Move(velocity * Time.deltaTime);
    }
}
