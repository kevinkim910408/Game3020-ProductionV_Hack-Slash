using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 5.0f;

    [Range(0, 360)]
    public float viewAngle = 90.0f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private List<Transform> visibleTargets = new List<Transform>();

    private Transform nearestTarget;
    private float distanceToTarget = 0.0f;

    public float delay = 0.2f;
    
    public List<Transform> VisibleTargets => visibleTargets;
    public Transform NearestTarget => nearestTarget;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FindTargetWithDelay", delay);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        distanceToTarget = 0.0f;
        nearestTarget = null;
        visibleTargets.Clear();

        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position,
            viewRadius, targetMask);
        for(int i = 0; i < targetInViewRadius.Length; ++i)
        {
            Transform target = targetInViewRadius[i].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized; 
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    if(nearestTarget == null || (distanceToTarget > dstToTarget))
                    {
                        nearestTarget = target;
                        distanceToTarget = dstToTarget;
                    }
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDegree, bool angleIsGlbal)
    {
        if (!angleIsGlbal)
        {
            angleInDegree += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0,
            Mathf.Cos(angleInDegree * Mathf.Deg2Rad));

    }

}
