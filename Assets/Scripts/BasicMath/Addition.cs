using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Addition : PagesAbstract
{
    public Transform object1;
    private Vector3 newPosition;

    [Header("+ newVector2")]
    public Vector3 newVector2;

    private void OnDrawGizmos()
    {
        if (examples.Length < 1) examples = new bool[20];
        SetTitle("Scaling By Float", 5);

        if (!examples[0]) return;
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
        SetTitle("Scaling By Float", 7);
        Example_6();

        if (!examples[6]) return;
        Example_7();
    }

    private void Example_7()
    {
        if (currentPage > 7) return;

        object1.gameObject.SetActive(false);
        Labeling(newPosition + (new Vector3(0,0.8f)), "But is also a new Vector :D");
        Gizmos.DrawSphere(newPosition, 0.2f);
        Gizmos.DrawLine(newPosition, Vector3.zero);
        DrawWorldSpaceBasisVectors();        
    }

    private void Example_6()
    {
        if (currentPage > 6) return;

        Gizmos.DrawSphere(newPosition, 0.2f);
        Labeling(newPosition + (new Vector3(0,1f)), "Remember: The result is a new Position" + newPosition);
        Labeling(object1.position + (new Vector3(0,1f)), "offset" + object1.position);
        Gizmos.DrawLine(object1.transform.position, newPosition);
    }

    private void Example_5()
    {
        if (currentPage > 5) return;


        Gizmos.DrawRay(Vector3.zero, object1.position);
        DrawBasisVector(object1);
        Labeling(object1.position - Vector3.zero, "Object1 is now the offset");
        Labeling(object1.position - (Vector3.zero - new Vector3(0, -0.2f)), "The formula here is Object1.transform.position + newVector2");
        Labeling(object1.position - (Vector3.zero - new Vector3(0, -0.4f)), "Move the new Vector using newVector2");
        Labeling(object1.position - (Vector3.zero - new Vector3(0, -0.8f)), "remember how to read: When you see (Vector + Vector), Remember: The first Vector is the offset");

        Gizmos.DrawSphere(newPosition, 0.2f);
        Labeling(newPosition + Vector3.up, "Offset + newVector2 = " + newPosition);
        Gizmos.DrawLine(object1.transform.position, newPosition);
        
        newPosition = object1.transform.position + newVector2;
    }

    private void Example_4()
    {
        if (currentPage > 4) return;

        Gizmos.DrawRay(Vector3.zero, object1.position);
        DrawWorldSpaceBasisVectors();
        Labeling(object1.position - Vector3.zero + new Vector3(0.5f, 0, 0), "When adding to Vectors together: Vector1 + Vector2");
        Labeling(object1.position - (Vector3.zero - new Vector3(0.5f, -0.4f)), "The left Vector from the addition is ALWAYS a offset, moving from (0,0,0) to the Vector1 position");
        Labeling(object1.position - (Vector3.zero - new Vector3(0.5f, -0.8f)), "Vector 1 is a new origin position, when adding another position, you are placing the new vector from the new origin Vector1");        
    }

    private void Example_3()
    {
        if (currentPage > 3) return;
        
            Gizmos.DrawRay(Vector3.zero, object1.position);
            DrawWorldSpaceBasisVectors();
            Labeling(object1.position - Vector3.zero + new Vector3(0.5f,0,0), "A Vector Is just a position related to another position, when going from one to another it also has a lenght and direction");
            Labeling(object1.position - (Vector3.zero - new Vector3(0.5f, -0.4f)), "A Vector / GameObject Position alone, is always a position from world space to the Vector");
            Labeling(object1.position - (Vector3.zero - new Vector3(0.5f, -0.8f)), "When Adding any object to your scene, behind the curtains is always worldspace(0,0,0) + GameObject.transform.position");
    }

    private void Example_2()
    {
        Vector3 xOffset = new Vector3(0.2f, 0f, 0f);
        object1.gameObject.SetActive(true);
        if (currentPage > 2) return;
        
        Handles.Label(object1.position + xOffset, object1.gameObject.name + object1.gameObject.transform.position);
        Labeling(object1.position + new Vector3(0.2f,0.8f), "But is also a Vector");
        Gizmos.DrawRay(Vector3.zero, object1.position);
    }

    void Example_1()
    {
        if (currentPage > 1) return;
        Vector3 xOffset = new Vector3(0.2f, 0f, 0f);
        object1.gameObject.SetActive(true);
        Handles.Label(object1.position + xOffset, object1.gameObject.name + object1.gameObject.transform.position);
        Labeling(object1.position + Vector3.up, "This is just a object with a position");
    }


    private void Labeling(Vector3 position, string message)
    {
        Handles.Label(position + new Vector3(0f, -0.5f, 0f), message);
    }

    void DrawBasisVector(Transform gameObject)
    {
        Vector3 xOffset = new Vector3(0.2f, 0f, 0f);
        Handles.Label(gameObject.position + xOffset, gameObject.name + gameObject.transform.position);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(gameObject.position, Vector3.up);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gameObject.position, Vector3.right);
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
