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
    public void OnPoint(InputValue value)
    {
        MouseScreenPosition = value.Get<Vector2>();
    }

    public void OnClik(InputValue value)
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


    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Vector3 direction = new Vector3(targetPosition.x, transform.position.y , targetPosition.z);
            transform.Translate(direction * moveSpeed * Time.deltaTime);


            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
}
