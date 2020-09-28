using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: ControllerScript.cs
/// History: 
/// 2020-09-28  - add variables about movements.
///             - Jump, Dash methods
///             - Critical Error: error CS1061: 'CharacterController' does not contain a definition for 'Move' and no accessible exten
///             - How to fix? change the script name not same as 'CharacterController'. This name makes Unity does not know how to access 'CharacterController'.
///             - Working good
///             
/// Last Modified: 2020-09-28
/// </summary>

public class ControllerScript : MonoBehaviour
{
    #region Variables
    //basic variables about the movements
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float jumpHeight = 2.0f;
    [SerializeField]
    float dashDistance = 5.0f;

    //gravity and drag
    [SerializeField]
    float gravity = -29.81f;
    [SerializeField]
    private Vector3 drags;

    //to get CharacterController from the unity
    private CharacterController characterController;

    //to calculate
    private Vector3 calcVelocity = Vector3.zero;

    //movement(vector3)
    private Vector3 inputDirection = Vector3.zero;

    //prevent double jump
    private bool isGround = false;
    [SerializeField]
   LayerMask groundLayerMask; // in the ground layer, player can jump only
    [SerializeField]
    float groundCheckDistance = 0.3f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //to get CharacterController from the unity
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check grounded
        bool isGrounded = characterController.isGrounded;

        //if player is on the ground, gravity = 0
        if (isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0f;
        }

        //user input - basic movments
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * speed);

        //actual code about user movements
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        //user input - jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //user input - dash
        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash");
            calcVelocity += Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * drags.x + 1)) / -Time.deltaTime),
                0,
                (Mathf.Log(1f / (Time.deltaTime * drags.z + 1)) / -Time.deltaTime))
                );
        }

        //gravity
        calcVelocity.y += gravity * Time.deltaTime;

        //dash ground drags
        calcVelocity.x /= 1 + drags.x * Time.deltaTime;
        calcVelocity.y /= 1 + drags.y * Time.deltaTime;
        calcVelocity.z /= 1 + drags.z * Time.deltaTime;

        characterController.Move(calcVelocity * Time.deltaTime);
    }
}