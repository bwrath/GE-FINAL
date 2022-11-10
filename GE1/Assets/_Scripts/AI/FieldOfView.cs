using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{
    public List<Transform> visibleTargets;

    [SerializeField] public float Radius;
    [SerializeField] [Range(0, 360f)] public float Angle;
    [SerializeField] public float Interval;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    private void Start()
    {
        StartCoroutine(FindTargets(Interval));
    }

    IEnumerator FindTargets(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            FindTarget();
        }
    }

    private void FindTarget()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, Radius, TargetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[0].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < Angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, ObstacleMask))
                    visibleTargets.Add(target);
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) //for angleIsGlobal, if true the angle will always point at the same direction, otherwise it will be relative to the object using the field of view.
    {
        if (!angleIsGlobal) angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)); //This function converts Unity angles into a direction, since Unity angles starts from the upward direction
    }
}
