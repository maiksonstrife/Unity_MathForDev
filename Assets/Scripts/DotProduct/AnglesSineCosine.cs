using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnglesSineCosine : PagesAbstract
{
    public GameObject ShadowGroup;
    public Transform PivotCube;

    [Header("The concept of Normalized")]
    [Range(0, 1)]
    public float scalar;
    public float radius;

    private void OnDrawGizmos()
    {
        if (examples == null || examples.Length != 12) examples = new bool[12];

        ExamplesController();
        SetTitle("The Angle", 1, 4);
        if (examples[0] && currentPage == 1) Example_1();
        if (examples[1] && currentPage == 2) Example_2();
        if (examples[2] && currentPage == 3) Example_3();
        if (examples[3] && currentPage == 4) Example_4();
        SetTitle("Sine and Cosine", 5, 8);
        if (examples[4] && currentPage == 5) Example_5();
        if (examples[5] && currentPage == 6) Example_6();
        if (examples[6] && currentPage == 7) Example_7();
        if (examples[7] && currentPage == 8) Example_8();
        if (examples[8] && currentPage == 9) Example_9();
    }

    private void Example_9()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "Finally with X and Y component", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "we can create the vector from angle", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.8f), "", guiStyle);

        //RED DOT CONTROLLER
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle the red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        //THE VECTORS
        Vector3 vectorA = RenderToMouse();
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        //DRAWING THE VECTORS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + vectorA, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + vectorB, 0.1f);
        //DRAW RADIUS
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 1);
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3, 0, 0), Vector3.up * 2);
        Handles.Label(new Vector3(3.1f, 2, 0), "Y lenght 1");
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(2, 1, 0), Vector3.right * 2);
        Handles.Label(new Vector3(4.1f, 1, 0), "X lenght 1");

        //GET COMPONENTS
        float dotScalar = Vector3.Dot(vectorA, vectorB);
        float angle = Vector2.Angle(vectorA, vectorB);
        float convertedToRadians = angle * Mathf.Deg2Rad;
        float cosine = Mathf.Cos(convertedToRadians);
        float sine = Mathf.Sin(convertedToRadians);


        //SCALAR APPLY
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * dotScalar);

        //Getting SignedAngle
        float signedAngle = Vector2.SignedAngle(vectorB, vectorA);

        //COSINE / DOT PRODUCT / SINE
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + vectorB * dotScalar);
        if (signedAngle > 0)
            Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + Vector3.up * sine);
        else if (signedAngle <= 0)
            Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + Vector3.down * sine);

        if (signedAngle <= 0) signedAngle += 360;

        //DRAW COMPONENTS
        float drawToRadians = signedAngle * Mathf.Deg2Rad;
        float drawcosine = Mathf.Cos(drawToRadians);
        float drawsine = Mathf.Sin(drawToRadians);
        Vector3 VectorAngle = new Vector3(drawcosine, drawsine, 0);
        Gizmos.DrawLine(Vector3.zero, VectorAngle);
        Handles.Label(VectorAngle, "new Vector3(cosine, sine)", guiStyle);

        //MIDDLE RIGHT TEXT
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "", guiStyle);
        Handles.Label(new Vector3(4.5f, 1.2f, 0), "", guiStyle);

        //PIVOT CUBE
        Vector3 difference = (new Vector3(3f, 1f, 0) + vectorA) - PivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        PivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //BELLOW Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Angle: " + signedAngle + "º", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Cosine (X Value): " + cosine, guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Sine (Y Component): " + sine, guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Dot Product: " + dotScalar, guiStyle);


    }

    private void Example_8()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "To get Y component we Use Sine", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "Sine function takes Angle and Y Component as argument", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.8f), "", guiStyle);

        //RED DOT CONTROLLER
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle the red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        //THE VECTORS
        Vector3 vectorA = RenderToMouse();
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        //DRAWING THE VECTORS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + vectorA, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + vectorB, 0.1f);
        //DRAWING RADIUS
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 1);
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3, 0, 0), Vector3.up * 2);
        Handles.Label(new Vector3(3.1f, 2, 0), "Y lenght 1");
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(2, 1, 0), Vector3.right * 2);
        Handles.Label(new Vector3(4.1f, 1, 0), "X lenght 1");

        //GET COMPONENTS
        float dotScalar = Vector3.Dot(vectorA, vectorB);
        float angle = Vector2.Angle(vectorA, vectorB);
        float convertedToRadians = angle * Mathf.Deg2Rad;
        float cosine = Mathf.Cos(convertedToRadians);
        float sine = Mathf.Sin(convertedToRadians);

        //SCALAR APPLY
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * dotScalar);

        //Getting SignedAngle
        float signedAngle = Vector2.SignedAngle(vectorB, vectorA);

        //COSINE / DOT PRODUCT / SINE
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + vectorB * dotScalar);
        if (signedAngle > 0) 
            Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + Vector3.up * sine);
        else if (signedAngle <= 0) 
            Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + Vector3.down * sine);

        if (signedAngle <= 0) signedAngle += 360;


        //MIDDLE RIGHT TEXT
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "We can assume: Cosine = DotProduct", guiStyle);
        Handles.Label(new Vector3(4.5f, 1.2f, 0), "      (if both vectors is normalized)", guiStyle);

        //PIVOT CUBE
        Vector3 difference = (new Vector3(3f, 1f, 0) + vectorA) - PivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        PivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //BELLOW Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Angle: " + signedAngle + "º", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Cosine (X Component): " + cosine, guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Sine (Y Component): " + sine, guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Dot Product: " + dotScalar, guiStyle);
    }

    private void Example_7()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "Cosine looks Similar to the Dot Product, that's because the result is the same", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "if Dot product is working with two normalized Vectors", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.8f), "it will ends with same               properties as angles", guiStyle);

        //RED DOT CONTROLLER
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle the red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        //THE VECTORS
        Vector3 vectorA = RenderToMouse();
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        //DRAWING THE VECTORS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + vectorA, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + vectorB, 0.1f);
        //DRAWING RADIUS
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 1);
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3, 0, 0), Vector3.up * 2);
        Handles.Label(new Vector3(3.1f, 2, 0), "Y lenght 1");
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(2, 1, 0), Vector3.right * 2);
        Handles.Label(new Vector3(4.1f, 1, 0), "X lenght 1");

        //GET COMPONENTS
        float dotScalar = Vector3.Dot(vectorA, vectorB);
        float angle = Vector2.Angle(vectorA, vectorB);
        float convertedToRadians = angle * Mathf.Deg2Rad;
        float Cosine = Mathf.Cos(convertedToRadians);

        //SCALAR APPLY
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * dotScalar);

        //COSINE / DOT PRODUCT
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + vectorB * dotScalar);

        float signedAngle = Vector2.SignedAngle(vectorB, vectorA);
        if (signedAngle <= 0) signedAngle += 360;

        //MIDDLE RIGHT TEXT
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "The projection will end exactly as Cosine", guiStyle);
        Handles.Label(new Vector3(4.5f, 1.2f, 0), " Dot Product == Cosine", guiStyle);
        Handles.Label(new Vector3(4.5f, 0.9f, 0), "     (if both vectors are normalized)", guiStyle);

        //PIVOT CUBE
        Vector3 difference = (new Vector3(3f, 1f, 0) + vectorA) - PivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        PivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //BELLOW Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Angle: " + signedAngle + "º", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Cosine (X Value): " + Cosine, guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Dot Product: " + dotScalar, guiStyle);
    }

    private void Example_6()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "Cosine Formula takes radians as argument, not angle, Cos(radians)", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "Radians is more eficient than angles both mathematically and computionality", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.8f), "we can simply convert angle to Radians using: angle * Mathf.Deg2Rad", guiStyle);

        //RED DOT CONTROLLER
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle the red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        //THE VECTORS
        Vector3 vectorA = RenderToMouse();
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        float shadowScalar = Vector3.Dot(vectorA, vectorB);

        //DRAWING THE VECTORS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + vectorA, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + vectorB, 0.1f);
        //DRAWING RADIUS
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 1);
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3, 0, 0), Vector3.up * 2);
        Handles.Label(new Vector3(3.1f, 2, 0), "Y lenght 1");
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(2, 1, 0), Vector3.right * 2);
        Handles.Label(new Vector3(4.1f, 1, 0), "X lenght 1");

        //GET COMPONENTS
        float dotScalar = Vector3.Dot(vectorA, vectorB);
        float angle = Vector2.Angle(vectorA, vectorB);
        float convertedToRadians = angle * Mathf.Deg2Rad;
        float Cosine = Mathf.Cos(convertedToRadians);

        //SCALAR APPLY
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * shadowScalar);

        //COSINE / DOT PRODUCT
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + vectorB * shadowScalar);

        float signedAngle = Vector2.SignedAngle(vectorB, vectorA);
        if (signedAngle <= 0) signedAngle += 360;
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "Angle = " + signedAngle + "º", guiStyle);

        //PIVOT CUBE
        Vector3 difference = (new Vector3(3f, 1f, 0) + vectorA) - PivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        PivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //BELLOW Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Cosine : " + Cosine, guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Angle: " + signedAngle, guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);

        //float angle = Mathf.Acos(shadowScalar) * Mathf.Rad2Deg;
    }

    private void Example_5()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "With the Angle components we can calculate the direction of the VectorA", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "To Get X Component we use COSINE", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.8f), "Cosine is a Function                using the X coordinate and the Angle", guiStyle);

        //RED DOT CONTROLLER
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle the red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        //THE VECTORS
        Vector3 vectorA = RenderToMouse();
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        float shadowScalar = Vector3.Dot(vectorA, vectorB);

        //DRAWING THE VECTORS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + vectorA, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + vectorB, 0.1f);
        //DRAWING RADIUS
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 1);
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3, 0, 0), Vector3.up * 2);
        Handles.Label(new Vector3(3.1f, 2, 0), "Y lenght 1");
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(2, 1, 0), Vector3.right * 2);
        Handles.Label(new Vector3(4.1f, 1, 0), "X lenght 1");

        //GET COMPONENTS
        float dotScalar = Vector3.Dot(vectorA, vectorB);
        float angle = Vector2.Angle(vectorA, vectorB);
        float convertedToRadians = angle * Mathf.Deg2Rad;
        float Cosine = Mathf.Cos(convertedToRadians);

        //SCALAR APPLY
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * shadowScalar);

        //COSINE / DOT PRODUCT
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + vectorB * shadowScalar);

        float signedAngle = Vector2.SignedAngle(vectorB, vectorA);
        if (signedAngle <= 0) signedAngle += 360;
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "Angle = " + signedAngle + "º", guiStyle);

        //PIVOT CUBE
        Vector3 difference = (new Vector3(3f, 1f, 0) + vectorA) - PivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        PivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //BELLOW Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Cosine : " + Cosine, guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Angle: " + signedAngle, guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);

        //float angle = Mathf.Acos(shadowScalar) * Mathf.Rad2Deg;
    }

    private void Example_4()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "Finally the Angle provides X and Y coordinates", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "With a Magnitude of 1", guiStyle);

        //RED DOT CONTROLLER
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle the red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        //THE VECTORS
        Vector3 vectorA = RenderToMouse();
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        float shadowScalar = Vector3.Dot(vectorA, vectorB);

        //DRAWING THE VECTORS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + vectorA, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + vectorB, 0.1f);

        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 1);
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3, 0, 0), Vector3.up * 2);
        Handles.Label(new Vector3(3.1f, 2, 0), "Y lenght 1");
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(2, 1, 0), Vector3.right * 2);
        Handles.Label(new Vector3(4.1f, 1, 0), "X lenght 1");

        //SCALAR APPLY
        Gizmos.color = Color.green;
        //Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * shadowScalar);

        //COSINE / DOT PRODUCT
        Gizmos.color = Color.magenta;
        //Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + vectorB * shadowScalar);

        float angle = Vector2.SignedAngle(vectorB, vectorA);
        if (angle <= 0) angle += 360;
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "Angle: " +angle, guiStyle);


        //PIVOT CUBE
        Vector3 difference = (new Vector3(3f, 1f, 0) + vectorA) - PivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        PivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //BELLOW Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Angle: " + angle, guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);

        //float angle = Mathf.Acos(shadowScalar) * Mathf.Rad2Deg;

    }

    private void Example_3()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "Also in a given angle there's a radius, the bigger Circle", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "Also with a Lenght of 1", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.8f), "And a origin point, the tinier circle", guiStyle);

        //RED DOT CONTROLLER
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle the red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        //THE VECTORS
        Vector3 vectorA = RenderToMouse();
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        float shadowScalar = Vector3.Dot(vectorA, vectorB);

        //DRAWING THE VECTORS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + vectorA, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + vectorB, 0.1f);

        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 1);
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 0.1f);

        //SCALAR APPLY
        Gizmos.color = Color.green;
        //Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * shadowScalar);

        //COSINE / DOT PRODUCT
        Gizmos.color = Color.magenta;
        //Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + vectorB * shadowScalar);

        float angle = Vector2.SignedAngle(vectorB, vectorA);
        if (angle <= 0) angle += 360;
        

        //PIVOT CUBE
        Vector3 difference = (new Vector3(3f, 1f, 0) + vectorA) - PivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        PivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //BELLOW Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Angle: " + angle, guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);

        //float angle = Mathf.Acos(shadowScalar) * Mathf.Rad2Deg;
    }

    private void Example_2()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "To get the angle we only need a direction value", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "What it means: Vectors with Lenght of 1", guiStyle);

        //RED DOT CONTROLLER
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle the red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        //THE VECTORS
        Vector3 vectorA = RenderToMouse();
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        float shadowScalar = Vector3.Dot(vectorA, vectorB);

        //DRAWING THE VECTORS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + vectorA, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + vectorB, 0.1f);
        Gizmos.DrawWireSphere(new Vector3(3f, 1f, 0), 1);

        //SCALAR APPLY
        Gizmos.color = Color.green;
        //Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * shadowScalar);

        //COSINE / DOT PRODUCT
        Gizmos.color = Color.magenta;
        //Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + vectorB * shadowScalar);

        float angle = Vector2.SignedAngle(vectorB, vectorA);
        if (angle <= 0) angle += 360;
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "Angle = " + angle + "º", guiStyle);

        //PIVOT CUBE
        Vector3 difference = (new Vector3(3f, 1f, 0) + vectorA) - PivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        PivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //BELLOW Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Angle: " + angle, guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);

        //float angle = Mathf.Acos(shadowScalar) * Mathf.Rad2Deg;
    }

    private void Example_1()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;

        //TOP TEXT
        Handles.Label(Vector3.up * 3, "We are getting Angle Between VectorA (moving) And VectorB (Stationary)", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "", guiStyle);

        //RED DOT CONTROLLER
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle the red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        //THE VECTORS
        Vector3 vectorA = RenderToMouse();
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        float shadowScalar = Vector3.Dot(vectorA, vectorB);

        //DRAWING THE VECTORS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + vectorA, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + vectorB, 0.1f);

        //SCALAR APPLY
        Gizmos.color = Color.green;
        //Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * shadowScalar);

        //COSINE / DOT PRODUCT
        Gizmos.color = Color.magenta;
        //Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + vectorB * shadowScalar);

        float angle = Vector2.SignedAngle(vectorB, vectorA);
        if (angle <= 0) angle += 360;
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "Angle = " + angle + "º", guiStyle);

        //PIVOT CUBE
        Vector3 difference = (new Vector3(3f, 1f, 0) + vectorA) - PivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        PivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //BELLOW Chart Legend
        //guiStyle.fontSize = 20;
        //guiStyle.fontStyle = FontStyle.Bold;
        //Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Chart Legend", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Magenta Line: Dot product Calculation", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        //Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);

        //float angle = Mathf.Acos(shadowScalar) * Mathf.Rad2Deg;
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Angle: " + angle, guiStyle);
    }

    private void ExamplesController()
    {

    }
}
