using UnityEngine;

public class Bezier : MonoBehaviour
{
    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;

    float timeValue = 0f;


    // Update is called once per frame
    void Update()
    {
        timeValue += Time.deltaTime / 2f;
        transform.position = GetPointOnBezierCurve(point0.position, point1.position, point2.position, point3.position, timeValue);
    }

    Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        /*Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 abc = Vector3.Lerp(ab, bc, t);

        return abc;*/

        return Mathf.Pow(1 - t, 3) * p0 + Mathf.Pow(1 - t, 2) * 3 * t * p1 + Mathf.Pow(t, 2) * 3 * (1 - t) * p2 + Mathf.Pow(t, 3) *  p3;
    }
}
