using UnityEngine;

public class DotExample : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 toPlayer = player.position - transform.position;

        toPlayer.y = 0;

        Vector3 forward = transform.forward;
        forward.y = 0;

        forward.Normalize();
        toPlayer.Normalize();

        float dot = Vector3.Dot(forward, toPlayer);

        if (dot > 0f)
        {
            Debug.Log("플레이어가 적앞");
        }
        else if (dot < 0f) { Debug.Log("플레이어가 적 뒤"); }
        else { Debug.Log("플레이어가 적 옆"); }
    }
}
