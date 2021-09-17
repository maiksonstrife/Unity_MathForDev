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
        if (examples == null || examples.Length !=10) examples = new bool[10];

        Object1.transform.localScale = new Vector3(1, 1, 1);

        ExamplesController();
        SetTitle("What is Scalar", 1, 4);
        if (examples[0] && currentPage == 1) Example_1();
        if (examples[1] && currentPage == 2) Example_2();
        if (examples[2] && currentPage == 3) Example_3();
        if (examples[3] && currentPage == 4) Example_4();
        SetTitle("Multiplying", 5, 6);
        if (examples[4] && currentPage == 5) Example_5();
        if (examples[5] && currentPage == 6) Example_6();
        SetTitle("Dividing", 7, 10);
        if (examples[6] && currentPage == 7) Example_7(); 
        if (examples[7] && currentPage == 8) Example_8();
        if (examples[8] && currentPage == 9) Example_9();
    }

    private void Example_9()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(-2f, 1), "On Division we have the Interpolation");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(-2f, 0.8f), "Transforming a unity in equal spaces");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(-2f, 0.6f), "Division and Multiply has a close relationship with Scalars");

        Object1.transform.rotation = Quaternion.Euler(0, 0, 45);
        Vector3 newVector = Object1.right * 5;
        Gizmos.DrawLine(Object1.position, Object1.position + newVector);

        int length = 4;

        for (int i = 0; i <= length; i++)
        {
            float dividePos = (float)i / 4;

            Gizmos.DrawSphere(Object1.position + newVector * dividePos, 0.1f);
            Handles.Label(Object1.position + newVector * dividePos + new Vector3(0.5f, 0),  i + " / 4 : " + dividePos);
        }
    }

    private void Example_8()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(-2f, 1), "We can find any given point from bottom to top using Division");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(-2f, 0.8f), "1/2, 1/3, 1/4, 1/5, of the Object");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(-2f, 0.6f), "float Half = (float) 1/2; Vector * Half");

        Object1.transform.rotation = Quaternion.Euler(0, 0, 45);
        Vector3 newVector = Object1.right*5;
        Gizmos.DrawLine(Object1.position, Object1.position + newVector);

        int length = 5; 

        for (int i = 0; i <= length; i++)
        {
            float dividePos = (float)1 / i;

            Gizmos.DrawSphere(Object1.position + newVector * dividePos, 0.1f);
            Handles.Label(Object1.position + newVector * dividePos + new Vector3(0.5f, 0), "1 / " + i + " : " + dividePos);
        }
    }

    private void Example_7()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, 0), "With Division we can find a position inside a Scalar");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.2f), "And apply to a Vector afterwards with multiply");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.4f), "float Half = (float) 1/2; Vector * Half");

        Object1.transform.rotation = Quaternion.Euler(0, 0, 45);
        Vector3 newVector = Object1.right;
        Gizmos.DrawLine(Object1.position, Object1.position + newVector);
        float half = (float) 1 / 2;
        Gizmos.DrawSphere(Object1.position + newVector * half, 0.2f);
        Handles.Label(Object1.position + newVector * half + new Vector3(0.5f, 0), "Scalar : " + half);
    }

    private void Example_6()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, 0), "Scalars is a universal ruler as a measure system");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.2f), "With multiplication we can travel along a vector directly");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.4f), "Vector * Scalar");

        Object1.transform.rotation = Quaternion.Euler(0, 0, 45);
        Vector3 newVector = Object1.right;
        Gizmos.DrawLine(Object1.position, Object1.position + newVector);
        Gizmos.DrawSphere(Object1.position + newVector * scalar, 0.2f);
        Handles.Label(Object1.position + newVector * scalar + new Vector3(0.5f, 0), "Scalar : " + scalar);
    }

    private void Example_5()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, 0), "Any Object, Vector, Direction, Space, Constant number, the rule Applies");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.2f), "We can get a position from anything");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.4f), "offset + Vector * scalar");

        Object1.transform.rotation = Quaternion.Euler(0,0,45);
        Vector3 newVector = Object1.right;
        Gizmos.DrawLine(Object1.position, Object1.position + newVector);
        Gizmos.DrawSphere(Object1.position + newVector * scalar, 0.2f);
        Handles.Label(Object1.position + newVector * scalar + new Vector3(0.5f, 0), "Scalar : " + scalar);
        
    }

    private void Example_4()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, 0), "Any object can be set by the size of 1");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.2f), "if that unity is multiplied by a scalar");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.4f), "You get the position at the given scalar from the unity (0.25 * 1)");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.6f), "Notice that Scalars can measure any object equaly");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.8f), "0.2347 is a random number, that will deliver the same position any any given Object");

        Gizmos.DrawSphere((Object1.position - Vector3.up) + Vector3.up * 2 * scalar, 0.2f);

        Handles.Label((Object1.position - Vector3.up) + Vector3.up * 2 * 0.5f + new Vector3(0.5f, 0.4f), "Use Scalar to travel along the Objects (Scalar * Object)");
        Handles.Label((Object1.position - Vector3.up) + Vector3.up * 2 * 0.5f + new Vector3(0.5f, 0), "This Position is...");
        Handles.Label((Object1.position - Vector3.up) + Vector3.up * 2 * 0.5f + new Vector3(0.5f, -0.2f), "Scalar : " + scalar);
    }

    private void Example_3()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, 0), "We can also say that 0,5 is the half of the Cylinder");

        Gizmos.DrawSphere((Object1.position - Vector3.up) + Vector3.up * 2 * 0.5f, 0.2f);
        Handles.Label((Object1.position - Vector3.up) + Vector3.up * 2 * 0.5f + new Vector3(0.5f, 0), "scalar : " + 0.5);
    }

    private void Example_2()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, 0), "Scalar is used to travel to a point along the object");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.2f), "This cylinder we can define it by the size of 1 in Height");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.4f), "1 is the top of the cylinder");
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(0.5f, -0.6f), "And 0 is the bottom, 1 and 0 is the Scalars");

        Gizmos.DrawSphere(Object1.position + Vector3.up, 0.2f);
        Handles.Label(Object1.position + Vector3.up + new Vector3(0.5f,0), "scalar : " + 1);
    }

    private void Example_1()
    {
        Handles.Label(Object1.position + Vector3.up * 2 + new Vector3(1f, 0), "Scalar is a number to travel along a Vector Space");
    }

    void ExamplesController()
    {
        if (currentPage == 4) Object2.gameObject.SetActive(true);
        else Object2.gameObject.SetActive(false);


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
}
