using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 5.0F;
    public float gravity = 10.0F;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {        
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}