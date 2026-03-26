using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public PlayerController playerScript;
    public float viewAngle = 60f;
    public float maxDistance = 10f;
    public float stopDistance = 1.2f;
    public float dashSpeed = 15f;

    private bool isDetected = false;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (!isDetected && distance <= maxDistance)
        {
            Vector3 forward = transform.forward;
            Vector3 toPlayer = (player.position - transform.position).normalized;
            float dot = (forward.x * toPlayer.x) + (forward.y * toPlayer.y) + (forward.z * toPlayer.z);
            float angle = Mathf.Acos(Mathf.Clamp(dot, -1f, 1f)) * Mathf.Rad2Deg;

            if (angle < viewAngle * 0.5f)
            {
                isDetected = true;
            }
        }

        if (isDetected)
        {
            MoveAndCheckParry(distance);
        }
        else
        {
            transform.Rotate(0, 45 * Time.deltaTime, 0);
        }
    }

    void MoveAndCheckParry(float distance)
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        transform.forward = new Vector3(toPlayer.x, 0, toPlayer.z);

        if (distance > stopDistance)
        {
            transform.position += transform.forward * dashSpeed * Time.deltaTime;
        }
        else
        {
            
            Vector3 forward = transform.forward;

            
            float crossY = (forward.z * toPlayer.x) - (forward.x * toPlayer.z);
            
            if (crossY > 0)
            {
                if (playerScript.isRightParring) Destroy(gameObject);
                else RestartScene();
            }
            else
            {
                if (playerScript.isLeftParring) Destroy(gameObject);
                else RestartScene();
            }
        }
    }

    void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
