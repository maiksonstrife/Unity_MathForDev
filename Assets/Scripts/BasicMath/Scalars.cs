using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scalars : PagesAbstract
{

    public Transform Object1;
    public Transform Object2;

    [Header("What is a Scalar?")]
    [Range(0,1)]
    public float scalar;

    private void OnDrawGizmos()
    {
        if(examples.Length < 1) examples = new bool[20];
        Object1.transform.localScale = new Vector3(1, 1, 1);

        ExamplesController();
        SetTitle("What is Scalar", 5);
        if (examples[0] && currentPage == 1) Example_1();
        if (examples[1] && currentPage == 2) Example_2();
        if (examples[2] && currentPage == 3) Example_3();
        if (examples[2] && currentPage == 4) Example_4();
        if (examples[2] && currentPage == 5) Example_5();
    }

    void ExamplesController()
    {
        if (currentPage <= 4)
        {
            Object1.transform.rotation = Quaternion.Euler(0, 0, 0);
            Object1.gameObject.SetActive(true);
        }
        else
        {
            Object1.transform.rotation = Quaternion.Euler(0, 0, 45);
            Object1.gameObject.SetActive(false);
        }
    }

    private void Example_5()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, 0), "With a Vector the rule stays the same");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.2f), "Formula : ");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.4f), "offset + Vector * scalar");

        Object1.transform.rotation = Quaternion.Euler(0,0,45);
        Vector3 newVector = Object1.right;
        Gizmos.DrawLine(Object1.position, Object1.position + newVector);
        Gizmos.DrawSphere(Object1.position + newVector * scalar, 0.2f);
        Handles.Label(Object1.position + newVector * scalar + new Vector3(0.5f, 0), "Scalar : " + scalar);
        
    }

    private void Example_4()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, 0), "Any unity: Object, position, Vertices");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.2f), "Multipled by something: you get a measure");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.4f), "That is equivalent to a position from that unity");

        Gizmos.DrawSphere((Object1.position - Vector3.up) + Vector3.up * 2 * scalar, 0.2f);

        Handles.Label((Object1.position - Vector3.up) + Vector3.up * 2 * 0.5f + new Vector3(0.5f, 0.4f), "Use Scalar to travel along Cylinder");
        Handles.Label((Object1.position - Vector3.up) + Vector3.up * 2 * 0.5f + new Vector3(0.5f, 0), "scalar : " + scalar);
    }

    private void Example_3()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, 0), "If a unit is equal to 1");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.2f), "Where is the half of Cylinder?");

        Gizmos.DrawSphere((Object1.position - Vector3.up) + Vector3.up * 2 * 0.5f, 0.2f);
        Handles.Label((Object1.position - Vector3.up) + Vector3.up * 2 * 0.5f + new Vector3(0.5f, 0), "scalar : " + 0.5);
    }

    private void Example_2()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, 0), "If we want to travel along cylinder");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.2f), "We now that 1 cylinder equals to  * 1");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.4f), "Cylinder * 1 is 1 unit cylinder");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.6f), "* 1 is a scalar being used to travel along cylinder");

        Gizmos.DrawSphere(Object1.position + Vector3.up, 0.2f);
        Handles.Label(Object1.position + Vector3.up + new Vector3(0.5f,0), "scalar : " + 1);
    }

    private void Example_1()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(1f, 0), "Scalar is just multiplying by a number");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(1f, -0.2f), "The number would be used to travel along a object");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(1f, -0.4f), "as a measure system");
    }
}
