using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Division : PagesAbstract
{
    public Transform Object1;
    [Range(0, 1)]
    public float scalar;
    [Range(1, 4)]
    public int scaleSize;
    [Range(1, 4)]
    public int dividedPieces;

    private GUIStyle guiStyle;
    private const float TAU = 6.28318530718f;

    private void OnDrawGizmos()
    {
        if (examples == null || examples.Length != 9) examples = new bool[9];
        guiStyle = new GUIStyle();
        ExamplesController();
        Lessons();
    }

    public override void ExamplesController()
    {
        SetTitle("Get back to the Basics", 1, 4);
        SetTitle("Using the piece number to get a set of positions", 5, 6);
        SetTitle("Apply Division as Scalar", 7, 7);
        SetTitle("Interpolating", 8, 9);
        if (currentPage <= 5) Object1.position = new Vector3(12.07f, -4, 0);
        else Object1.position = new Vector3(12.07f, 1, 0);
    }

    public override void Example_8()
    {
        //Top Label
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, 0), "interpolate is transforming a single offset");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.3f), "and divide it by tiny parts");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.6f), "adding them together as little steps");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.9f), "to reach the same goal");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -1.2f), "It can be used to: Smooth Animation or simply getting intervals");

        //Drawing the Scale
        Gizmos.DrawLine((Object1.position), Object1.position + Vector3.up * 2);
        Vector3 topLeft = (Object1.position + Vector3.up * 2) + new Vector3(-0.3f, 0, 0);
        Vector3 topRight = (Object1.position + Vector3.up * 2) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(topLeft, topRight);
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(topRight + new Vector3(0.1f, 0.2f, 0), "Scale: " + 1, guiStyle);
        Vector3 bottomLeft = (Object1.position) + new Vector3(-0.3f, 0, 0);
        Vector3 bottomRight = (Object1.position) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(bottomLeft, bottomRight);


        guiStyle.fontSize = 40;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(Vector3.right * 8 + Vector3.up * 2 + new Vector3(-0.2f, 1.2f, 0), "i", guiStyle);
        Handles.Label(Vector3.right * 8 + Vector3.up * 2 + new Vector3(0, 1.2f, 0), "Scale Size / Division nº");
        Handles.Label(Vector3.right * 8 + Vector3.up * 2 + new Vector3(0, 0.8f, 0), "/", guiStyle);
        Handles.Label(Vector3.right * 8 + Vector3.up * 2 + new Vector3(0.2f, 0.6f, 0), "" + dividedPieces, guiStyle);
        Handles.Label(Vector3.right * 8 + Vector3.up * 2 + new Vector3(0.4f, 0.6f, 0), "Broken down by " + dividedPieces + " pieces");

        //Drawing The Cursor
        for (int i = 0; i <= dividedPieces; i++)
        {
            float divisionPos = (float)i / dividedPieces;
            Vector3 cursorPositionLeft = Object1.position + new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
            Vector3 cursorPositionRight = Object1.position - new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
            Gizmos.DrawLine(cursorPositionLeft, cursorPositionRight);
            Gizmos.DrawSphere((Object1.position) + Vector3.up * 2 * divisionPos, 0.1f);
            Handles.Label(cursorPositionRight + new Vector3(0.2f, 0.2f, 0), "Scalar : " + divisionPos);

            DrawingCircle(dividedPieces);
        }

        Handles.Label(new Vector3(3, 1, 0), "looping all the positions");
        Handles.Label(new Vector3(3, 0.6f, 0), "getting the Scalar at each one and applying them");
        Handles.Label(new Vector3(3, 0.2f, 0), "is getting the interpolated interval");

        void DrawingCircle(int dividedPieces)
        {
            for (int i = 1; i <= dividedPieces; i++)
            {
                float offset = (float) -0.3f * i;
                float interpolation = i / (float)dividedPieces;
                Handles.Label(new Vector3(3, -0.4f + offset, 0),  i + "/4" + " = " + "interpolation " + interpolation);
                float angRad = interpolation * TAU;
                float x = Mathf.Cos(angRad);
                float y = Mathf.Sin(angRad);
                Vector3 point = Vector3.right + new Vector3(x, y);
                Gizmos.DrawSphere(point, 0.1f);
                if (i == dividedPieces)
                    Handles.Label(point + new Vector3(0.1f, 0.3f, 0), interpolation.ToString());
                else Handles.Label(point + new Vector3(0.1f, -0.1f, 0), interpolation.ToString());
            }
            Handles.Label(new Vector3(3, -0.3f, 0), "INTERPOLATION");
            Gizmos.DrawWireSphere(Vector3.right, 1);
        }
    }

    public override void Example_7()
    {
        //Top Label
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, 0), "Select a Scale Size");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.3f), "And the Resulting Scalar will be applyed");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.6f), "To a division of 4");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.9f), "");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -1.2f), "");

        //Drawing the Scale
        Gizmos.DrawLine((Object1.position), Object1.position + Vector3.up * 2);
        Vector3 topLeft = (Object1.position + Vector3.up * 2) + new Vector3(-0.3f, 0, 0);
        Vector3 topRight = (Object1.position + Vector3.up * 2) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(topLeft, topRight);
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(topRight + new Vector3(0.1f, 0.2f, 0), "Scale: " + 1, guiStyle);
        Vector3 bottomLeft = (Object1.position) + new Vector3(-0.3f, 0, 0);
        Vector3 bottomRight = (Object1.position) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(bottomLeft, bottomRight);


        guiStyle.fontSize = 40;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(Vector3.right * 8 + Vector3.up * 2 + new Vector3(-0.2f, 1.2f, 0), "" + scaleSize, guiStyle);
        Handles.Label(Vector3.right * 8 + Vector3.up * 2 + new Vector3(0, 1.2f, 0), "Scale Size / Division nº");
        Handles.Label(Vector3.right * 8 + Vector3.up * 2 + new Vector3(0, 0.8f, 0), "/", guiStyle);
        Handles.Label(Vector3.right * 8 + Vector3.up * 2 + new Vector3(0.2f, 0.6f, 0), "4", guiStyle);
        Handles.Label(Vector3.right * 8 + Vector3.up * 2 + new Vector3(0.4f, 0.6f, 0), "Broken down by " + 4 + " pieces");

        //Drawing Scale Cursor
        float scalarResult = (float)scaleSize / 4;
        Vector3 cursorPositionLeft = Object1.position + new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * scalarResult;
        Vector3 cursorPositionRight = Object1.position - new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * scalarResult;
        Gizmos.DrawLine(cursorPositionLeft, cursorPositionRight);
        Gizmos.DrawSphere((Object1.position) + Vector3.up * 2 * scalarResult, 0.1f);
        Handles.Label(cursorPositionRight + new Vector3(0.2f, 0.2f, 0), "Scalar Result: " + scalarResult);
        
        DrawingCircle();
        DrawingVectorD();

        void DrawingCircle()
        {
            Handles.Label(new Vector3(2, 1, 0), "TAU * Scalar Result");
            Handles.Label(new Vector3(2, 0.6f, 0), "    (TAU is 1 full circle)");
            float angRad = scalarResult * TAU;
            float x = Mathf.Cos(angRad);
            float y = Mathf.Sin(angRad);
            Vector3 point = Vector3.right + new Vector3(x, y);
            Gizmos.DrawSphere(point, 0.1f);

            Handles.Label(point + new Vector3(0.1f, -0.1f, 0), ""+ scalarResult);
            Gizmos.DrawWireSphere(Vector3.right, 1);
        }

        void DrawingVectorD()
        {
            Vector3 vectorA = new Vector3(6, -4, 0);
            Vector3 vectorB = new Vector3(9, 0, 0);
            Vector3 vectorcDist = vectorB - vectorA;
            Gizmos.DrawLine(vectorA, vectorA + vectorcDist);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(vectorA, 0.2f);
            Handles.Label(vectorA + new Vector3(0.1f, -0.1f, 0), "ScalarResult * Vector");
            Gizmos.color = Color.white;

            Gizmos.DrawSphere(vectorA + vectorcDist * scalarResult, 0.2f);
            Handles.Label((vectorA + vectorcDist * scalarResult) + new Vector3(0.2f, 0, 0), "" + scalarResult);

            //Handles.Label(vectorC * scalarResult + new Vector3(0.1f, -0.1f, 0), "" + scalarResult);
        }
    }

    public override void Example_6()
    {
        //Top Label
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, 0), "To break down a Scale to several pieces");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.3f), "We set the division (Pieces) the number we want");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.6f), "SET THE NUMBER OF Divisions -> Slider DividedPieces");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.9f), "");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -1.2f), "");

        //Drawing the Scale
        Gizmos.DrawLine((Object1.position), Object1.position + Vector3.up * 2);
        Vector3 topLeft = (Object1.position + Vector3.up * 2) + new Vector3(-0.3f, 0, 0);
        Vector3 topRight = (Object1.position + Vector3.up * 2) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(topLeft, topRight);
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(topRight + new Vector3(0.1f, 0.2f, 0), "Scale: " + 1, guiStyle);
        Vector3 bottomLeft = (Object1.position) + new Vector3(-0.3f, 0, 0);
        Vector3 bottomRight = (Object1.position) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(bottomLeft, bottomRight);


        guiStyle.fontSize = 30;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(Vector3.right + new Vector3(-2f, 1.2f, 0), "for loop", guiStyle);
        Handles.Label(Vector3.right + new Vector3(0, 1.2f, 0), "Scale Size / Division nº");
        Handles.Label(Vector3.right + new Vector3(1, 0.6f, 0), "/", guiStyle);
        Handles.Label(Vector3.right + new Vector3(1.2f, 0.4f, 0), "" + dividedPieces, guiStyle);
        Handles.Label(Vector3.right + new Vector3(1.4f, 0.6f, 0), "Broken down by " + dividedPieces + " pieces");

        //Drawing The Cursor
        for (int i = 0; i <= dividedPieces; i++)
        {
            float divisionPos = (float)i / dividedPieces;
            Vector3 cursorPositionLeft = Object1.position + new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
            Vector3 cursorPositionRight = Object1.position - new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
            Gizmos.DrawLine(cursorPositionLeft, cursorPositionRight);
            Gizmos.DrawSphere((Object1.position) + Vector3.up * 2 * divisionPos, 0.1f);
            Handles.Label(cursorPositionRight + new Vector3(0.2f, 0.2f, 0), "Scalar : " + divisionPos);
        }

        guiStyle.fontSize = 25;
        Handles.Label(Vector3.right + new Vector3(3.5f, 0f, 0), "(int i = 0; i <= dividedPieces; i++)", guiStyle);
        Handles.Label(Vector3.right + new Vector3(3f, -0.8f, 0), "     float divisionPos = (float)i / dividedPieces;", guiStyle);

        //Down Label
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, 0), "With a for loop");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.3f), "we can get all the positions of the division");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.6f), "And use that positions to use as scalars applying to anything");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -1.5f), "**Starting using the word  DIVISION instead of PIECES, now the concept behind it is Explained");
    }

    public override void Example_5()
    {
        //Top Label
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, 0), "If the Scale Size is greater than the nº of broken pieces");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.3f), "Means one piece is in offset the scale");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.6f), "and the resulting scalar will be greater than 1 scale");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.9f), " ");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -1.2f), "MOVE SLIDERS - Scale Size, DividedPieces");

        //Drawing the Scale
        Gizmos.DrawLine((Object1.position), Object1.position + Vector3.up * 2);
        Vector3 topLeft = (Object1.position + Vector3.up * 2) + new Vector3(-0.3f, 0, 0);
        Vector3 topRight = (Object1.position + Vector3.up * 2) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(topLeft, topRight);
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(topRight + new Vector3(0.1f, 0.2f, 0), "Scale: " + 1, guiStyle);
        Vector3 bottomLeft = (Object1.position) + new Vector3(-0.3f, 0, 0);
        Vector3 bottomRight = (Object1.position) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(bottomLeft, bottomRight);

        //Drawing The Cursor
        float divisionPos = (float)scaleSize / dividedPieces;
        Vector3 cursorPositionLeft = Object1.position + new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
        Vector3 cursorPositionRight = Object1.position - new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
        Gizmos.DrawLine(cursorPositionLeft, cursorPositionRight);
        Gizmos.DrawSphere((Object1.position) + Vector3.up * 2 * divisionPos, 0.1f);

        if (scaleSize > dividedPieces)
        {
            GUI.color = Color.green;
            Handles.Label(Vector3.right + new Vector3(0, 1.2f, 0), "At least ONE PIECE is higher than Scale (Unity)");
            Handles.Label(Vector3.right + new Vector3(0.5f, 0.8f, 0), scaleSize + " IS HIGHER THAN " + dividedPieces + " = More than 1", guiStyle);
        }
        else if (scaleSize <= dividedPieces)
        {
            GUI.color = Color.white;
            Handles.Label(Vector3.right + new Vector3(0, 1.2f, 0), "Scale Size / Piece nº");
            Handles.Label(Vector3.right + new Vector3(0.5f, 0.8f, 0), scaleSize + " IS LOWER OR EQUAL THAN " + dividedPieces + " = 0 To 1", guiStyle);

        }

        guiStyle.fontSize = 40;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(Vector3.right + new Vector3(-0.2f, 1.2f, 0), "" +scaleSize, guiStyle);
        Handles.Label(cursorPositionRight + new Vector3(0.2f, 0.2f, 0), "Scalar : " + divisionPos);
        Handles.Label(Vector3.right + new Vector3(0, 0.8f, 0), "/", guiStyle);
        Handles.Label(Vector3.right + new Vector3(0.2f, 0.6f, 0), "" + dividedPieces, guiStyle);
        Handles.Label(Vector3.right + new Vector3(0.4f, -0.4f, 0), "Broken down by " + dividedPieces + " parts");

        //Down Label
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, 0), "Between 0 and 1, is being inside the scale of something");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.3f), " ");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.6f), "1/4, 2/4, 3/4, 4/4 -> Scalar: 0.0 to 1 of a scale (inside scale)");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.9f), "5/4 or more -> greater than 1 unity (outside scale)");

        for (int i = 1; i <= scaleSize; i++)
        {
            DrawScale(i);
        }

        //Drawing The Cursor
        Vector3 positionOffset = Object1.position + Vector3.right * 3;
        divisionPos = (float)scaleSize / dividedPieces;
        cursorPositionLeft = positionOffset + new Vector3(-0.3f, 0, 0) + Vector3.up * (scaleSize * 2) * divisionPos;
        cursorPositionRight = positionOffset - new Vector3(-0.3f, 0, 0) + Vector3.up * (scaleSize * 2) * divisionPos;
        Gizmos.DrawLine(cursorPositionLeft, cursorPositionRight);
        Gizmos.DrawSphere((positionOffset) + Vector3.up * (scaleSize * 2) * divisionPos, 0.1f);
        Handles.Label(cursorPositionRight + new Vector3(0.2f, 0.2f, 0), "Scalar : " + divisionPos);
    }

    public override void Example_4()
    {
        //Top Label
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, 0), "As you change the number of the piece");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.3f), "you get the piece position as a scale");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.6f), "");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.9f), "-> MOVE SLIDER - Scale Size");

        //Drawing the Scale
        Gizmos.DrawLine((Object1.position), Object1.position + Vector3.up * 2);
        Vector3 topLeft = (Object1.position + Vector3.up * 2) + new Vector3(-0.3f, 0, 0);
        Vector3 topRight = (Object1.position + Vector3.up * 2) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(topLeft, topRight);
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(topRight + new Vector3(0.1f, 0.2f, 0), "Scale: " + 1, guiStyle);
        Vector3 bottomLeft = (Object1.position) + new Vector3(-0.3f, 0, 0);
        Vector3 bottomRight = (Object1.position) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(bottomLeft, bottomRight);

        //Drawing The Cursor
        float divisionPos = (float)scaleSize / 4;
        Vector3 cursorPositionLeft = Object1.position + new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
        Vector3 cursorPositionRight = Object1.position - new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
        Gizmos.DrawLine(cursorPositionLeft, cursorPositionRight);
        Gizmos.DrawSphere((Object1.position) + Vector3.up * 2 * divisionPos, 0.1f);
        Handles.Label(cursorPositionRight + new Vector3(0.2f, 0.2f, 0), "Scalar : " + divisionPos);

        guiStyle.fontSize = 40;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(Vector3.right + new Vector3(-0.2f, 1.2f, 0), "" + scaleSize, guiStyle);
        Handles.Label(Vector3.right + new Vector3(0, 1.2f, 0), "Scale Size / Piece nº");
        Handles.Label(Vector3.right + new Vector3(0, 0.8f, 0), "/", guiStyle);
        Handles.Label(Vector3.right + new Vector3(0.2f, 0.6f, 0), "" + 4, guiStyle);
        Handles.Label(Vector3.right + new Vector3(0.4f, 0.6f, 0), "Broken down by " + dividedPieces + " parts");

        //Down Label
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, 0), "Notice that if you double the scale");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.3f), "you double the piece size (scalar) as well");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.6f), "And get the position of the piece (scalar) nº " + scaleSize);
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.9f), "That's how it's works");

        for (int i = 1; i <= scaleSize; i++)
        {
            DrawScale(i);
        }

        //Drawing The Cursor
        Vector3 positionOffset = Object1.position + Vector3.right * 3;
        divisionPos = (float)scaleSize / 4;
        cursorPositionLeft = positionOffset + new Vector3(-0.3f, 0, 0) + Vector3.up * (scaleSize * 2) * divisionPos;
        cursorPositionRight = positionOffset - new Vector3(-0.3f, 0, 0) + Vector3.up * (scaleSize * 2) * divisionPos;
        Gizmos.DrawLine(cursorPositionLeft, cursorPositionRight);
        Gizmos.DrawSphere((positionOffset) + Vector3.up * (scaleSize * 2) * divisionPos, 0.1f);
        Handles.Label(cursorPositionRight + new Vector3(0.2f, 0.2f, 0), "Scalar : " + divisionPos);
    }

    public override void Example_3()
    {
        //Top Label
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, 0), "It's a double standard");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.3f), "we can see 1 as the Scale Size");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.6f), "Or the piece we are on");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.9f), "-> MOVE SLIDER - Divided Pieces");

        //Drawing the Scale
        Gizmos.DrawLine((Object1.position), Object1.position + Vector3.up * 2);
        Vector3 topLeft = (Object1.position + Vector3.up * 2) + new Vector3(-0.3f, 0, 0);
        Vector3 topRight = (Object1.position + Vector3.up * 2) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(topLeft, topRight);
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(topRight + new Vector3(0.1f, 0.2f, 0), "Scale: " + 1, guiStyle);
        Vector3 bottomLeft = (Object1.position) + new Vector3(-0.3f, 0, 0);
        Vector3 bottomRight = (Object1.position) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(bottomLeft, bottomRight);

        //Drawing The Cursor
        float divisionPos = (float)1 / dividedPieces;
        Vector3 cursorPositionLeft = Object1.position + new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
        Vector3 cursorPositionRight = Object1.position - new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
        Gizmos.DrawLine(cursorPositionLeft, cursorPositionRight);
        Gizmos.DrawSphere((Object1.position) + Vector3.up * 2 * divisionPos, 0.1f);
        Handles.Label(cursorPositionRight + new Vector3(0.2f, 0.2f, 0), "Scalar : " + divisionPos);

        guiStyle.fontSize = 40;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(Vector3.right + new Vector3(-0.2f, 1.2f, 0), "1", guiStyle);
        Handles.Label(Vector3.right + new Vector3(0, 1.2f, 0), "Scale Size / Piece nº");
        Handles.Label(Vector3.right + new Vector3(0, 0.8f, 0), "/", guiStyle);
        Handles.Label(Vector3.right + new Vector3(0.2f, 0.6f, 0), "" + dividedPieces, guiStyle);
        Handles.Label(Vector3.right + new Vector3(0.4f, 0.6f, 0), "Broken down by " + dividedPieces + " pieces");

        //Down Label
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, 0), "The number of the piece we are on: 1");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.3f), "It's 1 Out of " + dividedPieces + " broken pieces");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.6f), "Or simply: 1/" + dividedPieces);
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.9f), "**This logic works only if the Scale is less or equal thabn the number of pieces");
    }

    public override void Example_2()
    {
        //Top Label
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, 0), "With Divide, we can get a exact position from a scale");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.3f), "providing the Scale Size");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.6f), "broken down by X divisions");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.9f), "The result is a Scalar the size of one piece");

        //Drawing the Scale
        Gizmos.DrawLine((Object1.position), Object1.position + Vector3.up * 2);
        Vector3 topLeft = (Object1.position + Vector3.up * 2) + new Vector3(-0.3f, 0, 0);
        Vector3 topRight = (Object1.position + Vector3.up * 2) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(topLeft, topRight);
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(topRight + new Vector3(0.1f, 0.2f,0), "Scale: 1", guiStyle);
        Vector3 bottomLeft = (Object1.position) + new Vector3(-0.3f, 0, 0);
        Vector3 bottomRight = (Object1.position) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(bottomLeft, bottomRight);

        //Drawing The Cursor
        float divisionPos = (float)1 / 2;
        Vector3 cursorPositionLeft = Object1.position + new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
        Vector3 cursorPositionRight = Object1.position - new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * divisionPos;
        Gizmos.DrawLine(cursorPositionLeft, cursorPositionRight);
        Gizmos.DrawSphere((Object1.position) + Vector3.up * 2 * divisionPos, 0.1f);
        Handles.Label(cursorPositionRight + new Vector3(0.2f, 0.2f, 0), "Scalar : " + divisionPos);

        guiStyle.fontSize = 40;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(Vector3.right + new Vector3(-0.2f,1.2f,0), "1", guiStyle);
        Handles.Label(Vector3.right + new Vector3(0,1.2f,0), "Scale Size / Piece nº");
        Handles.Label(Vector3.right + new Vector3(0, 0.8f, 0), "/", guiStyle);
        Handles.Label(Vector3.right + new Vector3(0.2f, 0.6f, 0), "2", guiStyle);
        Handles.Label(Vector3.right + new Vector3(0.4f, 0.6f, 0), "Broken down by two pieces");

        //Down Label
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, 0), "If you broken 1 Pizza into 2 pieces");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.3f), "The 2 pieces together is equal to 1 Pizza");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.6f), "But the piece we are on is the first one (1/)");
        Handles.Label(Vector3.down * 2 + new Vector3(0.1f, -0.9f), "out of 2 pieces (/2), so the result is the size of the first: 0.5");
    }

    public override void Example_1()
    {
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, 0), "We have a scale, it could anything, a pizza, a circle, a radian, etc.");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.3f), "We can get any position from it by multiplying by a scalar");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.6f), "But if we want to get a exact position from the scalar and apply to the scale");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -0.9f), "We should use Division to get that scalar position");
        Handles.Label(Vector3.up * 2.8f + new Vector3(0.1f, -1.2f), "MOVE SLIDER - Scalar ");

        Gizmos.DrawSphere((Object1.position) + Vector3.up * 2 * scalar, 0.1f);
        Gizmos.DrawLine((Object1.position), Object1.position  + Vector3.up * 2);

        Vector3 scalarPositionLeft = Object1.position + new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * scalar;
        Vector3 scalarPositionRight = Object1.position - new Vector3(-0.3f, 0, 0) + Vector3.up * 2 * scalar;
        Gizmos.DrawLine(scalarPositionLeft, scalarPositionRight);

        Handles.Label(scalarPositionRight + new Vector3(0.2f,0,0), "Scalar : " + scalar);
    }

    private void DrawScale(int scaleNumber)
    {
        scaleNumber *= 2;
        Vector3 positionOffset = Object1.position + Vector3.right * 3;
        Vector3 drawScale = positionOffset + Vector3.up * scaleNumber;
        Gizmos.DrawLine((positionOffset), drawScale);
        Vector3 topLeft = (drawScale) + new Vector3(-0.3f, 0, 0);
        Vector3 topRight = (drawScale) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(topLeft, topRight);
        guiStyle.fontSize = 15;
        guiStyle.fontStyle = FontStyle.BoldAndItalic;
        Handles.Label(topRight + new Vector3(0.1f, 0.2f, 0), "Scale: " + scaleNumber/2, guiStyle);
        Vector3 bottomLeft = (positionOffset) + new Vector3(-0.3f, 0, 0);
        Vector3 bottomRight = (positionOffset) + new Vector3(0.3f, 0, 0);
        Gizmos.DrawLine(bottomLeft, bottomRight);
    }
}