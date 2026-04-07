using JetBrains.Annotations;
using UnityEngine;

public class GenerateGaussian : MonoBehaviour
{
    public float mean = 50.0f;
    public float stdDev = 10.0f;
    public void Test()
    {
        Debug.Log(Generate(mean, stdDev));
    }

    float Generate(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value;
        float u2 = 1.0f - Random.value;

        float randomStdNormal = Mathf.Sin(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
        return mean + stdDev * randomStdNormal;
    }
}
