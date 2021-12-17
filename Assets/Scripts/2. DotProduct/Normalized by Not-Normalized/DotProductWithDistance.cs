using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DotProductWithDistance : PagesAbstract
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
    [Range(1,2)]
    public float someScalar =1.55f;

    private void OnDrawGizmos()
    {
        if (examples == null || examples.Length != 8) examples = new bool[8];
        guiStyle = new GUIStyle();
        Lessons();
        ExamplesController();
    }

    public override void ExamplesController()
    {
        SetTitle("Overview", 1, 1);
        SetTitle("The Problem", 2, 3);
        SetTitle("Solution", 4, 8);

        pivot1 = new Vector3(0, 1, 0);
        pivot2 = new Vector3(6, 1, 0);
        pivot3 = new Vector3(1, -3, 0);
        pivot4 = new Vector3(7, -3, 0);

        Vector3 playerInitialPos = new Vector3(1, 1.5f, 0);
        Vector3 playerSlopePos = new Vector3(10, 1.5f);

        if (currentPage == 1)
        {
            player.transform.localPosition = playerInitialPos;
            stage.transform.gameObject.SetActive(false);
            slope.transform.gameObject.SetActive(false);
            player.transform.gameObject.SetActive(false);
        }
        if (currentPage == 2)
        {
            stage.transform.gameObject.SetActive(true);
            slope.transform.gameObject.SetActive(true);
            player.transform.gameObject.SetActive(true);
        }
        if (currentPage > 2)
        {
            stage.transform.gameObject.SetActive(false);
            slope.transform.gameObject.SetActive(true);
            player.transform.gameObject.SetActive(true);
            player.transform.localPosition = playerSlopePos;

        }

        if (currentPage >= 6)
        {
            //stage.transform.gameObject.SetActive(false);
            //slope.transform.gameObject.SetActive(false);
            //player.transform.gameObject.SetActive(false);
        }
    }

    private void DrawPlayerVelocity()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.transform.position, player.transform.position + player.transform.right * someScalar);
        Gizmos.DrawSphere(player.transform.position + player.transform.right * someScalar, 0.1f);
        Handles.Label(player.transform.position + player.transform.right + new Vector3(0, -0.1f), "PlayerVelocity");
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

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivot2 + NotNormalized, pivot2 + vectorB * dotScalar2);

        Handles.Label(pivot2 + Vector3.up * 1.5f + Vector3.left, "Scalar = " + dotScalar2, guiStyle);

        //Chart Legend 1
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(pivot1 + Vector3.down * 1.2f, "Normalized x Not-Normalized", guiStyle);
        guiStyle.fontSize = 10;
        Handles.Label(pivot1 + Vector3.down * 1.6f, "PRO: Scalar can be used to Direction Information");
        Handles.Label(pivot1 + Vector3.down * 1.8f, "CON: Scalar looses distance information");

        //Chart Legend 1
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(pivot2 + Vector3.down * 1.2f, "Normalized x Not-Normalized", guiStyle);
        guiStyle.fontSize = 10;
        Handles.Label(pivot2 + Vector3.down * 1.6f, "PRO: Scalar holds distance information");
        Handles.Label(pivot2 + Vector3.down * 1.8f, "CON: Scalar looses some direction information");
    }

    public override void Example_2()
    {
        base.Example_2();
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(player.transform.position + new Vector3(0, 0.4f), "Player");
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "The Player is moving faster than 1 unity, his velocity in Xaxis is equal to " + someScalar, guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "soon he will reach a slope", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "and to correct his direction we have to normalize his velocity", guiStyle);

        DrawPlayerVelocity();
    }

    public override void Example_3()
    {
        base.Example_3();
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;
        Handles.Label(player.transform.position + new Vector3(0, 0.4f), "Player");
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "How to solve this problem?", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "The player must adjust his velocity", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "but loose his current velocity is not the right decision", guiStyle);

        DrawPlayerVelocity();
        DrawNormals();
    }

    public override void Example_4()
    {
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.fontSize = 15;
        Handles.Label(player.transform.position + new Vector3(0, 0.4f), "Player");
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "2. The slope Normal", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "Finding the Right direction from the slope angle", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "Should retrieve the direction where the player could walk", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.9f), "To get the direction: Normalized x Normalized", guiStyle);

        Gizmos.DrawLine(slopePivot, slopePivot + ProjectOnGround(player.transform.right, slopeNormal));
        Handles.Label(slopePivot + ProjectOnGround(player.transform.right, slopeNormal), "ProjectedDirectionOnPlane");

        Handles.Label(Vector3.down * 0.4f, "easy as the formula");
        Handles.Label(Vector3.down * 0.6f, "ProjectedDirectionOnPlane = Vector3.right - slopeNormal * vector3.dot(slopeNormal, Vector3.right)");

        DrawPlayerVelocity();
        DrawNormals();
    }



    public override void Example_5()
    {
        base.Example_5();
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.Bold;

        //DrawNormals();

        Handles.Label(Vector3.up * 3, "With the right direction from the slope: ProjectedDirectionOnPlane", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "is possible to draw his velocity over this direction", guiStyle);
        Handles.Label(Vector3.down * 0.3f, "But one more problem rises: ", guiStyle);
        Handles.Label(Vector3.down * 0.6f, "When climbing a slope, his velocity should not remain the same", guiStyle);
        Handles.Label(Vector3.down * 0.9f, "Thats where Normalized x Not-Normalized enters", guiStyle);


        //Gizmos.DrawLine(slopePivot, slopePivot + ProjectOnGround(player.transform.right, slopeNormal));
        //Handles.Label(slopePivot + ProjectOnGround(player.transform.right, slopeNormal), "PlayerMovementProjected", guiStyle);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.transform.position, player.transform.position + ProjectOnGround(player.transform.right, slopeNormal) * someScalar);
    }

    public override void Example_6()
    {
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.fontSize = 15;
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "DirectionProjectedOnPlane is the direction the player will climb", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.3f), "His velocity is it's actual velocity", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.6f), "What happens when projecting", guiStyle);
        Handles.Label(Vector3.up * 3 + new Vector3(0, -0.9f), "DirectionProjectedOnPlane to PlayerVelocity", guiStyle);

        Gizmos.DrawLine(slopePivot, slopePivot + ProjectOnGround(player.transform.right, slopeNormal));
        Handles.Label(slopePivot + ProjectOnGround(player.transform.right, slopeNormal), "ProjectedDirectionOnPlane");

        Handles.Label(Vector3.down * 0.4f, "easy as the formula");
        Handles.Label(Vector3.down * 0.6f, "ProjectedDirectionOnPlane = Vector3.right - slopeNormal * vector3.dot(slopeNormal, Vector3.right)");

        DrawPlayerVelocity();

        //Normalized x Not-Normalized
        Vector3 pivot2 = new Vector3(6f, 1f, 0);
        //Vector3 projectedDirection = ProjectOnGround(Vector3.right, slopeNormal).normalized;
        Vector3 projectedDirection = RenderToMouse();
        Vector3 playerVelocity = Vector3.right * someScalar;
        float dotScalar2 = Vector3.Dot(playerVelocity, projectedDirection);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(pivot2, playerVelocity);
        Gizmos.DrawSphere(pivot2 + playerVelocity, 0.1f);
        Gizmos.DrawRay(pivot2, projectedDirection);
        Gizmos.DrawSphere(pivot2 + projectedDirection, 0.1f);

        Gizmos.color = Color.green;
        //Gizmos.DrawRay(pivot2, projectedDirection * dotScalar2);
        //Gizmos.DrawRay(pivot2, playerVelocity * dotScalar2);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pivot2 + playerVelocity, pivot2 + projectedDirection * dotScalar2);
        if(dotScalar2 > 1f)
            Gizmos.DrawLine(pivot2 + projectedDirection * dotScalar2, pivot2 + projectedDirection);
        //Gizmos.DrawLine(pivot2 + projectedDirection, pivot2 + playerVelocity * dotScalar2);

        Handles.Label(pivot2 + Vector3.down * 0.2f, "Original Velocity: " + someScalar);
        Handles.Label(pivot2 + Vector3.down * 0.4f, "dotProduct: " + dotScalar2);
        Handles.Label(pivot2 + Vector3.down * 0.6f, "New velocity Scalar:" );

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
        Handles.Label(pivot1 + Vector3.up * 1.3f + Vector3.left * 2f, "dotProjection = Vector3.Dot(slopeNormal, player.transform.right)", guiStyle);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot1, pivot1 + player.transform.right);
        Handles.Label(pivot1 + player.transform.right, "PlayerVelocity");
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
        Handles.Label(pivot2 + player.transform.right, "PlayerVelocity");
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
        Handles.Label(pivot3 + Vector3.up * 1.3f + Vector3.left * 2f, "PlayerVelocity - Dot Projection * Slope Normal", guiStyle);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot3, pivot3 + player.transform.right);
        Handles.Label(pivot3 + player.transform.right, "PlayerVelocity");
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pivot3, pivot3 + slopeNormal);
        Handles.Label(pivot3 + slopeNormal + new Vector3(0, 0.2f, 0), "Slope Normal");

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(pivot3, pivot3 + player.transform.right * dotScalar);
        Gizmos.DrawLine(pivot3 + player.transform.right * dotScalar, pivot3 + slopeNormal);
        Handles.Label(pivot3 + player.transform.right * dotScalar, "Dot Projection");

        Gizmos.DrawLine(pivot3, pivot3 + slopeNormal * dotScalar);
        Handles.Label(pivot3 + slopeNormal * dotScalar, "PlayerVelocity - Dot Projection * Slope Normal");

        Gizmos.DrawLine(pivot3 + player.transform.right, pivot3 + (player.transform.right - slopeNormal * dotScalar));
        Handles.Label(pivot3 + (player.transform.right - slopeNormal * dotScalar) + Vector3.right * 0.1f, "PlayerVelocity - Dot Projection * Slope Normal");

        Handles.Label(Vector3.down * 4f, "Subtracting  to the PlayerVelocity");
        Handles.Label(Vector3.down * 4.3f, "we get a position");

        //Pivot4
        guiStyle.fontSize = 25;
        Handles.Label(pivot4 + Vector3.up + Vector3.left, "4", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pivot4, pivot4 + player.transform.right);
        Handles.Label(pivot4 + player.transform.right, "PlayerVelocity");
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
