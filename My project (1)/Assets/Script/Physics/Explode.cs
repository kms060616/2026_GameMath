using UnityEngine;

public class Explode : MonoBehaviour
{
    public float delay = 1.5f;
    public float force = 10f;
    public float radius = 5f;
    public float upwardsModifier = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Invoke("Explode", 2f);
        Invoke(nameof(RunExplode), delay);
    }


    void RunExplode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (var col in colliders)
        {
            Rigidbody rb = col.attachedRigidbody;
            if (rb == null) continue;
            Vector3 toTraget = rb.position - explosionPos;
            float distance = toTraget.magnitude;
            Vector3 dir = toTraget.normalized;
            float attenuation = 1f - Mathf.Clamp01(distance / radius);
            dir += Vector3.up * upwardsModifier;
            dir = dir.normalized;
            Vector3 impulse = dir * force * attenuation;
            rb.AddForce(impulse, ForceMode.Impulse);

        }
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
