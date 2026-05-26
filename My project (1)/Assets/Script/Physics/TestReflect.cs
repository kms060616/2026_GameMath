using UnityEngine;


public class TestReflect : MonoBehaviour
{
    public Vector3 velocity = new Vector3(2f, -3f, 0f);

    public Vector3 gravity = new Vector3(0, -9.81f, 0);

    float damping = 0.9f;

    private int bounceCount = 0;

    public float force = 10f;
    public float radius = 5f;
    public float upwardsModifier = 1f;


    // Update is called once per frame
    void Update()
    {
        velocity += gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy와 충돌하여 오브젝트를 제거합니다.");
            RunExplode();
            return;
        }

        Vector3 normal = col.contacts[0].normal.normalized;
        float dot = Vector3.Dot(velocity, normal);
        Vector3 reflect = velocity - 2f * dot * normal;

        velocity = reflect * damping;

        bounceCount++;

        if (bounceCount >= 3)
        {
            Debug.Log("3번 튕겨서 오브젝트를 제거합니다.");
            RunExplode();
        }


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
}
