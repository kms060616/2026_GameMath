using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    public Transform centerObject;
    public float orbitRadius = 5f; 
    public float orbitSpeed = 2f;  

    private float currentAngle = 0f;

    void Update()
    {
        if (centerObject == null) return;

        
        currentAngle += orbitSpeed * Time.deltaTime;

        float x = Mathf.Cos(currentAngle) * orbitRadius;
        float z = Mathf.Sin(currentAngle) * orbitRadius;

        transform.position = centerObject.position + new Vector3(x, 0, z);
    }
}
