using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NormalizedByNotNormalized : PagesAbstract
{
    private GUIStyle guiStyle;
    private Vector3 pivot1;
    private Vector3 pivot2;
    private Vector3 pivot3;
    private Vector3 pivot4;
    public Transform scenario;
    public Transform player;
    public float someScalar = 1.55f;

    private void OnDrawGizmos()
    {
        if (examples == null || examples.Length != 4) examples = new bool[4];
        guiStyle = new GUIStyle();
        Lessons();
        ExamplesController();
    }

    public override void ExamplesController()
    {
        SetTitle("Overview", 1, 2);
        SetTitle("Use Cases", 3, 4);

        pivot1 = new Vector3(1, 1, 0);
        pivot2 = new Vector3(6, 1, 0);
        pivot3 = new Vector3(1, -3, 0);
        pivot4 = new Vector3(7, -3, 0);

        if (currentPage > 3)
            scenario.gameObject.SetActive(true);
        else
            scenario.gameObject.SetActive(false);

    }

    Vector3 ProjectOnDirection(Vector3 directionToProject, Vector3 groundNormal)
    {
        Vector3 projectedVector = directionToProject - groundNormal * Vector3.Dot(directionToProject, groundNormal);
        return projectedVector;
    }

    public void DrawDotProduct(Vector3 notNormalized, Vector3 normalized)
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(pivot1, notNormalized);
        Gizmos.DrawSphere(pivot1 + notNormalized, 0.1f);
        Gizmos.DrawRay(pivot1, normalized);
        Gizmos.DrawSphere(pivot1 + normalized, 0.1f);

        float dotScalar2 = Vector3.Dot(notNormalized, normalized);

        if (dotScalar2 > 0)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(pivot1 + notNormalized, pivot1 + normalized * dotScalar2);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pivot1, pivot1 + normalized * dotScalar2);
            if (dotScalar2 > 1f)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(pivot1 + normalized * dotScalar2, pivot1 + normalized);
            }
        }
        else if (dotScalar2 < 0)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(pivot1 + notNormalized, pivot1 + normalized * dotScalar2);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pivot1, pivot1 + normalized * dotScalar2);
            if (dotScalar2 < -1f)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(pivot1 + normalized * dotScalar2, pivot1 - normalized);
            }
        }
    }

    public override void Example_1()
    {
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.fontSize = 15;
        guiStyle.normal.textColor = Color.white;
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "How a direction (normalized one) subtracts from the distance vector (not-normalized)", guiStyle);
        Handles.Label(Vector3.up * 2.7f, "", guiStyle);
        //PROJECTION CHART
        guiStyle.fontStyle = FontStyle.Normal;
        Vector3 chartPivot = pivot1 + Vector3.up *0.5f + Vector3.right * 2.6f;
        Handles.Label(chartPivot, "1. Same Direction: Scalar equals the Same Magnitude", guiStyle);
        Handles.Label(chartPivot + Vector3.down * 0.3f, "2. Every other direction takes from the magnitude in scalar", guiStyle);
        Handles.Label(chartPivot + Vector3.down * 0.6f, "3. Perpendicular reduces the magnitude to zero ins scalar", guiStyle);
        Handles.Label(chartPivot + Vector3.down * 0.9f, "4. Opposite direction results in negative magnitude in scalar", guiStyle);

        Vector3 projectedDirection = RenderToMouse();
        Vector3 playerVelocity = Vector3.right * someScalar;

        guiStyle.fontSize = 15;
        Handles.Label(Vector3.down * 0.2f, "Click + Hold", guiStyle);
        Handles.Label(Vector3.down * 0.4f, "circle this red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);

        DrawDotProduct(playerVelocity, projectedDirection);
    }

    public override void Example_2()
    {
        base.Example_2();

        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.fontSize = 15;
        guiStyle.normal.textColor = Color.white;
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "The result can be interpreted in many applications", guiStyle);
        Handles.Label(Vector3.up * 2.7f, "The results subtracts from the Not-Normalized Scale", guiStyle);
        //PROJECTION CHART
        guiStyle.fontStyle = FontStyle.Normal;
        Vector3 chartPivot = pivot1 + Vector3.up * 0.5f + Vector3.right * 2.6f;
        Handles.Label(chartPivot, "1. How a slope will change a velocity from another object", guiStyle);
        Handles.Label(chartPivot + Vector3.down * 0.3f, "2. How deep a impact can go under the surface", guiStyle);
        Handles.Label(chartPivot + Vector3.down * 0.6f, "3. How loud a impact will be", guiStyle);
        Handles.Label(chartPivot + Vector3.down * 0.9f, "4. How much a ship can turn", guiStyle);
        Handles.Label(chartPivot + Vector3.down * 1.2f, "Just to name a few", guiStyle);

        Vector3 projectedDirection = RenderToMouse();
        Vector3 playerVelocity = Vector3.right * someScalar;
        float dotScalar2 = Vector3.Dot(playerVelocity, projectedDirection);

        DrawDotProduct(playerVelocity, projectedDirection);

        Handles.Label(pivot1 + Vector3.down * 0.2f + Vector3.left * 0.5f, "Not-Normalized Scale: " + playerVelocity.magnitude);
        Handles.Label(pivot1 + Vector3.down * 0.4f + Vector3.left * 0.5f, "dotProduct / New Scale: " + dotScalar2);


        guiStyle.fontSize = 15;
        Handles.Label(Vector3.down * 0.2f, "Click + Hold", guiStyle);
        Handles.Label(Vector3.down * 0.4f, "circle this red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);
    }

    public override void Example_3()
    {
        base.Example_3();

        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.fontSize = 15;
        guiStyle.normal.textColor = Color.white;
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "A meteor is falling", guiStyle);
        Handles.Label(Vector3.up * 2.7f, "", guiStyle);
        //PROJECTION CHART
        guiStyle.fontStyle = FontStyle.Normal;
        Vector3 chartPivot = pivot1 + Vector3.up * 1.5f + Vector3.right * 2.6f;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(chartPivot, 0.2f);
        Vector3 meteorVelocity = (Vector3.right + Vector3.down) * 1.6f;
        Gizmos.DrawLine(chartPivot, chartPivot + meteorVelocity);
        Handles.Label(chartPivot + Vector3.right * 0.2f, "METEOR");
        Handles.Label(chartPivot + meteorVelocity + Vector3.up *0.2f + Vector3.left * 0.5f, "meteorVelocity");
        Handles.Label(chartPivot + meteorVelocity + Vector3.right , "GROUND");
        Vector3 groundPosition = chartPivot + meteorVelocity;
        Gizmos.DrawLine(groundPosition + Vector3.left * 2, groundPosition + Vector3.right * 4);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundPosition, groundPosition + Vector3.up);
        Handles.Label(groundPosition + Vector3.up, "groundNormal");

        Vector3 projectedDirection = Vector3.up;
        Vector3 anyDistance = meteorVelocity;
        float dotScalar2 = Vector3.Dot(anyDistance, projectedDirection);

        DrawDotProduct(anyDistance, projectedDirection);


        Handles.Label(pivot1 + Vector3.down * 0.2f + Vector3.left * 0.5f, "Not-Normalized Scale: " + anyDistance.magnitude);
        Handles.Label(pivot1 + Vector3.down * 0.4f + Vector3.left * 0.5f, "dotProduct / New Scale: " + dotScalar2);

        //Handles.Label(Vector3.right * 4, "Using absolute value: " + Mathf.Abs(dotScalar2));
        Handles.Label(Vector3.right * 4 + Vector3.down * 0.3f, "Meteor traveled Distance: " + (anyDistance.magnitude + dotScalar2) );
        Handles.Label(Vector3.right * 4 + Vector3.down * 0.6f, "Impact Volume: " + Mathf.Abs(dotScalar2) + " (Absolute value)");


        guiStyle.fontSize = 15;
        Handles.Label(Vector3.down * 0.2f, "Click + Hold", guiStyle);
        Handles.Label(Vector3.down * 0.4f, "circle this red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);
    }

    public override void Example_4()
    {
        base.Example_4();

        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.fontSize = 15;
        guiStyle.normal.textColor = Color.white;
        //TOP TEXT
        Handles.Label(Vector3.up * 3, "Someone is climbing a Slope", guiStyle);
        Handles.Label(Vector3.up * 2.7f, "", guiStyle);
        //PROJECTION CHART
        guiStyle.fontStyle = FontStyle.Normal;
        Vector3 chartPivot = pivot1 + Vector3.up * 1.5f + Vector3.right * 2.6f;
        Gizmos.color = Color.red;

        Vector3 playerDirection = (Vector3.up * 0.7f + Vector3.right).normalized;
        Vector3 playerVelocity = playerDirection * someScalar;
        Vector3 projectedDirection = RenderToMouse();
        float newScalar = Vector3.Dot(playerVelocity, projectedDirection);
        Vector3 playerNewVelocity = playerDirection * newScalar;

        DrawDotProduct(playerVelocity, projectedDirection);
        Gizmos.DrawLine(player.position, player.position + playerVelocity);

        Handles.Label(pivot1 + Vector3.down * 0.2f + Vector3.left * 0.5f, "Not-Normalized Scale: " + playerVelocity.magnitude);
        Handles.Label(pivot1 + Vector3.down * 0.4f + Vector3.left * 0.5f, "dotProduct / New Velocity: " + Vector3.Dot(playerVelocity, projectedDirection));

        Handles.Label(Vector3.right * 4 + Vector3.down * 0, "playerVelocity = playerDirection * Velocity");
        Handles.Label(Vector3.right * 4 + Vector3.down * 0.3f, "newVelocity = Vector3.dot(playerDirection, slopeNormal)");
        Handles.Label(Vector3.right * 4 + Vector3.down * 0.6f, "adjustedVelocity = playerDirection * newVelocity");

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(player.position + Vector3.down * 0.1f, ( player.position + Vector3.down * 0.1f ) + playerNewVelocity);
        Handles.Label((player.position + Vector3.down * 0.1f) + playerNewVelocity, "Adjusted Velocity");

        guiStyle.fontSize = 15;
        Handles.Label(Vector3.down * 0.2f, "Click + Hold", guiStyle);
        Handles.Label(Vector3.down * 0.4f, "circle this red dot", guiStyle);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 0.1f);
    }


}
