using System;
using UnityEditor;
using UnityEngine;

public class NormalizedByNormalized : PagesAbstract
{
    public GameObject ShadowGroup;
    public Transform MovingCube;

    [Header("The concept of Normalized")]
    [Range(0, 1)]
    public float scalar;

    private void OnDrawGizmos()
    {
        if (examples == null || examples.Length != 20) examples = new bool[20];

        ExamplesController();
        SetTitle("The concept of Normalized", 1, 3);
        if (examples[0] && currentPage == 1) Example_1();
        if (examples[1] && currentPage == 2) Example_2();
        if (examples[2] && currentPage == 3) Example_3();
        SetTitle("Visualizing the Vectors Together", 4, 6);
        if (examples[2] && currentPage == 4) Example_4();
        if (examples[2] && currentPage == 5) Example_5();
        if (examples[2] && currentPage == 6) Example_6();
        SetTitle("Projecting the \"Shadow\" ", 7, 9);
        if (examples[2] && currentPage == 7) Example_7();
        if (examples[2] && currentPage == 8) Example_8();
        if (examples[2] && currentPage == 9) Example_9();
        SetTitle("Resume", 10, 11);
        if (examples[2] && currentPage == 10) Example_10();
        if (examples[2] && currentPage == 11) Example_11();
        SetTitle("Conclusion: Test for yourself", 12, 12);
        if (examples[2] && currentPage == 12) Example_12();
    }

    private void Example_12()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle this red dot", guiStyle);
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        Vector3 vectorA = RenderToMouse();
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        float shadowScalar = Vector3.Dot(vectorA, vectorB);

        //IT WORKS BOTH WAYS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + vectorA, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + vectorB, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * shadowScalar);
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA * shadowScalar);

        //THE PROJECTION
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorA, new Vector3(3f, 1f, 0) + vectorB * shadowScalar);
        Gizmos.DrawLine(new Vector3(3f, 1, 0) + vectorB, new Vector3(3f, 1f, 0) + vectorA * shadowScalar);

        guiStyle.fontSize = 25;
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "Scalar = " + shadowScalar, guiStyle);

        //Rotating Cube
        Vector3 difference = (new Vector3(3f, 1f, 0) + vectorA) - MovingCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        MovingCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Chart Legend", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Magenta Line: Dot product Calculation", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);

    }

    private void Example_11()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "Resume: 2. Using the Scalar", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "float shadowScalar = Vector3.dot(VectorA, VectorB)", guiStyle);

        Vector3 vectorA = (new Vector3(1.6f, 2, 0)).normalized;
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        Gizmos.DrawRay(new Vector3(1f, 1f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(1f, 1f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.1f);
        Gizmos.DrawRay(new Vector3(1f, 1, 0), new Vector3(1.6f, 0, 0).normalized);
        Gizmos.DrawSphere(new Vector3(1f, 1, 0) + new Vector3(1.6f, 0, 0).normalized, 0.1f);

        Gizmos.DrawRay(new Vector3(4f, 1f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(4f, 1f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.1f);
        Gizmos.DrawRay(new Vector3(4f, 1, 0), new Vector3(1.6f, 0, 0).normalized);
        Gizmos.DrawSphere(new Vector3(4f, 1, 0) + new Vector3(1.6f, 0, 0).normalized, 0.1f);


        float shadowScalar = Vector3.Dot(vectorA, vectorB);

        Gizmos.color = Color.magenta;
        Vector3 vectorAProjection = new Vector3(1f, 1f, 0) + vectorA * shadowScalar;
        Vector3 vectorBProjection = new Vector3(1f, 1f, 0) + vectorB * shadowScalar;

        //THE SCALAR
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(4f, 1f, 0), vectorA * shadowScalar);
        Gizmos.DrawRay(new Vector3(1f, 1f, 0), vectorB * shadowScalar);

        Handles.Label(vectorBProjection + new Vector3(-1.5f, -0.2f, 0), "Vector3 NewVectorB = shadowScalar * VectorB");
        Handles.Label(new Vector3(4f, 0.8f, 0), "Vector3 NewVectorA = shadowScalar * VectorA");
    }

    private void Example_10()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 25;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.up * 3, "Resume: 1. Getting the Scalar", guiStyle);
        guiStyle.fontSize = 20;
        Handles.Label(Vector3.zero, " ", guiStyle);

        Gizmos.DrawRay(new Vector3(2.5f, 1f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(2.5f, 1f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
        Handles.Label(new Vector3(2f, 0.8f, 0), "Normalized VectorA");

        Gizmos.DrawRay(new Vector3(4, 1, 0), new Vector3(1.6f, 0, 0).normalized);
        Gizmos.DrawSphere(new Vector3(4, 1, 0) + new Vector3(1.6f, 0, 0).normalized, 0.2f);
        Handles.Label(new Vector3(4, 0.8f, 0), "Normalized VectorB");

        guiStyle.fontSize = 17;
        Handles.Label(new Vector3(-1.4f, 1.5f, 0), "float shadowScalar = Vector3.dot(", guiStyle);
        Handles.Label(new Vector3(3.5f, 1.5f, 0), ",", guiStyle);
        Handles.Label(new Vector3(5.5f, 1.5f, 0), ")", guiStyle);
    }

    private void Example_9()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "Dot product return the size of shadow as a Scalar", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(new Vector3(0.2f, 0.8f), "Using the scalar on the vector, you get the shadow vector", guiStyle);
        Handles.Label(new Vector3(0.2f, 0.4f), "Scalar * Vector", guiStyle);

        Vector3 vectorA = (new Vector3(1.6f, 2, 0)).normalized;
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        Gizmos.DrawRay(new Vector3(1f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(1f, 1f, 0) + vectorA, 0.1f);

        Gizmos.DrawRay(new Vector3(1f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(1f, 1, 0) + vectorB, 0.1f);


        float shadowScalar = Vector3.Dot(vectorA, vectorB);

        Gizmos.color = Color.magenta;
        Vector3 vectorAProjection = new Vector3(1f, 1f, 0) + vectorA * shadowScalar;
        Vector3 vectorBProjection = new Vector3(1f, 1f, 0) + vectorB * shadowScalar;

        Gizmos.DrawLine(new Vector3(1f, 1f, 0) + vectorA, new Vector3(1f, 1f, 0) + vectorB * shadowScalar);
        //Gizmos.DrawRay(vectorBProjection, vectorAProjection - vectorBProjection);

        Handles.Label(vectorAProjection + (vectorBProjection - vectorAProjection) / 3, "The Projection");

        //THE SCALAR
        Gizmos.color = Color.green;
        //Gizmos.DrawRay(new Vector3(1f, 1f, 0), vectorA * dotScalar);
        Gizmos.DrawRay(new Vector3(1f, 1f, 0), vectorB * shadowScalar);
        Handles.Label(vectorBProjection + new Vector3(-1.5f, 0, 0), "shadowScale * VectorB");

        MovingCube.transform.rotation = Quaternion.Euler(0,0,52);
    }

    private void Example_8()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "Dot Product is casting a shadow", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(new Vector3(0.2f, 0.8f), "You can always imagine the dot product", guiStyle);
        Handles.Label(new Vector3(0.2f, 0.4f), "As Casting shadow between two Vectors", guiStyle);
        Handles.Label(new Vector3(0.2f, 0f), "The result is the Size of the shadow", guiStyle);

        Vector3 vectorA = (new Vector3(1.6f, 2, 0)).normalized;
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        Gizmos.DrawRay(new Vector3(0f, 1f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(0f, 1f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.1f);

        Gizmos.DrawRay(new Vector3(0f, 1, 0), new Vector3(1.6f, 0, 0).normalized);
        Gizmos.DrawSphere(new Vector3(0f, 1, 0) + new Vector3(1.6f, 0, 0).normalized, 0.1f);

        float shadowScalar = Vector3.Dot(vectorA, vectorB);

        Gizmos.color = Color.magenta;
        Vector3 vectorAProjection = new Vector3(0f, 1f, 0) + vectorA * shadowScalar;
        Vector3 vectorBProjection = new Vector3(0f, 1f, 0) + vectorB * shadowScalar;

        Gizmos.DrawLine(new Vector3(0f, 1f, 0) + vectorA, new Vector3(0f, 1f, 0) + vectorB * shadowScalar);
        //Gizmos.DrawRay(vectorBProjection, vectorAProjection - vectorBProjection);

        Handles.Label(vectorAProjection + (vectorBProjection - vectorAProjection) / 3, "The Projection");

        //THE SCALAR
        Gizmos.color = Color.green;
        //Gizmos.DrawRay(new Vector3(1f, 1f, 0), vectorA * dotScalar);
        Gizmos.DrawRay(new Vector3(0f, 1f, 0), vectorB * shadowScalar);
        Handles.Label(vectorBProjection + new Vector3(-0.6f, 0, 0), "Size of Shadow");

        //IT WORKS BOTH WAYS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(3f, 1f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.1f);
        Gizmos.DrawRay(new Vector3(3f, 1, 0), new Vector3(1.6f, 0, 0).normalized);
        Gizmos.DrawSphere(new Vector3(3f, 1, 0) + new Vector3(1.6f, 0, 0).normalized, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorB * shadowScalar);
        Gizmos.DrawRay(new Vector3(3f, 1f, 0), vectorA * shadowScalar);

        //THE PROJECTION
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(3f, 1, 0) + new Vector3(1.6f, 2, 0).normalized, new Vector3(3f, 1f, 0) + vectorB * shadowScalar);
        Gizmos.DrawLine(new Vector3(3f, 1, 0) + new Vector3(1.6f, 0, 0).normalized, new Vector3(3f, 1f, 0) + vectorA * shadowScalar);

        Handles.Label(new Vector3(3f, 2, 0), "The projection is equal on Both sides");
    }

    private void Example_7()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "What Projection actually means?", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0,-0.4f), "(Hover the mouse over the scene)", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(new Vector3(0.2f, 0.8f), "The projection acts like a shadow, LITERALLY", guiStyle);
        Handles.Label(new Vector3(0.2f, 0.4f), "Dot Product is often used to calculate shadows", guiStyle);

        Vector3 vectorA = (new Vector3(1.6f, 2, 0)).normalized;
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        Gizmos.DrawRay(new Vector3(1f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(1f, 1f, 0) + vectorA, 0.1f);
        Handles.Label(new Vector3(1.3f, 1f, 0) + vectorA, "Normalized VectorA");

        Gizmos.DrawRay(new Vector3(1f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(1f, 1, 0) + vectorB, 0.1f);
        Handles.Label(new Vector3(1.3f, 1f, 0) + vectorB, "Normalized VectorB");

        float dotScalar = Vector3.Dot(vectorA, vectorB);

        Gizmos.color = Color.magenta;
        Vector3 vectorAProjection = new Vector3(1f, 1f, 0) + vectorA * dotScalar;
        Vector3 vectorBProjection = new Vector3(1f, 1f, 0) + vectorB * dotScalar;

        Gizmos.DrawLine(new Vector3(1f, 1f, 0) + vectorA, new Vector3(1f, 1f, 0) + vectorB * dotScalar);
        Handles.Label(vectorAProjection + (vectorBProjection - vectorAProjection) / 3, "The Projection");

    }

    private void Example_6()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "The Result of adding the together", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(new Vector3(0.2f, 0.8f), "Is to creating a projection of one vector into another", guiStyle);

        Vector3 vectorA = (new Vector3(1.6f, 2, 0)).normalized;
        Vector3 vectorB = (new Vector3(1.6f, 0, 0)).normalized;

        Gizmos.DrawRay(new Vector3(1f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(1f, 1f, 0) + vectorA, 0.2f);
        Handles.Label(new Vector3(1.3f, 1f, 0) + vectorA, "Normalized VectorA");

        Gizmos.DrawRay(new Vector3(1f, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(1f, 1, 0) + vectorB, 0.2f);
        Handles.Label(new Vector3(1.3f, 1f, 0) + vectorB, "Normalized VectorB");


        float dotScalar = Vector3.Dot(vectorA, vectorB);

        Gizmos.color = Color.magenta;
        Vector3 vectorAProjection = new Vector3(1f, 1f, 0) + vectorA * dotScalar;
        Vector3 vectorBProjection = new Vector3(1f, 1f, 0) + vectorB * dotScalar;

        Gizmos.DrawRay(vectorAProjection, vectorBProjection - vectorAProjection);
        Gizmos.DrawRay(vectorBProjection, vectorAProjection - vectorBProjection);

        Handles.Label(vectorAProjection + (vectorBProjection - vectorAProjection) / 3, "The Projection");
    }

    private void Example_5()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "The Dot Product compare the directions together", guiStyle);
        guiStyle.fontSize = 20;
        Handles.Label(Vector3.zero, " ", guiStyle);

        Gizmos.DrawRay(new Vector3(2, 1f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(2, 1f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
        Handles.Label(new Vector3(2, 0.8f, 0), "Normalized VectorA");

        Gizmos.DrawRay(new Vector3(5, 1, 0), new Vector3(1.6f, 0, 0).normalized);
        Gizmos.DrawSphere(new Vector3(5, 1, 0) + new Vector3(1.6f, 0, 0).normalized, 0.2f);
        Handles.Label(new Vector3(5, 0.8f, 0), "Normalized VectorB");

        guiStyle.fontSize = 30;
        Handles.Label(new Vector3(-1.5f, 1.5f, 0), "Vector3.dot(", guiStyle);
        Handles.Label(new Vector3(4.5f, 1.5f, 0), ",", guiStyle);
        Handles.Label(new Vector3(7.5f, 1.5f, 0), ")", guiStyle);
        Handles.Label(new Vector3(8f, 1.5f, 0), "->", guiStyle);

        Gizmos.DrawRay(new Vector3(9, 1f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(9, 1f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
        Gizmos.DrawRay(new Vector3(9, 1, 0), new Vector3(1.6f, 0, 0).normalized);
        Gizmos.DrawSphere(new Vector3(9, 1, 0) + new Vector3(1.6f, 0, 0).normalized, 0.2f);
    }

    private void Example_4()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "Imagine two normalized Vectors, anywhere they could be", guiStyle);
        guiStyle.fontSize = 20;
        Handles.Label(Vector3.zero, "Dot Product PROJECTS one Vector to another", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "They just need to be passed to the method: Vector3.dot(Vector1, Vector2)", guiStyle);

        Gizmos.DrawRay(new Vector3(0, 1f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(0, 1f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
        Handles.Label(new Vector3(0, 0.8f, 0), "Normalized VectorA");

        Gizmos.DrawRay(new Vector3(4, 2, 0), new Vector3(1.6f, 0, 0).normalized);
        Gizmos.DrawSphere(new Vector3(4, 2, 0) + new Vector3(1.6f, 0, 0).normalized, 0.2f);
        Handles.Label(new Vector3(4, 1.8f, 0), "Normalized VectorB");


    }

    private void Example_3()
    {
        Vector3 vectorA = new Vector3(1, 0, 0);
        Vector3 vectorB = new Vector3(2, 2, 0);
        Vector3 distAToB = vectorB - vectorA;
        Gizmos.DrawSphere(vectorA, 0.1f);
        Gizmos.DrawSphere(vectorB, 0.1f);
        Gizmos.DrawLine(vectorA, vectorA + distAToB);

        Gizmos.DrawLine(new Vector3(3, 0, 0), new Vector3(3, 0, 0) + distAToB.normalized);
        Gizmos.DrawSphere(new Vector3(3, 0, 0), 0.1f);
        Gizmos.DrawSphere(new Vector3(3, 0, 0) + distAToB, 0.1f);

        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;
        Handles.Label(vectorA + distAToB/2, "Distance", guiStyle);
        Handles.Label(new Vector3(3, 0, 0) + distAToB/2, "Direction", guiStyle);

        Handles.Label(Vector3.up * 3, "Normalized Vector loose distance information", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.zero, "But retains direction value with a lenght of 1", guiStyle);
        Handles.Label(-Vector3.up + new Vector3(0, -0.6f), "Is important to memorize: Normalized Vectors are all equal in lenght");
        Handles.Label(-Vector3.up + new Vector3(0, -1f), "Distance or lenght is not important anymore");
        Handles.Label(-Vector3.up + new Vector3(0, -1.4f), "What is important though, is they hold a direction");
    }

    private void Example_2()
    {

        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 35;
        Handles.Label(Vector3.up * 3, "Same Vectors now Normalized", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.zero, "They are equal in Lenght (Magnitude of 1)", guiStyle);

        Gizmos.DrawRay(new Vector3(2, -0.3f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(2, -0.3f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
        Gizmos.DrawRay(new Vector3(1, -0.1f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(1, -0.1f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
        Gizmos.DrawRay(new Vector3(3, 0f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(3, 0f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
        Gizmos.DrawRay(new Vector3(2.6f, 0.1f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(2.6f, 0.1f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
        Gizmos.DrawRay(new Vector3(4.5f, 0.3f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(4.5f, 0.3f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
        Gizmos.DrawRay(new Vector3(3.3f, 0.2f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(3.3f, 0.2f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
        Gizmos.DrawRay(new Vector3(4.1f, 0.4f, 0), new Vector3(1.6f, 2, 0).normalized);
        Gizmos.DrawSphere(new Vector3(4.1f, 0.4f, 0) + new Vector3(1.6f, 2, 0).normalized, 0.2f);
    }

    private void Example_1()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 35;
        Handles.Label(Vector3.up * 3, "Some random Vectors", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.zero, "The have random Lenght (Magnitude)", guiStyle);

        Gizmos.DrawRay(new Vector3(2, -0.3f, 0), new Vector3(1.6f, 2, 0));
        Gizmos.DrawSphere(new Vector3(2, -0.3f, 0) + new Vector3(1.6f, 2, 0), 0.2f);
        Gizmos.DrawRay(new Vector3(1, -0.1f, 0), new Vector3(1.6f, 2, 0));
        Gizmos.DrawSphere(new Vector3(1, -0.1f, 0) + new Vector3(1.6f, 2, 0), 0.2f);
        Gizmos.DrawRay(new Vector3(3, 0f, 0), new Vector3(1.6f, 2, 0));
        Gizmos.DrawSphere(new Vector3(3, 0f, 0) + new Vector3(1.6f, 2, 0), 0.2f);
        Gizmos.DrawRay(new Vector3(2.6f, 0.1f, 0), new Vector3(1.6f, 2, 0));
        Gizmos.DrawSphere(new Vector3(2.6f, 0.1f, 0) + new Vector3(1.6f, 2, 0), 0.2f);
        Gizmos.DrawRay(new Vector3(4.5f, 0.3f, 0), new Vector3(1.6f, 2, 0));
        Gizmos.DrawSphere(new Vector3(4.5f, 0.3f, 0) + new Vector3(1.6f, 2, 0), 0.2f);
        Gizmos.DrawRay(new Vector3(3.3f, 0.2f, 0), new Vector3(1.6f, 2, 0));
        Gizmos.DrawSphere(new Vector3(3.3f, 0.2f, 0) + new Vector3(1.6f, 2, 0), 0.2f);
        Gizmos.DrawRay(new Vector3(4.1f, 0.4f, 0), new Vector3(1.6f, 2, 0));
        Gizmos.DrawSphere(new Vector3(4.1f, 0.4f, 0) + new Vector3(1.6f, 2, 0), 0.2f);
    }

    private void ExamplesController()
    {
        if (currentPage > 6 && currentPage < 10 || currentPage == 12) ShadowGroup.gameObject.SetActive(true);
        else ShadowGroup.gameObject.SetActive(false);
    }
}
