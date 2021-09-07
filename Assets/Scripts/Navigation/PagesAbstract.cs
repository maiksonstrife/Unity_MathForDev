using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PagesAbstract : MonoBehaviour
{
    [SerializeField]
    protected int currentPage;
    [SerializeField]
    protected bool next;
    [SerializeField]
    protected bool previous;
    protected bool[] examples;
    private int lastChapterEnd;

    protected void SetTitle(string title, int chapterInit, int chapterend)
    {
        if (currentPage < chapterInit || currentPage > chapterend) return;
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;
        Handles.Label(Vector3.up * 4, "Page " + currentPage.ToString(), guiStyle);
        Handles.Label(Vector3.up * 4 + new Vector3(0, -0.6f), title, guiStyle);
    }

    protected void OnValidate()
    {
        if (next)
        {
            previous = false;
            currentPage++;
            if (currentPage >= examples.Length) currentPage = examples.Length;
            ExamplesController(currentPage, true);
            next = false;
        }

        if (previous)
        {
            next = false;
            currentPage--;
            if (currentPage == -1) currentPage = 0;
            ExamplesController(currentPage, false);
            previous = false;
        }
    }

    private void ExamplesController(int exampleNumber, bool isNext)
    {
        if (!isNext)
        {
            for (int i = exampleNumber; i < examples.Length; i++)
            {
                examples[i] = false;
            }
        }

        if (isNext)
        {
            for (int i = 0; i < exampleNumber; i++)
            {
                examples[i] = true;
            }
        }
    }

    protected Vector3 RenderToMouse()
    {
        #region Mouse To World Position

        Vector2 MousePos = Event.current.mousePosition;
        float ppp = EditorGUIUtility.pixelsPerPoint;
        MousePos.y = SceneView.lastActiveSceneView.camera.pixelHeight - MousePos.y * ppp;
        MousePos.x *= ppp;

        Vector2 MouseWorldPos = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(MousePos);

        Vector2 HalfWorldPos = MouseWorldPos * 0.5f;

        Vector2 mouseOffset = Event.current.mousePosition + new Vector2(12, 12);
        mouseOffset.y = SceneView.lastActiveSceneView.camera.pixelHeight - mouseOffset.y * ppp;
        mouseOffset.x *= ppp;
        Vector2 mouseOffsetWorld = SceneView.lastActiveSceneView.camera.ScreenToWorldPoint(mouseOffset);

        #endregion

        Vector2 MouseWorldRay = MouseWorldPos - Vector2.zero;

        return MouseWorldRay.normalized;
    }
}
