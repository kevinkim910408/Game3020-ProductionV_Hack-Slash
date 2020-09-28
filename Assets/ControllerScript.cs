using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: ControllerScript.cs
/// History: 
/// 2020-09-28  - add variables about movements.
///             - Jump, Dash methods
///             - Critical Error: error CS1061: 'CharacterController' does not contain a definition for 'Move' and no accessible exten
///             - How to fix? change the script name not same as 'CharacterController'. This name makes Unity does not know how to access 'CharacterController'.
///             - Character Controller + NavMesh Agent
///             - Working good
///             
/// Last Modified: 2020-09-28
/// </summary>

public class ControllerScript : MonoBehaviour
{
    #region Variables
    //get NavMesh
    private NavMeshAgent navMeshAgent;
    private Camera camera;

    //to get CharacterController from the unity
    private CharacterController characterController;

    //to calculate
    private Vector3 calcVelocity = Vector3.zero;

    //prevent double jump
    private bool isGround = false;
    [SerializeField]
    float groundCheckDistance = 0.3f;

    // to set layers
    [SerializeField]
    LayerMask groundLayerMask;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //to get CharacterController from the unity
        characterController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // will use character controller position
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = true;

        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // 0 - left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(Input.mousePosition.ToString());


            // Make ray from screen to world
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            // Check hit
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100, groundLayerMask))
            {
                //Debug.Log("We hit " + raycastHit.collider.name + " " + raycastHit.point);

                // Move our player to what we hit
                navMeshAgent.SetDestination(raycastHit.point);
            }
        }

        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            characterController.Move(navMeshAgent.velocity * Time.deltaTime);
        }
        else
        {
            characterController.Move(Vector3.zero);
        }
    }
    private void LateUpdate()
    {
        transform.position = navMeshAgent.nextPosition;
    }
} 