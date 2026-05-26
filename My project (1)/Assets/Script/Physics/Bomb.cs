using UnityEngine;
using UnityEngine.InputSystem;

public class Bomb : MonoBehaviour
{

    public GameObject BombPrefabs;
    public bool isCreateBomb = false;
    public Transform CreatePoint;


    public void OnCreateBomb(InputValue Value)
    {
        isCreateBomb = Value.isPressed;
        if (isCreateBomb == true)
        {
            Created();
        }
    }

    public void Created()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject gameObject = Instantiate(BombPrefabs, CreatePoint.position, Quaternion.identity);
        }
    }
    
}
