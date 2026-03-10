using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Vector2 moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
