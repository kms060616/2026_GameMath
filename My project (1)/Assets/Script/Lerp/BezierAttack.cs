using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BezierAttack : MonoBehaviour
{
    public bool isBezierAttack = false;
    
    public Transform targetPosition;
    public Transform startPosition;

    public GameObject prefabs;
    public string enemyTag = "Enemy";

    public void OnBezierAttack(InputValue Value)
    {
        isBezierAttack = Value.isPressed;
        if (isBezierAttack == true)
        {
            Shooting();
        }
    }

    public void Shooting()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        if (enemies.Length == 0)
        {
            Debug.LogWarning("공격할 적이 없습니다!");
            return;
        }

        Transform closestEnemy = GetClosestEnemy(enemies);

        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(prefabs, transform.position, Quaternion.identity);
            BezierDe bezierDe = go.GetComponent<BezierDe>();

            if (bezierDe != null)
            {
                bezierDe.Init(this.transform, closestEnemy);
            }
        }

    }

    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }
}
