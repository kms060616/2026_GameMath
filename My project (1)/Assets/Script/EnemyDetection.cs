using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 10f;
    public float viewAngle = 30f;
    public float detectScale = 2f;

    private Vector3 originScale;

    void Start()
    {
        originScale = transform.localScale;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= maxDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);
            float cosThreshold = Mathf.Cos(viewAngle * Mathf.Deg2Rad);

            if (dotProduct >= cosThreshold)
            {
                transform.localScale = Vector3.one * detectScale;
            }
            else
            {
                transform.localScale = originScale;
            }
        }
        else
        {
            transform.localScale = originScale;
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle, 0) * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftBoundary * maxDistance);
        Gizmos.DrawRay(transform.position, rightBoundary * maxDistance);
    }
}
