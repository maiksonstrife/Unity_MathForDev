using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DirectionAndAngles : PagesAbstract
{
    public GameObject shadowGroup;
    public Transform pivotCube;
    public Transform reconDrone;
    public Transform playerShip;
    [Header("Using DotProduct Scalar to detect Direction")]
    [Range(-1, 1)]
    public int scalar;
    [Header("Select Angle (Page 5)")]
    [Range(0, 180)]
    public int Angle;


    private GUIStyle guiStyle;
    private Vector3 pivotPoint;
    private Vector3 vectorA;
    private Vector3 vectorB;
    private float shadowScalar;
    private float Cosine;
    private const float TAU = 6.28318530718f;

    private void OnDrawGizmos()
    {
        if (examples == null || examples.Length != 12) examples = new bool[12];
        guiStyle = new GUIStyle();
        ExamplesController();
        SetTitle("Using Dot Product scalar as a Direction", 1, 2);
        if (examples[0] && currentPage == 1) Example_1();
        if (examples[1] && currentPage == 2) Example_2();
        SetTitle("Interpreting Direction as an Angle", 3, 3);
        if (examples[2] && currentPage == 3) Example_3();
        SetTitle("Using dot product as a Angle to detect Player", 4, 8);
        if (examples[3] && currentPage == 4) Example_4();
        if (examples[4] && currentPage == 5) Example_5();
        if (examples[5] && currentPage == 6) Example_6();
        if (examples[6] && currentPage == 7) Example_7();
        if (examples[7] && currentPage == 8) Example_8();
        if (examples[8] && currentPage == 9) Example_9();
    }

    private void Example_9()
    {
        
    }

    private void Example_8()
    {
        
    }

    private void Example_7()
    {
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        //DRAW CONTROLLER
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle this red dot", guiStyle);
        Handles.Label(Vector3.zero * 2 + new Vector3(0,-0.3f,0), "(You can change Angle in Slider Angle)", guiStyle);
        //ShipMovement
        playerShip.position = pivotPoint + vectorA;

        guiStyle.fontStyle = FontStyle.Bold;

        //DRAW ANGLE
        Vector3 pivotAngle = new Vector3(3, 1, 0);
        Gizmos.DrawLine(pivotAngle, pivotAngle + Vector3.right);
        Vector3 angleMeasure = Vector3.right;
        Vector3 rot = Quaternion.Euler(0, 0, Angle) * angleMeasure;
        Vector3 invRot = new Vector3(rot.x, -rot.y, 0);
        Gizmos.DrawSphere(pivotAngle + rot*2, 0.1f);
        Gizmos.DrawLine(pivotAngle, pivotAngle + rot*2);
        Gizmos.DrawLine(pivotAngle, pivotAngle + invRot * 2);
        Gizmos.DrawSphere(pivotAngle + invRot * 2, 0.1f);


        //DRAW DOT GIZMO
        DrawDotGizmo(false, false);

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "Mathf.Cos(radian), A Angle was chosen : " + Angle + " , and Mathf.Cos() accepts radians as argument" , guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "angle * Mathf.Deg2Rad, This formula converts Angle to Radians", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "droneEyeSight = Mathf.Cos(angle * Mathf.Deg2Rad)", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.9f), " ", guiStyle);

        //DETECTING FUNCTION
        if (PlayerDetection(Angle)) Handles.Label(new Vector3(4.5f, 1f, 0), "PLAYER INSIDE DRONE EYESIGHT :  PLAYER DETECTED", guiStyle);
           else Handles.Label(new Vector3(4.5f, 1f, 0), " ... ", guiStyle);

        //Drawing Formula
        guiStyle.fontSize = 17;
        Handles.Label(new Vector3(0f, -1f, 0), "public float angle;", guiStyle);
        Handles.Label(new Vector3(0f, -1.3f, 0), "Vector3 playerDirection = playerShip.position - reconDrone.position;", guiStyle);
        Handles.Label(new Vector3(0f, -1.6f, 0), "float dotDroneEyeSight = Mathf.Cos(angle * Mathf.Deg2Rad)", guiStyle);
        Handles.Label(new Vector3(0f, -1.9f, 0), "float dotDronePlayerDir = Vector3.Dot(droneFoward, playerDirection);", guiStyle);
        Handles.Label(new Vector3(0f, -2.2f, 0), "if (dotDronePlayerDir > dotDroneEyeSight)", guiStyle);
        Handles.Label(new Vector3(0f, -2.5f, 0), "PLAYER INSIDE DRONE EYESIGHT :  PLAYER DETECTED", guiStyle);


        bool PlayerDetection(float angle)
        {
            Vector3 playerDirection = playerShip.position - reconDrone.position;
            Vector3 droneFoward = reconDrone.transform.right;
            float dotDroneEyeSight = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dotDronePlayerDir = Vector3.Dot(droneFoward, playerDirection);
            if (dotDronePlayerDir > dotDroneEyeSight) return true;
            else return false;
        }
    }

    private void Example_6()
    {
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        //DRAW CONTROLLER
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle this red dot", guiStyle);

        guiStyle.fontStyle = FontStyle.Bold;
        //Draw DOT-COSINE
        Handles.Label(new Vector3(2.4f, 2.6f, 0), "Dot Product", guiStyle);
        Handles.Label(new Vector3(7.6f, 2.6f, 0), "Cosine", guiStyle);
        DrawDotGizmo(true, false);
        DrawCosineGizmo(new Vector3(8,1,0));
        Handles.Label(new Vector3(4f, 0f, 0), "Dot Product : " + shadowScalar, guiStyle);
        Handles.Label(new Vector3(7.5f, 0f, 0), "Is equal to ", guiStyle);
        Handles.Label(new Vector3(9f, 0f, 0), "Cosine : " + Cosine, guiStyle);
        //BOTTOM TEXT
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Is totally corret to say (DotProduct == Cosine)", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "If dot product is working with two Normalized Vectors", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "than Cosine retrives the same value:", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "float dotScalar = Mathf.Cos(radians)", guiStyle);
    }

    private void Example_5()
    {
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;

        Vector3 pivotAngle = new Vector3(4, 0, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivotAngle, pivotAngle + Vector3.right * 3);
        Gizmos.color = Color.green;
        Vector3 angleMeasure = Vector3.right * 3;
        Vector3 rot = Quaternion.Euler(0, 0, Angle) * angleMeasure;
        Gizmos.DrawLine(pivotAngle, pivotAngle + rot);

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "Using if (dot > someScalar) works perfectly fine, But is kind hard to think about an angle using dot", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "7.0f creates an angle as seen, that's fine, but what is the angle of 0.63f? 0.22f?", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "There's a longer method, using angle as a argument, and transforming it into a dot Product", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.9f), "SELECT ANGLE : MOVE SLIDER ANGLE", guiStyle);

        

        //RIGHT TEXT
        Handles.Label(new Vector3(4.5f, 1f, 0), "Angle : " + Angle, guiStyle);

        //Drawing Formula
        guiStyle.fontSize = 17;
        Handles.Label(new Vector3(0f, -1f, 0), "A given angle has two Vectors, we can use this information to get a DotProduct", guiStyle);
        Handles.Label(new Vector3(0f, -1.3f, 0), "The problem is: We cannot get DotProduct using angle as argument", guiStyle);
        Handles.Label(new Vector3(0f, -1.6f, 0), "But theres a solution: Cosine uses angle as argument", guiStyle);
        Handles.Label(new Vector3(0f, -1.9f, 0), "And Cosine can be equal to dotproduct, if the Vectors are normalized", guiStyle);
    }

    private void Example_4()
    {
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        DrawDotGizmo(false, false);
        Gizmos.DrawLine(pivotPoint, pivotPoint + DotReflection(vectorA));
        Gizmos.DrawSphere(pivotPoint + DotReflection(vectorA), 0.1f);

        for (int i = 1; i <= 8; i++)
        {
            float interpolation = (float)i / 8;
            float angRad = interpolation * TAU;
            float x = Mathf.Cos(angRad);
            float y = Mathf.Sin(angRad);
            float dotScalar = Mathf.Cos(angRad);
            if (Mathf.Approximately(dotScalar, -4.371139E-08f)) dotScalar = 0;
            else if (Mathf.Approximately(dotScalar, 1.192488E-08f)) dotScalar = 0;
            Vector3 point = pivotPoint + new Vector3(x, y);
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(point, 0.1f);

            if (i == 8)
                Handles.Label(point + new Vector3(0.1f, 0.1f, 0), " " + dotScalar);
            else if (i < 8) Handles.Label(point + new Vector3(0.1f, -0.1f, 0), "" + dotScalar);
        }

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "B) The drone casts a Vector from his eye : droneFoward = reconDrone.transform.right", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "C) playerDirection = playerDirection : playerShip.position - reconDrone.position;", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "D) dotDronePlayerDir = Vector3.dot(droneEye, playerDirection) ", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.9f), "", guiStyle);

        //DRAW CONTROLLER
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle this red dot", guiStyle);

        //CASTS VECTOR FROM DRONE FOWARD
        Vector3 droneFoward = reconDrone.transform.right;
        Gizmos.DrawLine(reconDrone.position, reconDrone.position + droneFoward);

        //Position Player Ship
        playerShip.position = pivotPoint + vectorA;
        Vector3 playerDirection = playerShip.position - reconDrone.position;
        float dotDronePlayerDir = Vector3.Dot(droneFoward, playerDirection);

        //RIGHT TEXT
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "dotDronePlayerDir = " + dotDronePlayerDir, guiStyle);
        Handles.Label(new Vector3(4.5f, 1f, 0), "if (dotDronePlayerDir > 0.7f) -> DETECTED", guiStyle);
        if(dotDronePlayerDir > 0.7f) Handles.Label(new Vector3(4.5f, 0.5f, 0), "PLAYER DETECTED", guiStyle);
        else Handles.Label(new Vector3(4.5f, 0.5f, 0), " ... ", guiStyle);

        //Drawing Formula
        guiStyle.fontSize = 17;
        Handles.Label(new Vector3(0f, -1f, 0), "float shadowScalar = Vector3.dot(", guiStyle);
        Gizmos.DrawRay(new Vector3(3.8f, -1.2f, 0), droneFoward);
        Handles.Label(new Vector3(5f, -1f, 0), ",", guiStyle);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(5.1f, -1.2f, 0), vectorA);
        Handles.Label(new Vector3(6f, -1f, 0), ")", guiStyle);

        //Chart Legend
        //guiStyle.fontSize = 20;
        //guiStyle.fontStyle = FontStyle.Bold;
        //Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "We can sayr for exemple", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Magenta Line: Dot product Calculation", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);
    }

    private void Example_3()
    {
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        DrawDotGizmo(false, false);
        Gizmos.DrawLine(pivotPoint, pivotPoint + DotReflection(vectorA));
        Gizmos.DrawSphere(pivotPoint + DotReflection(vectorA), 0.1f);

        for (int i = 1; i <= 8; i++)
        {
            float interpolation = (float) i / 8;
            float angRad =  interpolation * TAU;
            float x = Mathf.Cos(angRad);
            float y = Mathf.Sin(angRad);
            float dotScalar = Mathf.Cos(angRad);
            if (Mathf.Approximately(dotScalar, -4.371139E-08f)) dotScalar = 0;
            else if (Mathf.Approximately(dotScalar, 1.192488E-08f)) dotScalar = 0;
            Vector3 point = pivotPoint + new Vector3(x, y);
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(point, 0.1f);

            if (i == 8)
                Handles.Label(point + new Vector3(0.1f, 0.1f, 0), " " + dotScalar);
            else if (i < 8) Handles.Label(point + new Vector3(0.1f, -0.1f, 0), "" + dotScalar);
        }

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "Top and down, share the same values", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "half Right goes from 0.0 to 1, half left goes from 0.0 to -1", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.8f), "SLIDER -> Scalar, to change VectorA Direction", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -1.2f), "", guiStyle);

        //DRAW CONTROLLER
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle this red dot", guiStyle);

        //RIGHT TEXT
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "Scalar = " + shadowScalar, guiStyle);
        Handles.Label(new Vector3(4.5f, 1f, 0), "0.7f creates an angle both 0.7 to top", guiStyle);
        Handles.Label(new Vector3(4.5f, 0.5f, 0), "and 0.7f to down", guiStyle);

        //Chart Legend
        //guiStyle.fontSize = 20;
        //guiStyle.fontStyle = FontStyle.Bold;
        //Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "We can sayr for exemple", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Magenta Line: Dot product Calculation", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);
    }

    private void Example_2()
    {
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        DrawDotGizmo(true, true);


        //TOP TEXT
        Handles.Label(Vector3.up * 3, "Move VectorA by yourself and pay attention to the scalar result", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "and the POSITIVE and NEGATIVE values", guiStyle);
        //Handles.Label(Vector3.up * 3 + new Vector3(0, -0.8f), "SLIDER -> Scalar, to change VectorA Direction", guiStyle);

        //DRAW CONTROLLER
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle this red dot", guiStyle);

        //RIGHT TEXT
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "Scalar = " + shadowScalar, guiStyle);
        if (Mathf.Sign(shadowScalar) == -1) Handles.Label(new Vector3(4.5f, 1f, 0), "NEGATIVE: Towards OPPOSED direction", guiStyle);
        else if (Mathf.Sign(shadowScalar) == 1) Handles.Label(new Vector3(4.5f, 1f, 0), "POSITIVE: Towards SAME direction", guiStyle);
        //Chart Legend
        //guiStyle.fontSize = 20;
        //guiStyle.fontStyle = FontStyle.Bold;
        //Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Chart Legend", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Magenta Line: Dot product Calculation", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);
    }

    private void Example_1()
    {
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        DrawDotGizmo(true, true);

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "U", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "One of them, is to determine how close two directions are", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "SLIDER -> Scalar, to change VectorA Direction", guiStyle);

        //RIGHT TEXT
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "Scalar = " + shadowScalar, guiStyle);
        if (shadowScalar == -1) Handles.Label(new Vector3(4.5f, 1f, 0), "-1 they are opposed to each other", guiStyle);
        else if (shadowScalar == 0) Handles.Label(new Vector3(4.5f, 1f, 0), "0 they are perpendicular to each other", guiStyle);
        else Handles.Label(new Vector3(4.5f, 1f, 0), "1 They are pointing the same direction", guiStyle);

        //DRAW CONTROLLER
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(Vector3.zero, 0.1f); 
        //Handles.Label(Vector3.zero * 2, "Click + Hold, circle this red dot", guiStyle);

        //Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "From the perpendicular point the value is 0.0f", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "When they are goin to the same direction, value goes towards 1", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "When they are goin to the opposite direction, valu goes towards -1", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);
    }

    private void DrawCosineGizmo(Vector3 pivot)
    {
        //DRAWING THE VECTORS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(pivot, vectorA);
        Gizmos.DrawSphere(pivot + vectorA, 0.1f);
        Gizmos.DrawRay(pivot, vectorB);
        Gizmos.DrawSphere(pivot + vectorB, 0.1f);
        //DRAWING RADIUS
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(pivot, 1);
        Gizmos.DrawWireSphere(pivot, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(pivot + new Vector3(0, -1, 0), Vector3.up * 2);
        Handles.Label(pivot + new Vector3(0, 1.2f, 0), "Y lenght 1");
        Gizmos.color = Color.red;
        Gizmos.DrawRay(pivot + new Vector3(-1, 0, 0), Vector3.right * 2);
        Handles.Label(pivot + new Vector3(1.2f, 0, 0), "X lenght 1");
        //GET COMPONENTS
        float dotScalar = Vector3.Dot(vectorA, vectorB);
        float angle = Vector2.Angle(vectorA, vectorB);
        float convertedToRadians = angle * Mathf.Deg2Rad;
        Cosine = Mathf.Cos(convertedToRadians);

        //SCALAR APPLY
        Gizmos.color = Color.green;
        Gizmos.DrawRay(pivot, vectorB * shadowScalar);
        //COSINE / DOT PRODUCT
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivot + vectorA, pivot + vectorB * shadowScalar);
    }

    private void DrawDotGizmo(bool drawProjection, bool drawScalars)
    {
        //GET COMPONENTS
        pivotPoint = new Vector3(3f, 1f, 0);
        vectorB = (new Vector3(1f, 0, 0));
        if (currentPage == 1)
        {
            if (scalar == -1) vectorA = new Vector3(-1, 0, 0);
            else if (scalar == 0) vectorA = new Vector3(0, 1, 0);
            else vectorA = new Vector3(1, 0, 0);
        }
        else
        {
            vectorA = RenderToMouse().normalized;
        }
        shadowScalar = Vector3.Dot(vectorA, vectorB);

        Handles.DrawWireDisc(pivotPoint, Vector3.forward, 1);

        //DRAW VECTORS
        Gizmos.color = Color.green;
        Gizmos.DrawRay(pivotPoint, vectorA);
        Gizmos.DrawSphere(pivotPoint + vectorA, 0.1f);
        Gizmos.DrawRay(pivotPoint, vectorB);
        Gizmos.DrawSphere(pivotPoint + vectorB, 0.1f);

        //DRAW SCALARS SHADOW
        if (drawScalars)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(pivotPoint, vectorB * shadowScalar);
            Gizmos.DrawRay(pivotPoint, vectorA * shadowScalar);
        }

        //ROTATE CUBE
        Vector3 difference = (pivotPoint + vectorA) - pivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        pivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (!drawProjection) return;
        //DRAW PROJECTION
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivotPoint + vectorA, pivotPoint + vectorB * shadowScalar);
        Gizmos.DrawLine(pivotPoint + vectorB, pivotPoint + vectorA * shadowScalar);
    }

    private Vector3 DotReflection(Vector3 vectorToReflect)
    {
        float dotScalar = Vector3.Dot(vectorToReflect, Vector3.right);
        Vector3 reflectedVector = Vector3.right - dotScalar * vectorToReflect;
        return reflectedVector.normalized;
    }

    private void ExamplesController()
    {
        if(currentPage == 4 || currentPage == 7)
        {
            playerShip.gameObject.SetActive(true);
            reconDrone.gameObject.SetActive(true);
        }
        else
        {
            playerShip.gameObject.SetActive(false);
            reconDrone.gameObject.SetActive(false);
        }
    }
}
