using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRaycastTest : MonoBehaviour
{
    public float rayDistance = 100f;
    public CamearOrbit cam;
    float moveInput;
    private Rigidbody rb;
    public float forcePower = 10f;
    

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
        cam.moveInput = moveInput;
    }

    public void OnClick(InputValue value)
    {
        if (!value.isPressed)
            return;
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;


            if (rb != null)
            {
                Vector3 hitPoint = hit.point;
                Vector3 center = rb.gameObject.transform.position;
                Vector3 forceDirection = center - hitPoint;
                forceDirection.y = 0f;
                forceDirection.Normalize();


                rb = GetComponent<Rigidbody>();
                rb.AddForce(forceDirection * forcePower, ForceMode.Impulse);

                
            }
        }
    }
}
