using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    
    public void SimulateGachaSingle()
    {
        string result = Simulate();
        resultText.text = "가챠 결과: " + result;
        Debug.Log("Gacha Result: " + Simulate());
    }

    public void SimulateGachaTenTime()
    {
        List<string> results = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            results.Add(Simulate());
        }

        resultText.text = "10연차 결과:\n" + string.Join(", ", results);
        Debug.Log("Gacha Results: " + string.Join(", ", results));
    }
    string Simulate()
    {
        
        float r = Random.value;
        string result = string.Empty;
        

        if (r < 0.4f) result = "C";
        else if (r < 0.7f) result = "B";
        else if (r < 0.9f) result = "A";
        else result = "S";

        return result;
    }
}
