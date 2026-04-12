using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class DamageSimulator : MonoBehaviour
{
    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI logDisplay;
    public TextMeshProUGUI resultDisplay;
    public TextMeshProUGUI rangeDisplay;

    private int level = 1;
    private float totalDamage = 0, baseDamage = 20;
    private int attackCount = 0;

    
    private int weakPointCount = 0;
    private int missCount = 0;
    private int totalCritCount = 0;
    private float maxDamage = 0;

    private string weaponName;
    private float stdDevMult, critRate, critMult;

    private void ResetData()
    {
        totalDamage = 0;
        attackCount = 0;
        level = 1;
        baseDamage = 20f;
        weakPointCount = 0;
        missCount = 0;
        totalCritCount = 0;
        maxDamage = 0;
    }

    public void SetWeapon(int id)
    {
        ResetData();
        if (id == 0) SetStats("단검", 0.1f, 0.4f, 1.5f);
        else if (id == 1) SetStats("장검", 0.2f, 0.3f, 2.0f);
        else if (id == 2) SetStats("도끼", 0.3f, 0.2f, 3.0f);

        logDisplay.text = $"{weaponName} 장착!";
        UpdateUI();
    }

    private void SetStats(string _name, float _stdDev, float _critRate, float _critMult)
    {
        weaponName = _name;
        stdDevMult = _stdDev;
        critRate = _critRate;
        critMult = _critMult;
    }

    public void LevelUp()
    {
        totalDamage = 0;
        attackCount = 0;
        weakPointCount = 0;
        missCount = 0;
        totalCritCount = 0;
        maxDamage = 0;
        level++;
        baseDamage = level * 20f;
        logDisplay.text = $"레벨업! 현재 레벨: {level}";
        UpdateUI();
    }

    public void OnAttack()
    {
        ExecuteAttack();
        UpdateUI();
    }

    public void OnAttackX1000()
    {
        for (int i = 0; i < 1000; i++)
        {
            ExecuteAttack();
        }
        logDisplay.text = "<color=yellow>1,000회 연속 공격 완료!</color>";
        UpdateUI();
    }

    private void ExecuteAttack()
    {
        float u1 = 1.0f - Random.value;
        float u2 = 1.0f - Random.value;
        float z = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);

        float sd = baseDamage * stdDevMult;
        float normalDamage = baseDamage + (sd * z); 

        float finalDamage = normalDamage;
        bool isMiss = false;
        bool isWeakPoint = false;
        bool isCrit = false;


        if (z < -2.0f)
        {
            isMiss = true;
            finalDamage = 0;
            missCount++;
        }
        else if (z > 2.0f) 
        {
            isWeakPoint = true;
            finalDamage *= 2.0f;
            weakPointCount++;
        }

        if (!isMiss && Random.value < critRate)
        {
            isCrit = true;
            finalDamage *= critMult;
            totalCritCount++;
        }

        attackCount++;
        totalDamage += finalDamage;
        if (finalDamage > maxDamage) maxDamage = finalDamage;


        if (attackCount % 1000 != 0)
        {
            string msg = isMiss ? "<color=gray>Miss</color>" : $"{finalDamage:F1}";
            if (isWeakPoint) msg = $"<color=blue>[약점]</color> " + msg;
            if (isCrit) msg = $"<color=red>[치명타]</color> " + msg;
            logDisplay.text = $"데미지: {msg}";
        }
    }

    private void UpdateUI()
    {
        statusDisplay.text = $"Level: {level} / 무기: {weaponName}\n기본: {baseDamage} / 치명타: {critRate * 100}% (x{critMult})";

        rangeDisplay.text = $"일반 범위: [{baseDamage - (3 * baseDamage * stdDevMult):F1} ~ {baseDamage + (3 * baseDamage * stdDevMult):F1}]\n" +
                           $"약점/미스 기준: ±{2 * baseDamage * stdDevMult:F1}";

        float dpa = attackCount > 0 ? totalDamage / attackCount : 0;
        resultDisplay.text = $"<b>[누적 통계]</b>\n" +
                             $"공격 횟수: {attackCount}회\n" +
                             $"평균 DPA: {dpa:F2} / 최대 데미지: {maxDamage:F1}\n" +
                             $"<color=blue>약점 공격: {weakPointCount}회</color> / 명중 실패: {missCount}회</color>\n" +
                             $"총 크리티컬: {totalCritCount}회";
    }

    private float GetNormalStdDevDamage(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value;
        float u2 = 1.0f - Random.value;
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);

        return mean + (stdDev * randStdNormal);
    }
}
