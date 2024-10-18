using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private bool isGrounded;
    private bool isSprinting;
    public float speed = 5.0f;
    public float gravity = -9.8f;
    public float jumpForce = 1.0f;
    public float sprintModifier = 2.0f;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void Move(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        moveVector.y += gravity * Time.deltaTime;
        if (isGrounded && moveVector.y < 0)
            moveVector.y = -2.0f;
        controller.Move(moveVector * Time.deltaTime);
        Debug.Log("Force: " + moveVector);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            moveVector.y = Mathf.Sqrt(jumpForce * -2.0f * gravity);
        }
    }

    public void Sprint(bool isSprinting)
    {
        // if in the air, don't allow sprinting to affect movement
        if (!isGrounded)
            return;
        this.isSprinting = isSprinting;
        if (isSprinting)
            speed *= sprintModifier;
        else
            speed /= sprintModifier;
    }
}
