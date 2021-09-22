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

    private void OnDrawGizmos()
    {
        if (examples == null || examples.Length != 12) examples = new bool[12];
        guiStyle = new GUIStyle();
        Lessons();
        ExamplesController();
    }

    public override void ExamplesController()
    {
        SetTitle("The problem", 1, 3);
        SetTitle("Projecting Player Direction Over a Plave", 5, 5);
        SetTitle("How projecting works", 6, 10);

        pivot1 = new Vector3(1, 1, 0);
        pivot2 = new Vector3(7, 1, 0);
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
        guiStyle.fontStyle = FontStyle.Bold;

        Handles.Label(player.transform.position + new Vector3(0,0.4f), "Player");
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "The player is moving to the right direction", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "", guiStyle);

        DrawPlayerMovement();
    }

    public override void Example_2()
    {
        base.Example_2();
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(player.transform.position + new Vector3(0, 0.4f), "Player");
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "However when the player Reaches a slope", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "His PlayerMovement will make him stumble in the slope", guiStyle);
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
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "1. PlayerMovement direction that points To his right", guiStyle);

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
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "", guiStyle);

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
        Handles.Label(pivot1 + player.transform.right, "PlayerMovement");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot1, pivot1 + slopeNormal);
        Handles.Label(pivot1 + slopeNormal + new Vector3(0,0.2f,0), "Slope Normal");

        //Pivot2
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot2, pivot2 + player.transform.right);
        Handles.Label(pivot2 + player.transform.right, "PlayerMovement");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot2, pivot2 + slopeNormal);
        Handles.Label(pivot2 + slopeNormal + new Vector3(0, 0.2f, 0), "Slope Normal");

        Gizmos.color = Color.cyan;
        float dotScalar = Vector3.Dot(slopeNormal, player.transform.right);
        Gizmos.DrawLine(pivot2, pivot2 + player.transform.right * dotScalar);
        Gizmos.DrawLine(pivot2 + player.transform.right * dotScalar, pivot2 + slopeNormal);
        Handles.Label(pivot2 + player.transform.right * dotScalar, "Dot Projection");

        Handles.Label(Vector3.up * 2.8f, "With dot product we know how much the vectors", guiStyle);
        Handles.Label(Vector3.up * 2.5f, "are pointing towards or backwards each other", guiStyle);
        guiStyle.fontSize = 20;

        Handles.Label(Vector3.down * 0.6f, "Playermovement needs to rotate to match Normal", guiStyle);
        Handles.Label(Vector3.down * 0.9f, "Normal will retrieve direction and scale information", guiStyle);
        Handles.Label(Vector3.down * 1.2f, "From the PlayerMovement perspective, it should rotate in which direction?", guiStyle);

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

        Handles.Label(Vector3.up * 3, "Getting how much the normal moved and in which direction, in comparison to Player Movement");
        Handles.Label(Vector3.up * 2.7f, "DirectionOverNormal = player.transform.right - slopeNormal * Vector3.Dot(slopeNormal, player.transform.right)");

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

        Handles.Label(Vector3.down * 0f, "With dot product we know how much the vectors");
        Handles.Label(Vector3.down * 0.3f, "are pointing towards or backwards each other");

        //Pivot2
        guiStyle.fontSize = 25;
        Handles.Label(pivot2 + Vector3.up + Vector3.left, "2", guiStyle);
        guiStyle.fontSize = 10;
        Handles.Label(pivot2 + Vector3.up * 1.3f + Vector3.left * 2f, "normalScaled = slopeNormal * dotProjection", guiStyle);

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
        Handles.Label(pivot2 + slopeNormal * dotScalar, "normalScaled = Dot Projection * Slope Normal");

        Handles.Label(Vector3.down * 0f + Vector3.right * 6, "Applying Dot Projection on the slope we get a Vector : normalScaled");
        Handles.Label(Vector3.down * 0.3f + Vector3.right * 6, "normalScaled holds the direction and scale of the Slope normal");
        Handles.Label(Vector3.down * 0.6f + Vector3.right * 6, "in comparison to the Player Movement");

        //Pivot3
        guiStyle.fontSize = 25;
        Handles.Label(pivot3 + Vector3.up + Vector3.left, "3", guiStyle);
        guiStyle.fontSize = 10;
        Handles.Label(pivot3 + Vector3.up * 1.3f + Vector3.left * 2f, "directionOnNormalPlane = PlayerMovement - normalScaled", guiStyle);

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
        Handles.Label(pivot3 + slopeNormal * dotScalar, "normalScaled = Dot Projection * Slope Normal");

        Gizmos.DrawLine(pivot3 + player.transform.right, pivot3 + (player.transform.right - slopeNormal * dotScalar));
        Handles.Label(pivot3 + (player.transform.right - slopeNormal * dotScalar) + Vector3.right * 0.1f, "directionOnNormalPlane = PlayerMovement - normalScaled Value");

        Handles.Label(Vector3.down * 4f, "Subtracting normalScaled to the Player Movement");
        Handles.Label(Vector3.down * 4.3f, "we get a position equal the rotation of the Normal");

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
        Handles.Label(pivot4 + player.transform.right - slopeNormal * dotScalar, "Direction Over Normal");
    }

    public override void Example_8()
    {
        base.Example_8();

        Vector3 reflectingRight = Vector3.right - RenderToMouse() * Vector3.Dot(Vector3.right, RenderToMouse());
        //REFLECTION COMPARISION
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.zero, Vector3.right);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(Vector3.zero, reflectingRight.normalized);
        Handles.Label(reflectingRight.normalized, reflectingRight.normalized.ToString());
        Handles.Label(reflectingRight.normalized + Vector3.up * 0.2f, "Projection Over Normal");
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Vector3.zero, RenderToMouse().normalized);
        Handles.Label(RenderToMouse().normalized, RenderToMouse().normalized.ToString());
        Handles.Label(RenderToMouse().normalized + Vector3.up * 0.2f, "A Slope Normal");
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
