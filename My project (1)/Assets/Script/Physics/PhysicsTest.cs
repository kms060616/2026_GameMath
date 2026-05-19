using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsTest : MonoBehaviour
{
    public float forcePower = 10f; 
    private Rigidbody rb;
    [SerializeField] private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * forcePower, ForceMode.Impulse);
    }



    void Update()
    {
        speed = rb.linearVelocity.magnitude;
    }
}
