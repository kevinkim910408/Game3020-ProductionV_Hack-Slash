using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: RigidBodyCharacter.cs
/// History: 
/// 2020-09-28  - add variables about movements.
///             - 
///             
/// Last Modified: 2020-09-28
/// </summary>

public class RigidBodyCharater : MonoBehaviour
{
    #region Variables
    //basic variables about the movements
    public float speed = 5.0f;
    public float jumpHeight = 2.0f;
    public float dashDistance = 5.0f;

    //to get Rigidbody from the unity
    private Rigidbody rigidbody;

    //movement(vector3)
    private Vector3 inputDirection = Vector3.zero;

    //prevent double jump
    private bool isGround = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //to get Rigidbody from the unity
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGroundStatus();

        //user input - basic movments
        inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");

        //actual code about user movements
        if(inputDirection != Vector3.zero)
        { 
            transform.forward = inputDirection;
        }

        //user input - jump
        if (Input.GetButtonDown("Jump") && isGround)
        {
            Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpHeight * -2.0f * Physics.gravity.y);
            rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }

        //user input - dash
        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime), 
                0, (Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime)));
            rigidbody.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + inputDirection * speed * Time.fixedDeltaTime);
    }

    #region Methods
    //to check if player is on the ground
    void CheckGroundStatus()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out raycastHit, groundCheckDistance, groundLayerMask))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    #endregion Methods

}
