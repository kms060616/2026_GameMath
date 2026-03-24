using Unity.VisualScripting;
using UnityEngine;

public class White : MonoBehaviour
{
    public Transform player;
    public float viewAngle = 60f;
    public float maxDistance = 10f;
    public float stopDistance = 1.2f;
    public float dashSpeed = 15f;




    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 45 * Time.deltaTime, 0);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= maxDistance)
        {
            Vector3 toPlayer = (player.position - transform.position).normalized;
            Vector3 forward = transform.forward;
            float dot = (toPlayer.x * forward.x + toPlayer.y * forward.y + toPlayer.z * forward.z);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            if (angle < viewAngle / 2)
            {
                Debug.Log("플레이어가 시야 안에 있음!");
            }

        }
           

        
    }
}
