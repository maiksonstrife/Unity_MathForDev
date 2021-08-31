using UnityEngine;

public class LocalToWorld : MonoBehaviour
{
    [Header ("World To Local")]
    public Vector2 spacePoint;
    public Vector3 right;
    public Vector3 up;
    public Vector2 offset;
    public float localScaleX;
    public float localScaleY;
    [Header("Local To World")]
    public Vector2 worldSpacePoint;
    public Vector2 relactivePoint;
    public float worldScaleX;
    public float worldScaleY;
    public Transform localObjTransform;

    public Transform objectToProject;


    private void OnDrawGizmos()
    {
        Vector2 LocalObjPos = transform.position;
        right = transform.right;
        up = transform.up;


        Vector2 worldToLocal = WorldToLocal(spacePoint);
        localScaleX = Vector2.Dot(LocalObjPos, right);
        localScaleY = Vector2.Dot(LocalObjPos, up);


        DrawBasisVectors(LocalObjPos, right, up);
        DrawBasisVectors(Vector2.zero, Vector2.right, Vector2.up);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere((Vector3)worldToLocal, 0.2f);

        localObjTransform.localPosition = LocalToWorld(worldSpacePoint);
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(relactivePoint, 0.2f);


        Vector2 WorldToLocal(Vector2 spacePoint)
        {
            offset = right * spacePoint.x + up * spacePoint.y;
            return (Vector2)transform.position + offset;
        }

        Vector2 LocalToWorld(Vector2 worldSpacePoint)
        {
            relactivePoint = worldSpacePoint - LocalObjPos;
            Gizmos.DrawRay(LocalObjPos, relactivePoint);

            worldScaleX = Vector2.Dot(relactivePoint, right);
            worldScaleY = Vector2.Dot(relactivePoint, up);
            return new Vector2(worldScaleX, worldScaleY);
        }

        moveAlongXAxix(objectToProject.position);
    }

    void moveAlongXAxix(Vector3 objectToProject)
    {
        Gizmos.color = Color.red;
        float scaleX = Vector3.Dot(objectToProject, Vector2.right);
        float scaleY = Vector3.Dot(objectToProject, Vector2.up);
        Gizmos.DrawRay(Vector2.zero, Vector2.right * scaleX + Vector2.up * scaleY);

        scaleX = Vector3.Dot(Vector3.right, transform.position.normalized);
        Vector3 projectedDirection = transform.position.normalized * scaleX;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(Vector3.zero, transform.position.normalized);
        //World Space Vectors
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position.normalized, Vector3.right);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position.normalized, Vector3.up);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position.normalized, Vector3.forward);

        Gizmos.DrawRay(transform.position.normalized, Vector3.right - projectedDirection);
        Gizmos.DrawSphere(Vector3.right - transform.position.normalized, 0.1f);

    }

    void DrawBasisVectors(Vector2 pos, Vector2 right, Vector2 up)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(pos, right);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(pos, up);
        Gizmos.color = Color.white;
    }
}
