using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Subtraction : PagesAbstract
{
    public Transform object1;
    public Transform object2;
    private Vector3 newPosition;

    private void OnDrawGizmos()
    {
        if (examples.Length < 1) examples = new bool[20];

        if (!examples[6])
        {
            if (!examples[0]) return;
            SetTitle("Vector - Vectort", 6);
            Example_1();

            if (!examples[1]) return;
            Example_2();

            if (!examples[2]) return;
            Example_3();

            if (!examples[3]) return;
            Example_4();

            if (!examples[4]) return;
            Example_5();

            if (!examples[5]) return;
            Example_6();
        }
        else
        {
            if (!examples[6]) return;
            SetTitle("Distance", 11);
            Example_7();

            if (!examples[7]) return;
            Example_8();

            if (!examples[8]) return;
            Example_9();

            if (!examples[9]) return;
            Example_10();

            if (!examples[10]) return;
            Example_11();

            if (!examples[11]) return;
            SetTitle("Direction", 13);
            Example_12();

            if (!examples[12]) return;
            Example_13();
        }
    }

    private void Example_13()
    {
        Labeling(object1.position + Vector3.up + new Vector3(0, 0.4f), "That's the Direction");
        Labeling(object1.position + Vector3.up, "Normalizing a Vector is transforming it to a Lenght equal to 1");
        Labeling(object2.position, "That's the Direction: newPosition.normalized");
        Gizmos.DrawLine(object2.position, (object2.position + newPosition.normalized));
    }

    private void Example_12()
    {
        if (currentPage > 12) return;

        Labeling(object1.position + Vector3.up + new Vector3(0, 0.4f), "Third: Knowing the direction");
        Labeling(object1.position + Vector3.up, "of another Vector by normalizing the distance");
        Labeling(object2.position, "This is the distance");
        Gizmos.DrawLine(object2.position, object2.position + newPosition);
    }

    private void Example_11()
    {
        if (currentPage > 11) return;

        Labeling(object1.position + Vector3.up + new Vector3(0, 0.4f), "Second: Knowing how this Vector would behave");
        Labeling(object1.position + Vector3.up, "if placed into another Vector");
        Gizmos.DrawLine(Vector3.zero, Vector3.zero + newPosition);
    }

    private void Example_10()
    {
        if (currentPage > 10) return;

        Labeling(object1.position + Vector3.up + new Vector3(0, 0.4f), "It has 3 common uses");
        Labeling(object1.position + Vector3.up, "First: Knowing how close Vector B is from VectorA");
        Gizmos.DrawLine(object2.position, object2.position + newPosition);
    }

    private void Example_9()
    {
        if (currentPage > 9) return;

        Gizmos.DrawSphere(object1.position + (-object2.position), 0.2f);
        Gizmos.DrawLine(newPosition, Vector3.zero);
        Gizmos.DrawLine(object2.position, object2.position + newPosition);
        Gizmos.DrawSphere(object1.position + (-object2.position), 0.2f);
        Gizmos.DrawLine(object1.position, object1.position + (-object2.position));
        Labeling(object1.position + Vector3.up + new Vector3(0, 0.4f), "When you see the formula: Vector1 - Vector2");
        Labeling(object1.position + Vector3.up, "Read: It's the distance from Vector2 to Vector1");
    }

    private void Example_8()
    {
        if (currentPage > 8) return;

        Gizmos.DrawSphere(object1.position + (-object2.position), 0.2f);
        Gizmos.DrawLine(newPosition, Vector3.zero);
        Gizmos.DrawLine(object2.position, object2.position + newPosition);
        Labeling(object2.position + (newPosition), "This is the new Vector placed on Object2");
        Labeling(object2.position + (newPosition) + new Vector3(0,-0.4f), "Object2.position + newPosition");      
    }

    private void Example_7()
    {
        DrawWorldSpaceBasisVectors();
        newPosition = object1.position + (-object2.position);

        if (currentPage > 7) return;

        Gizmos.DrawSphere(object1.position + (-object2.position), 0.2f);
        Gizmos.DrawLine(newPosition, Vector3.zero);
        Labeling(object1.position + (-object2.position), "Do you see this Vector is equal to the distance from Object2 to Object1 ?");
    }

    private void Example_6()
    {
        Gizmos.DrawSphere(object1.position + (-object2.position), 0.2f);
        newPosition = object1.position + (-object2.position);
        Gizmos.DrawLine(newPosition, Vector3.zero);
        Labeling(object1.position + (-object2.position), "A position is also a Vector :D");
    }

    private void Example_5()
    {
        if (currentPage > 5) return;

        newPosition = object1.position + (-object2.position);
        Gizmos.DrawSphere(object1.position + (-object2.position), 0.2f);
        Gizmos.DrawLine(object1.position, newPosition);
        Labeling(object1.position + (-object2.position), "The result is the new position, where is this sphere" + newPosition);
        Labeling(object1.position + (-object2.position) + new Vector3(0, -0.4f), "Lets call it newPosition (newPosition = object1.position - object2.position)");
    }

    private void Example_4()
    {
        if (currentPage > 4) return;

        Gizmos.DrawLine(object1.position, object1.position + (-object2.position));
        Labeling(Vector3.zero + (-object2.position), "-Object2");
        Gizmos.DrawLine(Vector3.zero, Vector3.zero + (-object2.position));
        Labeling(object1.position + (-object2.position), "This is -Object2 placed on Object1");
        Labeling(object1.position + (-object2.position + new Vector3(0,-0.4f)), "that's equal to: Object1 - Object2");
        Labeling(object1.position + (-object2.position + new Vector3(0,-0.8f)), "who could also be read as : Object1 + (- Object2)");
    }

    private void Example_3()
    {
        if (currentPage > 3) return;

        Gizmos.color = Color.cyan;
        Labeling(-object2.position + new Vector3(0,1.2f), "This is the negative of Object2");
        Labeling(Vector3.zero + (-object2.position), "-Object2");
        Gizmos.DrawLine(Vector3.zero, Vector3.zero + (-object2.position));
        Gizmos.DrawSphere(Vector3.zero + (-object2.position), 0.2f);      
    }

    private void Example_2()
    {
        Vector3 xOffset = new Vector3(0.2f, 0f, 0f);
        object1.gameObject.SetActive(true);
        DrawWorldSpaceBasisVectors();

        if (currentPage > 2) return;

        Labeling(object1.position + new Vector3(0.2f, 0.8f), "But is also a Vector");
        Gizmos.DrawRay(Vector3.zero, object1.position);
        object2.gameObject.SetActive(true);
            Labeling(object2.position + new Vector3(0.2f, 0.8f), "But is also a Vector");
        Gizmos.DrawRay(Vector3.zero, object2.position);
    }

    void Example_1()
    {
        Vector3 xOffset = new Vector3(0.2f, 0f, 0f);
        object1.gameObject.SetActive(true);
        Handles.Label(object1.position + xOffset, object1.gameObject.name + " " +object1.gameObject.transform.position);
        object2.gameObject.SetActive(true);
        Handles.Label(object2.position + xOffset, object2.gameObject.name + " " + object2.gameObject.transform.position);

        if (currentPage > 1) return;

        Labeling(object1.position + Vector3.up, "This is just a object with a position");
        Labeling(object2.position + Vector3.up, "This is another a object with a position");
    }



    private void Labeling(Vector3 position, string message)
    {
        Handles.Label(position + new Vector3(0f, -0.5f, 0f), message);
    }

    void DrawWorldSpaceBasisVectors()
    {
        Handles.Label(Vector3.zero, "World Space (0,0,0)");
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Vector3.zero, Vector3.up);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.zero, Vector3.right);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward);
    }
}
