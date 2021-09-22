using System;
using UnityEditor;
using UnityEngine;

public class NormalizedByNormalized : PagesAbstract
{
    public GameObject ShadowGroup;
    public Transform PivotCube;

    [Header("The concept of Normalized")]
    [Range(0, 1)]
    public float scalar;
    private Vector3 vectorA;
    private Vector3 vectorB;
    private float shadowScalar;
    private GUIStyle guiStyle;
    private Vector3 vectorAProjection;
    private Vector3 vectorBProjection;

    private void OnDrawGizmos()
    {
        if (examples == null || examples.Length != 12) examples = new bool[12];
        ExamplesController();
        Lessons();
    }

    public override void ExamplesController()
    {
        guiStyle = new GUIStyle();
        vectorA = (new Vector3(1.6f, 2, 0)).normalized;
        vectorB = (new Vector3(1.6f, 0, 0)).normalized;
        shadowScalar = Vector3.Dot(vectorA, vectorB);
        vectorAProjection = vectorA * shadowScalar;
        vectorBProjection = vectorB * shadowScalar;
        SetTitle("The concept of Normalized", 1, 3);
        SetTitle("Visualizing the Vectors Together", 4, 6);
        SetTitle("Projecting the \"Shadow\" ", 7, 9);
        SetTitle("Resume", 10, 11);
        SetTitle("Conclusion: Test for yourself", 12, 12);
        if (currentPage > 6 && currentPage < 10) ShadowGroup.gameObject.SetActive(true);
        if (currentPage == 7) PivotCube.transform.rotation = Quaternion.Euler(0, 0, 52);
        if (currentPage == 12) ShadowGroup.gameObject.SetActive(true);
        else ShadowGroup.gameObject.SetActive(false);
    }

    public override void Example_12()
    {
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.zero * 2, "Click + Hold, circle this red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        Vector3 pivot1 = new Vector3(3f, 1f, 0);

        Vector3 mouseVector = RenderToMouse();
        shadowScalar = Vector3.Dot(vectorB, mouseVector);

        //IT WORKS BOTH WAYS
        Gizmos.color = Color.white;
        Gizmos.DrawRay(pivot1, mouseVector);
        Gizmos.DrawSphere(pivot1 + mouseVector, 0.1f);
        Gizmos.DrawRay(pivot1, vectorB);
        Gizmos.DrawSphere(pivot1 + vectorB, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(pivot1, vectorB * shadowScalar);
        Gizmos.DrawRay(pivot1, mouseVector * shadowScalar);

        //THE PROJECTION
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivot1 + mouseVector, pivot1 + vectorB * shadowScalar);
        Gizmos.DrawLine(pivot1 + vectorB, pivot1 + mouseVector * shadowScalar);

        guiStyle.fontSize = 25;
        Handles.Label(new Vector3(4.5f, 1.5f, 0), "Scalar = " + shadowScalar, guiStyle);

        //Rotating Cube
        Vector3 difference = (pivot1 + mouseVector) - PivotCube.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        PivotCube.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //Chart Legend
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "Chart Legend", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.9f), "Magenta Line: Dot product Calculation", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.2f), "Scalar: Dot product calculus result", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -1.5f), "Green Line: Scalar * VectorA / Scalar * VectorB", guiStyle);

    }

    public override void Example_11()
    {
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "Resume: 2. Using the Scalar", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.4f), "float shadowScalar = Vector3.dot(VectorA, VectorB)", guiStyle);
        Vector3 pivot1 = new Vector3(1f, 1f, 0);
        Vector3 pivot2 = new Vector3(4f, 1f, 0);


        Gizmos.DrawRay(pivot1, vectorA);
        Gizmos.DrawSphere(pivot1 + vectorA, 0.1f);
        Gizmos.DrawRay(pivot1, vectorB);
        Gizmos.DrawSphere(pivot1 + vectorB, 0.1f);

        Gizmos.DrawRay(pivot2, vectorA);
        Gizmos.DrawSphere(pivot2 + vectorA, 0.1f);
        Gizmos.DrawRay(pivot2, vectorB);
        Gizmos.DrawSphere(pivot2 + vectorB, 0.1f);



        Gizmos.color = Color.green;
        Gizmos.DrawRay(pivot1, vectorB * shadowScalar);
        Gizmos.DrawRay(pivot2, vectorA * shadowScalar);

        Handles.Label(pivot1 + vectorBProjection + new Vector3(-1.5f, -0.2f, 0), "Vector3 NewVectorB = shadowScalar * VectorB");
        Handles.Label(new Vector3(4f, 0.8f, 0), "Vector3 NewVectorA = shadowScalar * VectorA");
    }

    public override void Example_10()
    {
        guiStyle.fontSize = 25;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(Vector3.up * 3, "Resume: 1. Getting the Scalar", guiStyle);
        guiStyle.fontSize = 20;
        Handles.Label(Vector3.zero, " ", guiStyle);

        Gizmos.DrawRay(new Vector3(2.5f, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(2.5f, 1f, 0) + vectorA, 0.2f);
        Handles.Label(new Vector3(2f, 0.8f, 0), "Normalized VectorA");

        Gizmos.DrawRay(new Vector3(4, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(4, 1, 0) + vectorB, 0.2f);
        Handles.Label(new Vector3(4, 0.8f, 0), "Normalized VectorB");

        guiStyle.fontSize = 17;
        Handles.Label(new Vector3(-1.4f, 1.5f, 0), "float shadowScalar = Vector3.dot(", guiStyle);
        Handles.Label(new Vector3(3.5f, 1.5f, 0), ",", guiStyle);
        Handles.Label(new Vector3(5.5f, 1.5f, 0), ")", guiStyle);
    }

    public override void Example_9()
    {
        Vector3 pivot = new Vector3(1f, 1f, 0);

        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "Dot product return the size of shadow as a Scalar", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(new Vector3(0.2f, 0.8f), "Using the scalar on the vector, you get the shadow vector", guiStyle);
        Handles.Label(new Vector3(0.2f, 0.4f), "Scalar * Vector", guiStyle);

        Gizmos.DrawRay(pivot, vectorA);
        Gizmos.DrawSphere(pivot + vectorA, 0.1f);

        Gizmos.DrawRay(pivot, vectorB);
        Gizmos.DrawSphere(pivot + vectorB, 0.1f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivot + vectorA, pivot + vectorB * shadowScalar);
        Handles.Label((pivot + vectorBProjection) + Vector3.up * 0.5f, "The Projection");

        //THE SCALAR
        Gizmos.color = Color.green;
        Gizmos.DrawRay(pivot, vectorB * shadowScalar);
        Handles.Label(vectorBProjection + new Vector3(-1.5f, 0, 0), "shadowScale * VectorB");

        PivotCube.transform.rotation = Quaternion.Euler(0,0,52);
    }

    public override void Example_8()
    {
        Vector3 pivot1 = new Vector3(0f, 1f, 0f);
        Vector3 pivot2 = new Vector3(3f, 1f, 0);
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "Dot Product is casting a shadow", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(new Vector3(0.2f, 0.8f), "You can always imagine the dot product", guiStyle);
        Handles.Label(new Vector3(0.2f, 0.4f), "As Casting shadow between two Vectors", guiStyle);
        Handles.Label(new Vector3(0.2f, 0f), "The result is the Size of the shadow", guiStyle);

        //Pivot1
        Gizmos.DrawRay(pivot1, vectorA);
        Gizmos.DrawSphere(pivot1 + vectorA, 0.1f);
        Gizmos.DrawRay(pivot1, vectorB);
        Gizmos.DrawSphere(pivot1 + vectorB, 0.1f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivot1 + vectorA, pivot1 + vectorB * shadowScalar);
        Handles.Label( (pivot1 + vectorBProjection) + Vector3.up * 0.5f, "The Projection");

        //THE SCALAR
        Gizmos.color = Color.green;
        Gizmos.DrawRay(pivot1, vectorB * shadowScalar);
        Handles.Label(pivot1 + vectorBProjection * 0.2f, "Size of Shadow");

        //Pivot 2
        Gizmos.color = Color.white;
        Gizmos.DrawRay(pivot2, vectorA);
        Gizmos.DrawSphere(pivot2 + vectorA, 0.1f);
        Gizmos.DrawRay(pivot2, vectorB);
        Gizmos.DrawSphere(pivot2 + vectorB, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(pivot2, vectorB * shadowScalar);
        Gizmos.DrawRay(pivot2, vectorA * shadowScalar);

        //PROJECTION
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivot2 + vectorA, pivot2 + vectorB * shadowScalar);
        Gizmos.DrawLine(pivot2 + vectorB, pivot2 + vectorA * shadowScalar);

        Handles.Label(new Vector3(3f, 2, 0), "The projection is equal on Both sides");
    }

    public override void Example_7()
    {
        Vector3 pivot = new Vector3(1f, 1f, 0);
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "What Projection actually means?", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0,-0.4f), "(Hover the mouse over the scene)", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(new Vector3(0.2f, 0.8f), "The projection acts like a shadow, LITERALLY", guiStyle);
        Handles.Label(new Vector3(0.2f, 0.4f), "Dot Product is often used to calculate shadows", guiStyle);

        Gizmos.DrawRay(pivot, vectorA);
        Gizmos.DrawSphere(pivot + vectorA, 0.1f);
        Handles.Label(new Vector3(1.3f, 1f, 0) + vectorA, "Normalized VectorA");

        Gizmos.DrawRay(pivot, vectorB);
        Gizmos.DrawSphere(pivot + vectorB, 0.1f);
        Handles.Label(new Vector3(1.3f, 1f, 0) + vectorB, "Normalized VectorB");


        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(1f, 1f, 0) + vectorA, new Vector3(1f, 1f, 0) + vectorB * shadowScalar);
        Handles.Label(vectorAProjection + (vectorBProjection - vectorAProjection) / 3, "The Projection");

    }

    public override void Example_6()
    {
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "The Result of adding the together", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(new Vector3(0.2f, 0.8f), "Is to creating a projection of one vector into another", guiStyle);

        Vector3 pivot = new Vector3(1f, 1f, 0);

        Gizmos.DrawRay(pivot, vectorA);
        Gizmos.DrawSphere(pivot + vectorA, 0.2f);
        Handles.Label(new Vector3(1.3f, 1f, 0) + vectorA, "Normalized VectorA");

        Gizmos.DrawRay(pivot, vectorB);
        Gizmos.DrawSphere(pivot + vectorB, 0.2f);
        Handles.Label(new Vector3(1.3f, 1f, 0) + vectorB, "Normalized VectorB");

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivot + vectorA, new Vector3(1f, 1f, 0) + vectorBProjection);

        Gizmos.DrawRay(pivot, vectorBProjection);

        Handles.Label(vectorAProjection + (vectorBProjection - vectorAProjection) / 3, "The Projection");
    }

    public override void Example_5()
    {
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "The Dot Product compare the directions together", guiStyle);
        guiStyle.fontSize = 20;
        Handles.Label(Vector3.zero, " ", guiStyle);

        Gizmos.DrawRay(new Vector3(2, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(2, 1f, 0) + vectorA, 0.2f);
        Handles.Label(new Vector3(2, 0.8f, 0), "Normalized VectorA");

        Gizmos.DrawRay(new Vector3(5, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(5, 1, 0) + vectorB, 0.2f);
        Handles.Label(new Vector3(5, 0.8f, 0), "Normalized VectorB");

        guiStyle.fontSize = 30;
        Handles.Label(new Vector3(-1.5f, 1.5f, 0), "Vector3.dot(", guiStyle);
        Handles.Label(new Vector3(4.5f, 1.5f, 0), ",", guiStyle);
        Handles.Label(new Vector3(7.5f, 1.5f, 0), ")", guiStyle);
        Handles.Label(new Vector3(8f, 1.5f, 0), "->", guiStyle);

        Gizmos.DrawRay(new Vector3(9, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(9, 1f, 0) + vectorA, 0.2f);
        Gizmos.DrawRay(new Vector3(9, 1, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(9, 1, 0) + vectorB, 0.2f);
    }

    public override void Example_4()
    {
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.up * 3, "Imagine two normalized Vectors, anywhere they could be", guiStyle);
        guiStyle.fontSize = 20;
        Handles.Label(Vector3.zero, "Dot Product PROJECTS one Vector to another", guiStyle);
        Handles.Label(Vector3.zero + new Vector3(0, -0.6f), "They just need to be passed to the method: Vector3.dot(Vector1, Vector2)", guiStyle);

        Gizmos.DrawRay(new Vector3(0, 1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(0, 1f, 0) + vectorA, 0.2f);
        Handles.Label(new Vector3(0, 0.8f, 0), "Normalized VectorA");

        Gizmos.DrawRay(new Vector3(4, 2, 0), vectorB);
        Gizmos.DrawSphere(new Vector3(4, 2, 0) + vectorB, 0.2f);
        Handles.Label(new Vector3(4, 1.8f, 0), "Normalized VectorB");


    }

    public override void Example_3()
    {
        Vector3 distAToB = vectorB - vectorA;
        Gizmos.DrawSphere(vectorA, 0.1f);
        Gizmos.DrawSphere(vectorB, 0.1f);
        Gizmos.DrawLine(vectorA, vectorA + distAToB);

        Gizmos.DrawLine(new Vector3(3, 0, 0), new Vector3(3, 0, 0) + distAToB.normalized);
        Gizmos.DrawSphere(new Vector3(3, 0, 0), 0.1f);
        Gizmos.DrawSphere(new Vector3(3, 0, 0) + distAToB, 0.1f);

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

    public override void Example_2()
    {

        guiStyle.fontSize = 35;
        Handles.Label(Vector3.up * 3, "Same Vectors now Normalized", guiStyle);
        guiStyle.fontSize = 25;
        Handles.Label(Vector3.zero, "They are equal in Lenght (Magnitude of 1)", guiStyle);

        Gizmos.DrawRay(new Vector3(2, -0.3f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(2, -0.3f, 0) + vectorA, 0.2f);
        Gizmos.DrawRay(new Vector3(1, -0.1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(1, -0.1f, 0) + vectorA, 0.2f);
        Gizmos.DrawRay(new Vector3(3, 0f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3, 0f, 0) + vectorA, 0.2f);
        Gizmos.DrawRay(new Vector3(2.6f, 0.1f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(2.6f, 0.1f, 0) + vectorA, 0.2f);
        Gizmos.DrawRay(new Vector3(4.5f, 0.3f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(4.5f, 0.3f, 0) + vectorA, 0.2f);
        Gizmos.DrawRay(new Vector3(3.3f, 0.2f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(3.3f, 0.2f, 0) + vectorA, 0.2f);
        Gizmos.DrawRay(new Vector3(4.1f, 0.4f, 0), vectorA);
        Gizmos.DrawSphere(new Vector3(4.1f, 0.4f, 0) + vectorA, 0.2f);
    }

    public override void Example_1()
    {
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
}
