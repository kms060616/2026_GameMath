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
        if (!value.isPressed || BollGameManager.Instance.isGameOver || BollGameManager.Instance.isBallMoving)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Rigidbody targetRb = hit.collider.attachedRigidbody;

            if (targetRb != null)
            {
                string targetTag = targetRb.gameObject.tag;
                int currentTurn = BollGameManager.Instance.currentTurn;

                if ((currentTurn == 1 && targetTag != "Player1") || (currentTurn == 2 && targetTag != "Player2"))
                {
                    Debug.Log("자신의 공만 칠 수 있습니다!");
                    return;
                }

                Vector3 hitPoint = hit.point;
                Vector3 center = targetRb.gameObject.transform.position;
                Vector3 forceDirection = center - hitPoint;
                forceDirection.y = 0f;
                forceDirection.Normalize();
                targetRb.AddForce(forceDirection * forcePower, ForceMode.Impulse);
                BollGameManager.Instance.StartTurnAction();
            }
        }
    }
}
