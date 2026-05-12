
using UnityEngine;
using System.Collections.Generic;

public class BezierDe : MonoBehaviour
{
    public Transform p0;
    public Transform p3;

    [Header("Random Ranges")]
    public float p1Radius = 2f;
    public float p2Radius = 2f;
    public float p1Height = 3f;
    public float p2Height = 3f;

    [HideInInspector] public Vector3 p1;
    [HideInInspector] public Vector3 p2;

    List<Vector3> points;
    float time = 0f;
    bool isInitialized = false;

    public void Init(Transform start, Transform target)
    {
        p0 = start;
        p3 = target;

        GenerateRandomControlPoints();
        points = new List<Vector3> { p0.position, p1, p2, p3.position };

        isInitialized = true;
    }

        void Update()
    {
        if (!isInitialized) return;

        time += Time.deltaTime / 2f;

        if (time <= 1.0f)
        {
            transform.position = DeCasteljau(new List<Vector3>(points), time);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void GenerateRandomControlPoints()
    {
        Vector2 rand1 = Random.insideUnitCircle * p1Radius;
        p1 = p0.position + new Vector3(rand1.x, 0f, rand1.y);
        p1.y += p1Height;

        Vector2 rand2 = Random.insideUnitCircle * p2Radius;
        p2 = p3.position + new Vector3(rand2.x, 0f, rand2.y);
        p2.y+= p2Height;
    }

    Vector3 DeCasteljau(List<Vector3> p, float t)
    {
        while (p.Count > 1)
        {
            int last = p.Count - 1;

            var next = new List<Vector3>(last);
            for (int i = 0; i < last; i++)
                next.Add(Vector3.Lerp(p[i], p[i + 1], t));
            p = next;
        }

        return p[0];
    }

}
