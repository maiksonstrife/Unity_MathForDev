using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PointAlongDirection : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float dotDistance;
    public Vector3 aToB;
    public Vector3 aToBDir;
    public float magnitude;

    [Range(0f, 4f)]
    public float offset;
    [Range(0f, 45f)]
    public float threshold;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Vector3 vectorA = pointA.position;
        Vector3 vectorB = pointB.position;

        aToB = vectorB - vectorA;
        aToBDir = aToB.normalized;

        magnitude = aToBDir.magnitude;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(vectorA, vectorA + aToBDir);
        Handles.Label(vectorA + (aToB) / 4, "Bullet Direction");

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(vectorA,vectorA + pointA.forward);
        Handles.Label(vectorA + (vectorA + pointA.forward) / 4, "Object Foward");

        Vector3 offsetVector = aToBDir * offset;
        Gizmos.color = DetectPlayer() ? Color.green : Color.red;
        Gizmos.DrawSphere(vectorA + offsetVector, 0.2f);
    }


    private bool DetectPlayer()
    {
        float dotThreshold = Mathf.Cos(threshold * Mathf.Deg2Rad);

        dotDistance = Vector3.Dot(aToBDir, pointA.forward);

        return dotDistance > dotThreshold;
    }
#endif
}
