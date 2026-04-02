using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CriticalManager : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 300f;
    private float currentHealth;
    public float playerDamage = 30f;
    public float targetCritRate = 0.3f;

    [Header("Tracking")]
    private int totalHits = 0;
    private int critHits = 0;
    private int countCommon, countRare, countEpic, countLegend;

    [Header("Probabilities")]
    private float pCommon = 50f, pRare = 30f, pEpic = 15f, pLegend = 5f;
    private string lastLootMessage = "전투 대기 중...";

    [Header("UI Reference")]
    public TextMeshProUGUI healthText;  
    public TextMeshProUGUI attackDataText; 
    public TextMeshProUGUI itemProbText; 
    public TextMeshProUGUI itemCountText; 

    void Start()
    {
        currentHealth = maxHealth;
        UpdateAllUI();
    }

    public void OnAttackButtonClick()
    {
        if (currentHealth <= 0) return;

        bool isCrit = RollCrit();
        float damage = isCrit ? playerDamage * 2 : playerDamage;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0; 
            DropItem();
            UpdateAllUI();
            Invoke("SpawnNewEnemy", 0.8f); 
        }
        else
        {
            UpdateAllUI();
        }
    }

    private void SpawnNewEnemy()
    {
        currentHealth = maxHealth;
        lastLootMessage = "새로운 적 등장!";
        UpdateAllUI();
    }

    private bool RollCrit()
    {
        totalHits++;
        float currentRate = (totalHits <= 1) ? 0 : (float)critHits / totalHits;

        if (currentRate < targetCritRate && (float)(critHits + 1) / totalHits <= targetCritRate)
        {
            critHits++; return true;
        }
        if (currentRate > targetCritRate && (float)critHits / totalHits >= targetCritRate)
        {
            return false;
        }

        if (Random.value < targetCritRate)
        {
            critHits++; return true;
        }
        return false;
    }

    private void DropItem()
    {
        float roll = Random.Range(0f, 100f);
        string grade = "";

        if (roll < pLegend)
        {
            countLegend++;
            grade = "전설";
            ResetItemProbs();
        }
        else
        {
            if (roll < pLegend + pEpic) { countEpic++; grade = "희귀"; }
            else if (roll < pLegend + pEpic + pRare) { countRare++; grade = "고급"; }
            else { countCommon++; grade = "일반"; }

            ApplyPity();
        }
        lastLootMessage = $"최근 획득: {grade} 아이템!";
    }

    private void ApplyPity()
    {
        pLegend += 1.5f;
        pCommon -= 0.5f; pRare -= 0.5f; pEpic -= 0.5f;
    }

    private void ResetItemProbs()
    {
        pCommon = 50f; pRare = 30f; pEpic = 15f; pLegend = 5f;
    }

    private void UpdateAllUI()
    {
        if (healthText != null)
            healthText.text = $"적 체력 : {currentHealth} / {maxHealth}";

        float actualRate = totalHits == 0 ? 0 : (float)critHits / totalHits * 100f;
        attackDataText.text = $"전체 공격 회수 : {totalHits}\n" +
                              $"발생한 치명타 회수 : {critHits}\n" +
                              $"설정된 치명타 확률 : {targetCritRate * 100:F2}%\n" +
                              $"실제 치명타 확률 : {actualRate:F2}%";

        itemProbText.text = $"현재 아이템 확률\n" +
                            $"일반 : {pCommon:F1}%\n" +
                            $"고급 : {pRare:F1}%\n" +
                            $"희귀 : {pEpic:F1}%\n" +
                            $"전설 : {pLegend:F1}%";

        itemCountText.text = $"현재 드롭된 아이템\n" +
                             $"일반 : {countCommon}  고급 : {countRare}\n" +
                             $"희귀 : {countEpic}  전설 : {countLegend}\n\n" +
                             $"{lastLootMessage}";
    }
}
