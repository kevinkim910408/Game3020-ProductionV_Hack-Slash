using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: TopDownCamera.cs
/// History: 
/// 2020-09-28  - camera follows player
///             - mouse wheel = zoom in and out
///             
/// Last Modified: 2020-09-28
/// </summary>

public class TopDownCamera : MonoBehaviour
{
    #region Variables

    //Basic variables for camera
    [SerializeField]
    float height = 5.0f;
    [SerializeField]
    float distance = 10f;
    [SerializeField]
    float angle = 45.0f;
    [SerializeField]
    float lookAtHeight = 2.0f;
    [SerializeField]
    float smoothSpeed = 0.5f;

    //to use mouse wheels
    private Camera mainCamera;
    private Vector3 defalutDirection;
    [SerializeField]
    float mouseSpeed = 10.0f;

    //target = player
    [SerializeField]
    Transform target;

    private Vector3 refVelocity;
    #endregion

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        defalutDirection = transform.forward;
    }

    void Update()
    {
        float scrolling = Input.GetAxis("Mouse ScrollWheel") * mouseSpeed;

        //set max zoom in
        if (mainCamera.fieldOfView <= 20.0f && scrolling < 0)
            mainCamera.fieldOfView = 20.0f;
        //set max zoom out
        else if (mainCamera.fieldOfView >= 80.0f && scrolling > 0)
            mainCamera.fieldOfView = 80.0f;
        //actual code for zoom in and out
        else
            mainCamera.fieldOfView += scrolling;
    }
    //LateUpdate is called after all Update functions have been called.
    void LateUpdate()
    {
        HandleCamera();
    }

    [SerializeField]
    public void HandleCamera()
    {
        if (!target)
        {
            return;
        }

        // Build world position vector
        Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);

        // Build Rotated vector
        Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;

        // Move player position
        Vector3 flatTargetPosition = target.position;
        flatTargetPosition.y += lookAtHeight;

        Vector3 finalPosition = flatTargetPosition + rotatedVector;

        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed);

        transform.LookAt(target.position);
    }
}
