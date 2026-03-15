using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private bool isMoving = false;

    Vector2 MouseScreenPosition;
    Vector3 targetPosition;

    private bool isSprinting = false;
    public void OnPoint(InputValue value)
    {
        MouseScreenPosition = value.Get<Vector2>();
    }

    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(MouseScreenPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != gameObject)
                {
                    targetPosition = hit.point;
                    targetPosition.y = transform.position.y;
                    isMoving = true;
                    break;
                }
            }
        }
        
    }
    
    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isMoving) return;

        
        Vector3 direction = targetPosition - transform.position;

       
        float sqrDistance = direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;

        if (sqrDistance > 0.001f)
        {
            float distance = Mathf.Sqrt(sqrDistance);
            Vector3 normalizedDirection = direction / distance;

            float currentSpeed = isSprinting ? moveSpeed * 2f : moveSpeed;

            transform.position += normalizedDirection * currentSpeed * Time.deltaTime;

            if (normalizedDirection != Vector3.zero)
            {
                transform.forward = normalizedDirection;
            }
        }
        else
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
}
