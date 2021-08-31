using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RadialTrigger : MonoBehaviour
{
    public Transform objTf;
    [Range(0f, 4f)]
    public float radius;
    public float distance;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 objPos = objTf.position;
        Vector2 origin = transform.position;

        distance = Vector2.Distance(objPos, origin);

        bool isInside = distance < radius;

        Handles.color = isInside ? Color.green : Color.red;
        Handles.DrawWireDisc(origin, Vector3.forward, radius);
    }
#endif
}
