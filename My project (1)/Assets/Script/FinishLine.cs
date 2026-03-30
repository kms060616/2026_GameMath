using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter(Collider medical)
    {
        if (medical.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length == 0)
            {
                Debug.Log("모든 적 처치 완료! 다음 씬으로 이동합니다.");
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log($"아직 적이 {enemies.Length}명 남아있습니다!");
            }
        }
    }
}
