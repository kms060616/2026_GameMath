using UnityEngine;

public class Fov : MonoBehaviour
{
   public float rayLength = 2.0f;
    public Color GizmoColor = Color.blue;
    private void OnDrawGizmos()
    {
        DrawForwardRay();
    }
    private void DrawForwardRay()
    {
        Vector3 startPos = transform.position;
        Vector3 forwardDir = transform.forward * rayLength;
        Vector3 endPos = startPos + forwardDir;

        Gizmos.color = GizmoColor;
        Gizmos.DrawRay(startPos, forwardDir);
    }
}
