using UnityEngine;

public class RandomTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float chance = Random.value;
        int dice = Random.Range(1, 7);

        System.Random sysRand = new System.Random();
        int number = sysRand.Next(1, 7);

        Debug.Log("Unity Random (Random.value): " + chance);
        Debug.Log("Unity Random (Random.Range): " + dice);
        Debug.Log("Unity Random (Next): " + number);

    }

}
