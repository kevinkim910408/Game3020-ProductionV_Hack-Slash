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
        //user input
        inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");

        //actual code about user movements
        if(inputDirection != Vector3.zero)
        { 
            transform.forward = inputDirection;
        }
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + inputDirection * speed * Time.fixedDeltaTime);
    }


}
