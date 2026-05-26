using UnityEngine;
using UnityEngine.InputSystem;

public class Bomb : MonoBehaviour
{

    public GameObject BombPrefabs;
    public GameObject BombPrefabs2;
    public bool isCreateBomb = false;
    public bool isCreateBomb2 = false;
    public Transform CreatePoint;


    public void OnCreateBomb(InputValue Value)
    {
        isCreateBomb = Value.isPressed;
        if (isCreateBomb == true)
        {
            Created();
        }
    }
    public void OnCreateBomb2(InputValue Value)
    {
        isCreateBomb2 = Value.isPressed;
        if (isCreateBomb2 == true)
        {
            Created2();
        }
    }

    public void Created()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject gameObject = Instantiate(BombPrefabs, CreatePoint.position, Quaternion.identity);
        }
    }

    public void Created2()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject gameObject = Instantiate(BombPrefabs2, CreatePoint.position, Quaternion.identity);
        }
    }

}
