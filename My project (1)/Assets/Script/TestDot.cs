using UnityEngine;

public class TestDot : MonoBehaviour
{
    public Transform player;
    public float viewAngle = 60f;



    // Update is called once per frame
    void Update()
    {

        Vector3 toPlayer = (player.position -transform.position).normalized;
        Vector3 forward = transform.forward;
        float dot = (toPlayer.x * forward.x + toPlayer.y * forward.y + toPlayer.z * forward.z);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (angle < viewAngle / 2)
        {
            Debug.Log("플레이어가 시야 안에 있음!");
        }
    }
}
