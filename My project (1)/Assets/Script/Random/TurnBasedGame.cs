using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text;

public class TurnBasedGame : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] float critChance = 0.2f;
    [SerializeField] float meanDamage = 20f;
    [SerializeField] float stdDevDamage = 5f;
    [SerializeField] float enemyHP = 100f;
    [SerializeField] float poissonLambda = 2f;
    [SerializeField] float hitRate = 0.6f;
    [SerializeField] float critDamageRate = 2f;
    [SerializeField] int maxHitsPerTurn = 5;

    [Header("Probability Settings")]
    [SerializeField] float baseRareChance = 0.05f;
    [SerializeField] float chanceGainPerTurn = 0.05f;

    [Header("UI Reference")]
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] TextMeshProUGUI logText; 

    int currentTurn = 0;
    float currentRareChance = 0f;
    bool rareItemObtained = false;
    string[] rewards = { "Gold", "Weapon", "Armor", "Potion" };

    StringBuilder sbLog = new StringBuilder();

    public void StartSimulation()
    {
        rareItemObtained = false;
        currentTurn = 0;
        currentRareChance = baseRareChance;
        sbLog.Clear();

        sbLog.AppendLine("<color=#FFD700>=== ½Ã¹Ä·¹ÀÌ¼Ç ½ÃÀÛ ===</color>");

        while (!rareItemObtained)
        {
            currentTurn++;
            SimulateTurn();

            currentRareChance = Mathf.Min(1.0f, baseRareChance + (currentTurn * chanceGainPerTurn));

            if (currentTurn > 1000) break;
        }

        UpdateUI();
    }

    void SimulateTurn()
    {
        sbLog.AppendLine($"<b>[Turn {currentTurn}]</b> (È¹µæ È®·ü: {currentRareChance * 100:F0}%)");

        int enemyCount = SamplePoisson(poissonLambda);
        int totalKills = 0;

        for (int i = 0; i < enemyCount; i++)
        {
            int hits = SampleBinomial(maxHitsPerTurn, hitRate);
            float totalDamage = 0f;

            for (int j = 0; j < hits; j++)
            {
                float damage = SampleNormal(meanDamage, stdDevDamage);
                if (Random.value < critChance) damage *= critDamageRate;
                totalDamage += damage;
            }

            if (totalDamage >= enemyHP)
            {
                totalKills++;
                string reward = rewards[Random.Range(0, rewards.Length)];

                if ((reward == "Weapon" || reward == "Armor") && Random.value < currentRareChance)
                {
                    rareItemObtained = true;
                    sbLog.AppendLine($"<color=#00FF00>¡Ú ·¹¾î {reward} È¹µæ! ¡Ú</color>");
                }
            }
        }
        sbLog.AppendLine($"> Ã³Ä¡ ¼ö: {totalKills} / µîÀå: {enemyCount}");
    }

    void UpdateUI()
    {
        statusText.text = $"ÃÖÁ¾ °á°ú: {currentTurn}ÅÏ ¸¸¿¡ ¼º°ø\nÃÖÁ¾ È®·ü: {currentRareChance * 100:F0}%";

        logText.text = sbLog.ToString();

        Debug.Log(sbLog.ToString());
    }

    int SamplePoisson(float lambda)
    {
        int k = 0; float p = 1f; float L = Mathf.Exp(-lambda);
        while (p > L) { k++; p *= Random.value; }
        return k - 1;
    }

    int SampleBinomial(int n, float p)
    {
        int success = 0;
        for (int i = 0; i < n; i++) if (Random.value < p) success++;
        return success;
    }

    float SampleNormal(float mean, float stdDev)
    {
        float u1 = Random.value; float u2 = Random.value;
        float z = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);
        return mean + stdDev * z;
    }

}
