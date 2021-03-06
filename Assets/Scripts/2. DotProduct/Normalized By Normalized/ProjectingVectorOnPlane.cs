using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectingVectorOnPlane : PagesAbstract
{
    private GUIStyle guiStyle;
    public Transform player;
    public Transform slope;
    public Transform stage;
    private Vector3 slopePivot;
    private Vector3 slopeNormal;
    private Vector3 pivot1;
    private Vector3 pivot2;
    private Vector3 pivot3;
    private Vector3 pivot4;
    public float someScalar;

    private void OnDrawGizmos()
    {
        if (examples == null || examples.Length != 8) examples = new bool[8];
        guiStyle = new GUIStyle();
        Lessons();
        ExamplesController();
    }

    public override void ExamplesController()
    {
        SetTitle("The problem", 1, 3);
        SetTitle("Projecting Player Direction Over a Plane", 4, 5);
        SetTitle("How projecting works", 6, 8);

        pivot1 = new Vector3(0, 1, 0);
        pivot2 = new Vector3(6, 1, 0);
        pivot3 = new Vector3(1, -3, 0);
        pivot4 = new Vector3(7, -3, 0);

        Vector3 playerInitialPos = new Vector3(1, 1.5f, 0);
        Vector3 playerSlopePos = new Vector3(10, 1.5f);

        if (currentPage < 2)
        {
            player.transform.localPosition = playerInitialPos;
        }
        if (currentPage > 1)
            player.transform.localPosition = playerSlopePos;
        if (currentPage < 5)
        {
            stage.transform.gameObject.SetActive(true);
            slope.transform.gameObject.SetActive(true);
            player.transform.gameObject.SetActive(true);
        }
        if (currentPage > 4)
        {
            stage.transform.gameObject.SetActive(false);
            slope.transform.gameObject.SetActive(true);
            player.transform.gameObject.SetActive(true);
        }
            
        if (currentPage >= 6)
        {
            stage.transform.gameObject.SetActive(false);
            slope.transform.gameObject.SetActive(false);
            player.transform.gameObject.SetActive(false);
        }
    }

    private void DrawPlayerMovement()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.transform.position, player.transform.position + player.transform.right);
        Gizmos.DrawSphere(player.transform.position + player.transform.right, 0.1f);
        Handles.Label(player.transform.position + player.transform.right + new Vector3(0, -0.1f), "PlayerMovement");
    }

    public void DrawNormals()
    {
        Vector3 ground1Normal = new Vector3(1, 0, 0);
        slopePivot = new Vector3(2.8f, 0.63f, 0);
        slopeNormal = new Vector3(-0.63f, 1f).normalized;
        Vector3 ground3Normal = new Vector3(4.5f, 1, 0);

        Gizmos.color = Color.yellow;
        if (stage.transform.gameObject.activeSelf)
        {
            Gizmos.DrawLine(ground1Normal, ground1Normal + Vector3.up);
            Gizmos.DrawLine(ground3Normal, ground3Normal + Vector3.up);
        }
        Gizmos.DrawLine(slopePivot, slopePivot + slopeNormal);
        Handles.Label(slopePivot + new Vector3(-0.5f, 0.7f), "Slope Normal");
    }

    Vector3 ProjectOnGround(Vector3 directionToProject, Vector3 groundNormal)
    {
        Vector3 projectedVector = directionToProject - groundNormal * Vector3.Dot(directionToProject, groundNormal);
        return projectedVector;
    }

    public override void Example_1()
    {
        base.Example_1();

        guiStyle.fontSize = 15;
        Handles.Label(Vector3.down * 0.2f, "Click + Hold", guiStyle);
        Handles.Label(Vector3.down * 0.4f, "circle this red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        Vector3 pivot1 = new Vector3(3f, 1f, 0);
        Vector3 vectorB = Vector3.right;

        Vector3 mouseVector = RenderToMouse();
        float shadowScalar = Vector3.Dot(vectorB, mouseVector);

        //Normalized x Normalized
        Gizmos.color = Color.white;
        Gizmos.DrawRay(pivot1, mouseVector);
        Gizmos.DrawSphere(pivot1 + mouseVector, 0.1f);
        Gizmos.DrawRay(pivot1, vectorB);
        Gizmos.DrawSphere(pivot1 + vectorB, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(pivot1, vectorB * shadowScalar);
        Gizmos.DrawRay(pivot1, mouseVector * shadowScalar);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivot1 + mouseVector, pivot1 + vectorB * shadowScalar);
        Gizmos.DrawLine(pivot1 + vectorB, pivot1 + mouseVector * shadowScalar);

        Handles.Label(pivot1 + Vector3.up * 1.5f + Vector3.left, "Scalar = " + shadowScalar, guiStyle);

        //Normalized x Not-Normalized
        Vector3 pivot2 = new Vector3(7f, 1f, 0);
        Vector3 NotNormalized = mouseVector * someScalar;
        float dotScalar2 = Vector3.Dot(vectorB, NotNormalized);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(pivot2, NotNormalized);
        Gizmos.DrawSphere(pivot2 + NotNormalized, 0.1f);
        Gizmos.DrawRay(pivot2, vectorB);
        Gizmos.DrawSphere(pivot2 + vectorB, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(pivot2, vectorB * dotScalar2);
        Gizmos.DrawRay(pivot2, NotNormalized * dotScalar2);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivot2 + NotNormalized, pivot2 + vectorB * dotScalar2);
        Gizmos.DrawLine(pivot2 + vectorB, pivot2 + NotNormalized * dotScalar2);

        Handles.Label(pivot2 + Vector3.up * 1.5f + Vector3.left, "Scalar = " + dotScalar2, guiStyle);

        //Chart Legend 1
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(pivot1 + Vector3.down * 1.2f, "Normalized x Not-Normalized", guiStyle);
        guiStyle.fontSize = 10;
        Handles.Label(pivot1 + Vector3.down * 1.4f, "Scalar looses distance information");
        Handles.Label(pivot1 + Vector3.down * 1.6f, "Scalar can be used to Direction Information");

        //Chart Legend 1
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(pivot2 + Vector3.down * 1.2f, "Normalized x Not-Normalized", guiStyle);
        guiStyle.fontSize = 10;
        Handles.Label(pivot2 + Vector3.down * 1.4f, "Scalar looses some direction information" );
        Handles.Label(pivot2 + Vector3.down * 1.6f, "Scalar holds distance information" );
    }

    public override void Example_2()
    {
        base.Example_2();
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(player.transform.position + new Vector3(0, 0.4f), "Player");
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "However when the player Reaches a slope", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "His PlayerVelocity will make him stumble in the slope", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "His Right is not a correct direction to the new ground he is on", guiStyle);

        DrawPlayerMovement();
    }

    public override void Example_3()
    {
        base.Example_3();
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(player.transform.position + new Vector3(0, 0.4f), "Player");
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "How to solve this problem?", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "What Information do we have?", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "1. PlayerVelocity direction that points To his right", guiStyle);

        DrawPlayerMovement();
    }

    public override void Example_4()
    {
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.fontSize = 15;
        Handles.Label(player.transform.position + new Vector3(0, 0.4f), "Player");
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "2. The slope Normal", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "Normals are vectors stored in 3D objects", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "The shows the direction their face is pointing to", guiStyle);

        DrawPlayerMovement();
        DrawNormals();
    }



    public override void Example_5()
    {
        base.Example_5();
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;

        DrawNormals();
        DrawPlayerMovement();

        Handles.Label(Vector3.up * 3, "The solution was using the slope normal vector", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "to draw the direction of PlayerMovement over the Slope", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "The question is: ", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.9f), "Where is (direction) in this normal vector?", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -1.2f), "this is where projection over plane (normal) is needed", guiStyle);

        Gizmos.DrawLine(slopePivot, slopePivot + ProjectOnGround(player.transform.right, slopeNormal));
        Handles.Label(slopePivot + ProjectOnGround(player.transform.right, slopeNormal), "PlayerMovementProjected", guiStyle);
    }

    public override void Example_6()
    {
        base.Example_6();
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;

        //Pivot1
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot1, pivot1 + player.transform.right);
        Handles.Label(pivot1 + player.transform.right, "Right");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot1, pivot1 + Vector3.up);
        Handles.Label(pivot1 + Vector3.up + new Vector3(0, 0.2f, 0), "Up");
        Handles.Label(pivot1 * 0.6f, "We know where right is projected");
        Handles.Label(pivot1 * 0.4f, "if the up vector is pointing up");



        //Pivot2
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot2, pivot2 + player.transform.right);
        Handles.Label(pivot2 + player.transform.right, "PlayerMovement");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot2, pivot2 + slopeNormal);
        Handles.Label(pivot2 + slopeNormal + new Vector3(0,0.2f,0), "Slope Normal");
        Handles.Label(pivot2 + Vector3.down * 0.4f, "But what is the position of Right");
        Handles.Label(pivot2 + Vector3.down * 0.6f, "Under the Slope normal?");

        Vector3 pivot23  = new Vector3(13, 1, 0);
        //Pivot23
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot23, pivot23 + player.transform.right);
        Handles.Label(pivot23 + player.transform.right, "PlayerMovement");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot23, pivot23 + slopeNormal);
        Handles.Label(pivot23 + slopeNormal + new Vector3(0, 0.2f, 0), "Slope Normal");
        Handles.Label(pivot23 + Vector3.down * 0.4f, "Projecting slope vector over Vector3.right (Player Movement)");
        Handles.Label(pivot23 + Vector3.down * 0.6f, "We can see how much slope vector displaced against vector3.right (Player Movement)");
        Handles.Label(pivot23 + Vector3.down * 0.8f, "But it's a simple float scalar, that does not hold in wich direction it displaced");

        Gizmos.color = Color.cyan;
        float dotScalar = Vector3.Dot(slopeNormal, player.transform.right);
        Gizmos.DrawLine(pivot23, pivot23 + player.transform.right * dotScalar);
        Gizmos.DrawLine(pivot23 + player.transform.right * dotScalar, pivot23 + slopeNormal);
        Handles.Label(pivot23 + player.transform.right * dotScalar, "Dot Projection");

        Handles.Label(Vector3.up * 2.8f, "With dot product we know how much the vectors", guiStyle);
        Handles.Label(Vector3.up * 2.5f, "are pointing towards or backwards each other", guiStyle);
        guiStyle.fontSize = 20;

        Handles.Label(Vector3.down * 0.6f, "How to get the direction with only the scalar?", guiStyle);
        Handles.Label(Vector3.down * 1.0f, "", guiStyle);
        Handles.Label(Vector3.down * 1.2f, "", guiStyle);

        //Pivot3
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot3, pivot3 + player.transform.right);
        Handles.Label(pivot3 + player.transform.right, "PlayerMovement");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot3, pivot3 + slopeNormal);
        Handles.Label(pivot3 + slopeNormal + new Vector3(0, 0.2f, 0), "Slope Normal");
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(pivot3, pivot3 + slopeNormal * dotScalar);
        Handles.Label(pivot3 + slopeNormal * dotScalar + new Vector3(0, 0.2f, 0), "A new vector");
        Handles.Label(pivot3 + Vector3.down * 0.6f, "Applying the scalar to the slope Vector results in new vector");
        Handles.Label(pivot3 + Vector3.down * 0.8f, "with the direction and distance between the two");

        //Pivot3
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot4, pivot4 + player.transform.right);
        Handles.Label(pivot4 + player.transform.right, "PlayerMovement");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot4, pivot4 + slopeNormal);
        Handles.Label(pivot4 + slopeNormal + new Vector3(0, 0.2f, 0), "Slope Normal");
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(pivot4 + player.transform.right, pivot4 + player.transform.right - slopeNormal * dotScalar);
        Handles.Label(pivot4 + player.transform.right - slopeNormal * dotScalar + new Vector3(0, 0.2f, 0), "The distance");
        Handles.Label(pivot4 + Vector3.down * 0.6f, "Using it to get the difference from the Vector3.right");
        Handles.Label(pivot4 + Vector3.down * 0.8f, "Results in Vector3.right projected On Slope Vector");

        //Vector3 reflectingRight = Vector3.right - RenderToMouse() * Vector3.Dot(Vector3.right, RenderToMouse());
        //REFLECTION COMPARISION
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(Vector3.zero, Vector3.right);
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawRay(Vector3.zero, reflectingRight.normalized);
        //Handles.Label(reflectingRight.normalized, reflectingRight.normalized.ToString());
        //Gizmos.color = Color.green;
        //Gizmos.DrawRay(Vector3.zero, RenderToMouse().normalized);
        //Handles.Label(RenderToMouse().normalized, RenderToMouse().normalized.ToString());
        //Gizmos.color = Color.black;
        //Vector3 reflectedVector = new Vector3(RenderToMouse().normalized.x, -RenderToMouse().normalized.y, RenderToMouse().normalized.z);
        //Gizmos.DrawRay(Vector3.zero, reflectedVector);
        //Handles.Label(reflectedVector, reflectedVector.ToString());
    }

    public override void Example_7()
    {
        base.Example_7();

        Handles.Label(Vector3.up * 3, "Where is right from the slope Vector ?");
        Handles.Label(Vector3.up * 2.7f, "");

        //Pivot1
        guiStyle.fontSize = 25;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(pivot1 + Vector3.up + Vector3.left, "1", guiStyle);
        guiStyle.fontSize = 10;
        Handles.Label(pivot1 + Vector3.up * 1.3f + Vector3.left *2f, "dotProjection = Vector3.Dot(slopeNormal, player.transform.right)", guiStyle);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot1, pivot1 + player.transform.right);
        Handles.Label(pivot1 + player.transform.right, "PlayerMovement");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot1, pivot1 + slopeNormal);
        Handles.Label(pivot1 + slopeNormal + new Vector3(0, 0.2f, 0), "Slope Normal");

        Gizmos.color = Color.cyan;
        float dotScalar = Vector3.Dot(slopeNormal, player.transform.right);
        Gizmos.DrawLine(pivot1, pivot1 + player.transform.right * dotScalar);
        Gizmos.DrawLine(pivot1 + player.transform.right * dotScalar, pivot1 + slopeNormal);
        Handles.Label(pivot1 + player.transform.right * dotScalar, "Dot Projection");

        Handles.Label(Vector3.down * 0f, "Dot to Project slope on the direction");
        Handles.Label(Vector3.down * 0.3f, "get the displacement in scalar");

        //Pivot2
        guiStyle.fontSize = 25;
        Handles.Label(pivot2 + Vector3.up + Vector3.left, "2", guiStyle);
        guiStyle.fontSize = 10;
        Handles.Label(pivot2 + Vector3.up * 1.3f + Vector3.left * 2f, "Dot Projection * Slope Normal", guiStyle);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot2, pivot2 + player.transform.right);
        Handles.Label(pivot2 + player.transform.right, "PlayerMovement");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot2, pivot2 + slopeNormal);
        Handles.Label(pivot2 + slopeNormal + new Vector3(0, 0.2f, 0), "Slope Normal");

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(pivot2, pivot2 + player.transform.right * dotScalar);
        Gizmos.DrawLine(pivot2 + player.transform.right * dotScalar, pivot2 + slopeNormal);
        Handles.Label(pivot2 + player.transform.right * dotScalar, "Dot Projection");

        Gizmos.DrawLine(pivot2, pivot2 + slopeNormal * dotScalar);
        Handles.Label(pivot2 + slopeNormal * dotScalar, "Dot Projection * Slope Normal");

        Handles.Label(Vector3.down * 0f + Vector3.right * 6, "The scalar does not hold any distance, but the slope do");
        Handles.Label(Vector3.down * 0.3f + Vector3.right * 6, "applying scalar on to slope returns it's distance and direction");
        Handles.Label(Vector3.down * 0.6f + Vector3.right * 6, "");

        //Pivot3
        guiStyle.fontSize = 25;
        Handles.Label(pivot3 + Vector3.up + Vector3.left, "3", guiStyle);
        guiStyle.fontSize = 10;
        Handles.Label(pivot3 + Vector3.up * 1.3f + Vector3.left * 2f, "PlayerMovement - Dot Projection * Slope Normal", guiStyle);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot3, pivot3 + player.transform.right);
        Handles.Label(pivot3 + player.transform.right, "PlayerMovement");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot3, pivot3 + slopeNormal);
        Handles.Label(pivot3 + slopeNormal + new Vector3(0, 0.2f, 0), "Slope Normal");

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(pivot3, pivot3 + player.transform.right * dotScalar);
        Gizmos.DrawLine(pivot3 + player.transform.right * dotScalar, pivot3 + slopeNormal);
        Handles.Label(pivot3 + player.transform.right * dotScalar, "Dot Projection");

        Gizmos.DrawLine(pivot3, pivot3 + slopeNormal * dotScalar);
        Handles.Label(pivot3 + slopeNormal * dotScalar, "PlayerMovement - Dot Projection * Slope Normal");

        Gizmos.DrawLine(pivot3 + player.transform.right, pivot3 + (player.transform.right - slopeNormal * dotScalar));
        Handles.Label(pivot3 + (player.transform.right - slopeNormal * dotScalar) + Vector3.right * 0.1f, "PlayerMovement - Dot Projection * Slope Normal");

        Handles.Label(Vector3.down * 4f, "Subtracting  to the Player Movement");
        Handles.Label(Vector3.down * 4.3f, "we get a position");

        //Pivot4
        guiStyle.fontSize = 25;
        Handles.Label(pivot4 + Vector3.up + Vector3.left, "4", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot4, pivot4 + player.transform.right);
        Handles.Label(pivot4 + player.transform.right, "PlayerMovement");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot4, pivot4 + slopeNormal);
        Handles.Label(pivot4 + slopeNormal + new Vector3(0, 0.2f, 0), "Slope Normal");

        Gizmos.color = Color.green;
        Gizmos.DrawLine(pivot4, pivot4 + player.transform.right - slopeNormal * dotScalar);
        Handles.Label(pivot4 + player.transform.right - slopeNormal * dotScalar, "projection Over Normal");
        Handles.Label(pivot4 + Vector3.down, "The position is the right vector");
        Handles.Label(pivot4 + Vector3.down * 1.2f, "from the slope normal perpective");
    }

    public override void Example_8()
    {
        base.Example_8();

        Handles.Label(Vector3.up * 3, "Notice that, projecting a direction over a plane");
        Handles.Label(Vector3.up * 2.8f, "you get where that direction would supposed to be from that plane");
        Handles.Label(Vector3.up * 2.6f, "It's not a simple reflection, it's a projection");

        Vector3 reflectingRight = Vector3.right - RenderToMouse() * Vector3.Dot(Vector3.right, RenderToMouse());
        //REFLECTION COMPARISION
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.zero, Vector3.right);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(Vector3.zero, reflectingRight.normalized);
        Handles.Label(reflectingRight.normalized, reflectingRight.normalized.ToString());
        Handles.Label(reflectingRight.normalized + Vector3.up * 0.2f, "Projecting Right Over Normal");
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Vector3.zero, RenderToMouse().normalized);
        Handles.Label(RenderToMouse().normalized, RenderToMouse().normalized.ToString());
        Handles.Label(RenderToMouse().normalized + Vector3.up * 0.2f, "A Normal");
        Gizmos.color = Color.black;
        Vector3 reflectedVector = new Vector3(RenderToMouse().normalized.x, -RenderToMouse().normalized.y, RenderToMouse().normalized.z);
        Gizmos.DrawRay(Vector3.zero, reflectedVector);
        Handles.Label(reflectedVector, reflectedVector.ToString());
        Handles.Label(reflectedVector + Vector3.up * 0.2f, "Simple Reflection");
    }

    public override void Example_9()
    {
        base.Example_9();
    }

    public override void Example_10()
    {
        base.Example_10();
    }
}
