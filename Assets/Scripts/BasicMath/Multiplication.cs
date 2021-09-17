using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Multiplication : PagesAbstract
{
    public Transform object1;
    public Transform object2;

    [Header("Scaling by float")]
    public float scaleX;
    public float scaleY;

    [Header("Scaling by Vector")]
    public Vector3 scaleVector;

    [Header("Lock On Target")]
    [Range(0, 4.6f)]
    public float missile;
    [Header("Normalized Missile")]
    [Range(0, 4.6f)]
    public float normalizedMissile;

    [Header("Divided Missile")]
    [Range(0, 4.6f)]
    public float divideddMissile;


    private void OnDrawGizmos()
    {
        if (examples == null || examples.Length != 20) examples = new bool[20];
        SetTitle("Scaling By Float", 1, 6);

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
        Example_6();

        if (!examples[6]) return;
        SetTitle("World Space To Local Space", 7, 13);
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
        Example_12();

        if (!examples[12]) return;
        Example_13();

        if (!examples[13]) return;
        SetTitle("Scaling by Vector", 14, 14);
        Example_14();

        if (!examples[14]) return;
        SetTitle("Lock On Target", 15, 17);
        Example_15();

        if (!examples[15]) return;
        Example_16();

        if (!examples[16]) return;
        Example_17();

        if (!examples[17]) return;
        SetTitle("Normalizing Missile", 18, 20);
        Example_18();

        if (!examples[18]) return;
        Example_19();

        if (!examples[19]) return;
        Example_20();
    }

    private void Example_20()
    {
        if (currentPage > 20) return;
        Vector3 target = object2.position - object1.position;
        Handles.Label(-Vector3.up + new Vector3(0, -0.6f), "Use normalizedMissile to travel using normalized TargetDistance");
        Handles.Label(-Vector3.up + new Vector3(0, -1f), "Use Missile to travel using targetDistance");
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(object1.position, object1.position + target.normalized);
        Handles.Label(object1.position + target.normalized, "Normalized targetDistance");

        Vector3 scalarByDistance = missile * target;
        Vector3 scalarByDirection = normalizedMissile * target.normalized;
        Handles.Label(-Vector3.up + new Vector3(0, -1.4f), "Distance: missile * " + target + " = " + scalarByDistance);
        Handles.Label(-Vector3.up + new Vector3(0, -1.8f), "Normalized Distance: missile * " + target.normalized + " = " + scalarByDirection);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(object1.position + missile * target, 0.2f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(object1.position + normalizedMissile * scalarByDirection, 0.2f);
        Gizmos.DrawLine(object1.position, object1.position + normalizedMissile * scalarByDirection);


        if (normalizedMissile > 2 && normalizedMissile  < 2.2) Handles.Label((object1.position + normalizedMissile * scalarByDirection) + new Vector3(0,0.6f), "Double the Direction *2");
    }

    private void Example_19()
    {
        if (currentPage > 19) return;
        Vector3 target = object2.position - object1.position;
        Handles.Label(-Vector3.up + new Vector3(0, -0.6f), "If we normalize targetDistance, we get only the direction");
        Handles.Label(-Vector3.up + new Vector3(0, -1f), "That's only 1 unity lenght the targetDistance");
        Handles.Label(-Vector3.up + new Vector3(0, -1.4f), "That way the missile will travel 1 unit each * scalar");
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(object1.position, object1.position + target.normalized);
        Handles.Label(object1.position + target.normalized, "Normalized targetDistance");
    }

    private void Example_18()
    {
        if (currentPage > 18) return;
        Vector3 targetDistance = object2.position - object1.position;
        Gizmos.DrawLine(object1.position, object1.position + targetDistance);
        Handles.Label(-Vector3.up + new Vector3(0, -0.6f), "See how the missile moved super fast?");
        Handles.Label(-Vector3.up + new Vector3(0, -1f), "That's because the scalar missile is multiplying the entire target Position");
        Vector3 scalarByDistance = missile * targetDistance;
        Handles.Label(-Vector3.up + new Vector3(0, -1.4f), "missile * " + targetDistance + " = "+ scalarByDistance);
        Gizmos.DrawSphere(object1.position + missile * targetDistance, 0.2f);
    }

    private void Example_17()
    {
        if (currentPage > 17) return;
            Handles.Label(object1.position + new Vector3(0, -0.2f), "targetDistance = Object2.position - Object1.position");
            Handles.Label(object1.position + new Vector3(0, -0.6f), "offset = Object1.position");
            Handles.Label(object1.position + new Vector3(0, -1f), "scalar = missile");
            Handles.Label(object1.position + new Vector3(0, -1.4f), "offset + missile * targetDistance");
            Vector3 targetDistance = object2.position - object1.position;
            Gizmos.DrawSphere(object1.position + missile * targetDistance, 0.2f);
            Gizmos.DrawLine(object1.position, object1.position + targetDistance);
            Handles.Label(-Vector3.up + new Vector3(0,-0.6f), "Use Missile to move along the Path");
    }

    private void Example_16()
    {
        if (currentPage > 16) return;
            Handles.Label(object1.position + new Vector3(0, -0.2f), "We need the missile path: From player to target");
            Handles.Label(object1.position + new Vector3(0, -0.6f), "Let's use the Subtraction Distance formula");
            Handles.Label(object1.position + new Vector3(0, -1f), "target = Object2.position - Object1.position");
            Handles.Label(object1.position + new Vector3(0, -1.4f), "(the second vector is always the origin, the first is the destiny)");
            Vector3 target = object2.position - object1.position;
            Gizmos.DrawLine(object1.position, object1.position + target);

            Handles.Label(object1.position + new Vector3(0, 0.4f), "PLAYER" + object1.position);
            Handles.Label(object2.position + new Vector3(0, 0.4f), "TARGET" + target);
    }

    private void Example_15()
    {
        if (currentPage > 15) return;
            Handles.Label(object1.position + new Vector3(0, -0.2f), "This is the object1, imagine as the Player" + object1.position);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(object1.position, object1.right);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(object1.position, object1.up);

            Handles.Label(object2.position + new Vector3(0, -0.6f), "That's Object2, an enemy that we detected and put a Target on");
            Handles.Label(object2.position + new Vector3(0, -1f), "Let's shoot a missile at him!!!");
    }

    private void Example_14()
    {
        if (currentPage > 14) return;
            Gizmos.color = Color.blue;
            Vector3 offset = object1.position;
            Handles.Label(offset + new Vector3(0, -0.1f), "offset + object1.right * scaleVector.x + object1.up * scaleVector.y");

            Gizmos.color = Color.red;
            Gizmos.DrawRay(object1.position, object1.right);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(object1.position, object1.up);


            Vector3 target = object2.position - object1.position;
            Gizmos.DrawSphere(offset + object1.right * scaleVector.x + object1.up * scaleVector.y, 0.2f);

            Handles.Label(offset + new Vector3(0, -0.5f), "We can Bind the X and Y from scaleVector to the Basis Vectors");
            Handles.Label(offset + new Vector3(0, -0.9f), "Y axis = object1.up, scalar = scaleVector.y");
            Handles.Label(offset + new Vector3(0, -1.3f), "Use scale Vector to move along Local Vector X and Y");
    }

    private void Example_13()
    {
        if (currentPage > 13) return;
            Gizmos.color = Color.blue;
            Vector3 offset = object1.position;
            Handles.Label(offset + new Vector3(0, -0.1f), "offset + object1.right * scaleVector.x");

            Gizmos.color = Color.red;
            Gizmos.DrawRay(object1.position, object1.right);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(object1.position, object1.up);

            Gizmos.DrawSphere(offset + object1.right * scaleVector.x, 0.2f);

            Handles.Label(offset + new Vector3(0, -0.5f), "To move along any Axis, we apply a scalar along that axis");
            Handles.Label(offset + new Vector3(0, -0.9f), "axis = object1.right, scalar = scaleVector.x");
            Handles.Label(offset + new Vector3(0, -1.3f), "Use scale Vector to move along Local Vector X");
    }

    private void Example_12()
    {
        if (currentPage > 12) return;
            Gizmos.color = Color.blue;
            Vector3 offset = object1.position;
            Gizmos.DrawSphere(offset, 0.2f);
            Handles.Label(offset + new Vector3(0, -0.1f), "offset = object1.position");

            Gizmos.color = Color.red;
            Gizmos.DrawRay(object1.position, object1.right);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(object1.position, object1.up);
    }

    private void Example_11()
    {
        if (currentPage > 11) return;
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Vector3.zero, 0.2f);
            Handles.Label(Vector3.zero + new Vector3(0, -0.1f), "Imagine we have a object somewhere");
            Handles.Label(Vector3.zero + new Vector3(0, -0.4f), "we first must move to new position");
            Handles.Label(Vector3.zero + new Vector3(0, -0.8f), "Object1.position is the offset then");
    }

    private void Example_10()
    {
        if (currentPage > 10) return;
            Handles.Label(object1.position + new Vector3(0, -0.2f), "This is the object1 Local Position " + object1.position);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(object1.position, object1.right);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(object1.position, object1.up);

            Handles.Label(object1.position + new Vector3(0, -0.6f), "And Object1 Basis Vectors, the object is rotated +45 degress " + object1.position);
            Handles.Label(object1.position + new Vector3(0, -1f), "How to move along those axi's ?");
    }

    private void Example_9()
    {
        if (currentPage > 9) return;
            Handles.Label(object1.position + new Vector3(0, -0.2f), "This is the object1 Position " + object1.position);
    }

    private void Example_8()
    {
        if (currentPage > 8) return;
            Handles.Label(Vector3.zero + new Vector3(0, -0.2f), "Vector3.right and Vector3.up draws 2 Vectors, the Basis Vectors");
            Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Vector3.right is 1 unit to the right of position, and Vector3.up is a vector 1 unit up from the position");
            Handles.Label(Vector3.zero + new Vector3(0, -1f), "The Basis Vectors is a visual measure system, to known your coordinates and orientation");
            Gizmos.DrawSphere(Vector3.zero, 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Vector3.zero, Vector3.right);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(Vector3.zero, Vector3.up);
    }

    private void Example_7()
    {
        if (currentPage > 7) return;
            Handles.Label(Vector3.zero + new Vector3(0,-0.2f), "This is the World Position " + Vector3.zero);
            Gizmos.DrawSphere(Vector3.zero, 0.2f);
    }

    private void Example_6()
    {
        if (currentPage > 6) return;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Vector3.zero, Vector3.right);
            Handles.Label(Vector3.right + new Vector3(0, 0.4f), "Character Direction");
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Vector3.right * 1, 0.2f);

            Handles.Label(Vector3.right * 3 + new Vector3(0, -0.1f), "Character Speed");
            Gizmos.DrawSphere(Vector3.right * 3, 0.2f);


            Handles.Label(Vector3.zero + new Vector3(0, -0.4f), "(Vector3) Direction * (float) scale");
            Handles.Label(Vector3.zero + new Vector3(0, -0.8f), "Vector3 Direction = AnalogInput.normalized (1 unit lenght), float scale = 3");

            Handles.Label(Vector3.up + new Vector3(0, 0.4f), "A character is running as long as the player holds a direction on analog stick");
            Handles.Label(Vector3.up, "Direction is the analog input that's equal to 1, Speed is equal to two");
            Handles.Label(Vector3.right * 5 + new Vector3(0, 0.1f), " = The character will be moving 3 units per frame");
    }

    private void Example_5()
    {
        if (currentPage > 5) return;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Vector3.zero, Vector3.right);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(Vector3.zero, Vector3.up);
            Handles.Label(Vector3.zero + new Vector3(0, -0.1f), "Use scaleX to travel along Vector.right and ScaleY Vector.up");
            Handles.Label(Vector3.zero + new Vector3(0, -0.4f), "Vector3.right * scaleX + Vector3.up * scaleY");
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Vector3.right * scaleX + Vector3.up * scaleY, 0.2f);
            Handles.Label(Vector3.up, "you can even create a scalar by using multiply to move to a custom position");
    }

    private void Example_4()
    {
        if (currentPage > 4) return;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Vector3.zero, Vector3.right);
            Handles.Label(Vector3.zero + new Vector3(0, -0.1f), "Use scaleX to travel along Vector.right");
            Handles.Label(Vector3.zero + new Vector3(0, -0.4f), "public float scale, Vector3.right * scaleX");
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Vector3.right * scaleX, 0.2f);
            Handles.Label(Vector3.up, "Multiplying is moving trought a Scalar, the scalar is number that travels along the object opr Unity");
    }

    private void Example_3()
    {
        if (currentPage > 3) return;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Vector3.zero, Vector3.right);
            Handles.Label(Vector3.zero + new Vector3(0, -0.1f), "Thats a Sphere on a scale of half unit (*0.5) of the world position .right");
            Handles.Label(Vector3.zero + new Vector3(0, -0.4f), "float scale = 0.5f, Vector3.right * scale");
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Vector3.right *0.5f, 0.2f);
    }

    private void Example_2()
    {
        if (currentPage > 2) return;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Vector3.zero, Vector3.right);
            Handles.Label(Vector3.zero + new Vector3(0, -0.1f), "Thats a Sphere on a scale of 1 unit (*1) of the world position Vector3.right");
            Handles.Label(Vector3.zero + new Vector3(0, -0.4f), "float scale = 1f, Vector3.right * scale");
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Vector3.right * 1, 0.2f);
    }

    void Example_1()
    {
        if (currentPage > 1) return;
            Handles.Label(Vector3.zero + new Vector3(0, -0.2f), "This is World Space Origin (0,0,0)");
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Vector3.zero, Vector3.right);
            Handles.Label(Vector3.right + new Vector3(0, 0.2f, 0), "This is the World Space .right (1,0,0)");
    }
}
