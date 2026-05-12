using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BezierAttack : MonoBehaviour
{
    public bool isBezierAttack = false;
    
    public Transform targetPosition;
    public Transform startPosition;

    public GameObject prefabs;
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
        for (int i = 0; i < 10; i++)
        {
            BezierDe bezierDe = Instantiate(prefabs, transform.position, Quaternion.identity).GetComponent<BezierDe>();
            bezierDe.p0 = this.transform;
            bezierDe.p3 = targetPosition;
        }
        
    }
}
