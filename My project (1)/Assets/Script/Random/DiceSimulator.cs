using TMPro;
using UnityEngine;

public class DiceSimulator : MonoBehaviour
{
    int[] counts = new int[6];
    public int trials = 100;
    public TextMeshProUGUI[] text1 = new TextMeshProUGUI[6];
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < trials; i++)
        {
            int result = Random.Range(1, 7);
            counts[result - 1 ]++;
        }

        for (int i = 0; i < counts.Length; i++)
        {
            float percent = (float)counts[i] / trials * 100f;
            string result = $"{i + 1}: {counts[i]}회, ({percent:F2}%)";
            text1[i].text = result;
        }

    }

    public void ButtonClick()
    {
        for (int i = 0; i < 6; i++)
        {
            counts[i] = 0;
        }
        Simulate();
    }
    void Simulate()
    {
        for (int i = 0; i < trials; i++)
        {
            int result = Random.Range(1, 7);
            counts[result - 1]++;
        }

        for (int i = 0; i < counts.Length; i++)
        {
            float percent = (float)counts[i] / trials * 100f;
            string result = $"{i + 1}: {counts[i]}회, ({percent:F2}%)";
            text1[i].text = result;
        }
    }
    public void ButtonClicked()
    {

    }
}
