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

    private float footstepTimerElapsed = 0.0f;

    private void LateUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 motion = transform.right * x + transform.forward * z;

        if (motion != Vector3.zero)
        {
            FootstepTimerTick();
        }
        
        characterController.Move(motion * movementSpeed * Time.deltaTime);
    }

    private void FootstepTimerTick()
    {
        footstepTimerElapsed += Time.deltaTime;

        if (footstepTimerElapsed >= 0.7)
        {
            AudioManager.Instance.PlayRandomFootstepSound();
            footstepTimerElapsed = 0.0f;
        }
    }
}
