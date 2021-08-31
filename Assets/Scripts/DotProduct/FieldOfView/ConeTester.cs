using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConeTester : MonoBehaviour
{
    public float detectionAngle = 45f;
    public float dotAngle;
    public float angleDotDistance;
    private Vector3 objectToDetect;
    public Vector3 objectDirection;
    private float dotDirection;

    public bool dotToDegrees;

    public bool TestCone(Vector3 other)
    {
        GetDotDirection(other);

        if (dotToDegrees)
        {
            //Degree to Degree (cpu intensive)
            //Get Angle from dotDistance, convert to degress compare directly to maxDetectionAngle
            return DotToDegrees();
        }
        else
        {
            //rad to rad (not cpu intensive)
            //Get dot from maxDetection, compare dotDistance to dotDetection
            return DetectionToDotProduct();
        }
    }

    private void GetDotDirection(Vector3 other)
    {
        //Get Direction from this object to the other object
        objectDirection = (other - transform.position).normalized;
        //the dot from the direction between the objects
        dotDirection = Vector3.Dot(objectDirection, transform.forward);
    }

    private bool DotToDegrees()
    {
        //Get angle from the direction between the objects
        angleDotDistance = Mathf.Acos(dotDirection) * Mathf.Rad2Deg;
        //Compare if the angle between objects is less than the defined angle
        return angleDotDistance < detectionAngle;
    }

    private bool DetectionToDotProduct()
    {
        //Get the dotProduct from the DetectionAngle
        dotAngle = Mathf.Cos(detectionAngle * Mathf.Deg2Rad);
        //Compare if the dot from the direction between the objects is inside the defined detectionAngle
        return dotDirection > dotAngle;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + ((objectToDetect - transform.position)));
        Handles.Label(transform.position + (objectToDetect - transform.position) /2, "Distance " + objectDirection);

        Gizmos.DrawLine(transform.forward, transform.forward + ((objectToDetect - transform.forward)));
        Handles.Label(transform.forward + (objectToDetect - transform.forward)/2, "Dot Product from Distance " + dotDirection);

        Vector3 offset = new Vector3(-0.05f, 0, -0.05f);
        Gizmos.DrawWireCube(transform.forward + offset, new Vector3(0.1f,0.1f,0.1f));
        Gizmos.DrawLine(transform.forward/3, transform.forward/3 + ((objectToDetect - transform.forward)/3)) ;
        Handles.Label(transform.forward / 3 + (objectToDetect - transform.forward) / 3, "Dot To Angle: " + angleDotDistance) ;



        var NDirection = new Vector3(0, Mathf.Sin(Mathf.Deg2Rad * detectionAngle), Mathf.Cos(Mathf.Deg2Rad * detectionAngle));
        var EDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * detectionAngle), 0 , Mathf.Cos(Mathf.Deg2Rad * detectionAngle));
        var WDirection = new Vector3(-Mathf.Sin(Mathf.Deg2Rad * detectionAngle), 0 , Mathf.Cos(Mathf.Deg2Rad * detectionAngle));
        var SDirection = new Vector3(0, -Mathf.Sin(Mathf.Deg2Rad * detectionAngle), Mathf.Cos(Mathf.Deg2Rad * detectionAngle));

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.forward * 15);
        Gizmos.DrawLine(transform.position, transform.position + NDirection.normalized * 15);
        Gizmos.DrawLine(transform.position, transform.position + SDirection.normalized * 15);
        Gizmos.DrawLine(transform.position, transform.position + WDirection.normalized * 15);
        Gizmos.DrawLine(transform.position, transform.position + EDirection.normalized * 15);
    }
}
