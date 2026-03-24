using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private Vector2 moveInput;
    public float moveSpeed = 5.0f;
    public bool isLeftParring = false;
    public bool isRightParring = false;

    public void OnLeftParring(InputValue value)
    {
        isLeftParring = value.isPressed;
        if (isLeftParring == true)
        {
            Debug.Log("ÆÐ¸µÁß");
        }
    }
    public void OnRightParring(InputValue value)
    {
        isRightParring = value.isPressed;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        Quaternion rotation = Quaternion.Euler(0f, moveInput.x * rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = rotation * transform.rotation;

        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
