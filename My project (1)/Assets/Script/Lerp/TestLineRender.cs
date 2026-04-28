using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;



[RequireComponent(typeof(LineRenderer))]
public class TestLineRender : MonoBehaviour
{
    public Transform startPos; 
    [Header("Settings")]
    [Range(1f, 5f)] public float extend = 1.5f;
    public float rotationSpeed = 5f; 

    private LineRenderer lr;
    private Transform targetEnemy; 

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.widthMultiplier = 0.05f;
        lr.material = new Material(Shader.Find("Unlit/Color")) { color = Color.red };

        lr.enabled = false;
    }

    public void OnRightClick(InputValue value)
    {
        if (!value.isPressed) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("利 鸥百泼 肯丰: " + hit.collider.name);
                targetEnemy = hit.transform; 
                lr.enabled = true; 
            }
        }
        else
        {
            Debug.Log("鸥百 秦力");
            targetEnemy = null;
            lr.enabled = false;
        }
    }

    void Update()
    {
        if (targetEnemy == null || !startPos) return;

        Vector3 direction = targetEnemy.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 a = startPos.position;
        Vector3 b = targetEnemy.position;

        Vector3 extendedPoint = Vector3.LerpUnclamped(a, b, extend);

        lr.SetPosition(0, a);
        lr.SetPosition(1, extendedPoint);
    }
}
