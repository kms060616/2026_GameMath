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

    private void Start()
    {
        float degrees = 45f;
        float radians = degrees * Mathf.Deg2Rad;
        Debug.Log("45도 -> 라디안 : " + radians);

        float radiansValue = Mathf.PI / 3;
        float degreesValue = radiansValue * Mathf.Deg2Rad;
        Debug.Log("파이/3 라디안 -> 도 변환 : " + degreesValue);
    }


    // Update is called once per frame
    void Update()
    {
        float speed = 5f;
        float angle = 30f;//이동할 방향 도 단위
        float radians = angle * Mathf.Deg2Rad;


        Vector3 direction = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
