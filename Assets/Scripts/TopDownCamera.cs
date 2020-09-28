using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    #region Variables

    //Basic variables for camera
    [SerializeField]
    float height = 5f;
    [SerializeField]
    float distance = 10f;
    [SerializeField]
    float angle = 45f;
    [SerializeField]
    float lookAtHeight = 2f;
    [SerializeField]
    float smoothSpeed = 0.5f;

    //target = player
    [SerializeField]
    Transform target;

    private Vector3 refVelocity;
    #endregion

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
