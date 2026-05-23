using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (BollGameManager.Instance == null || !BollGameManager.Instance.isBallMoving) return;

        GameObject hitObject = collision.gameObject;
        Debug.Log($"[물리 충돌 감지] {gameObject.name}와(과) {hitObject.name}이(가) 부딪힘!");

        if (hitObject.CompareTag("Target"))
        {
            BollGameManager.Instance.RecordHit(hitObject);
        }
        else if (gameObject.CompareTag("Player1") && hitObject.CompareTag("Player2") && BollGameManager.Instance.currentTurn == 1)
        {
            BollGameManager.Instance.RecordHit(hitObject);
        }
        else if (gameObject.CompareTag("Player2") && hitObject.CompareTag("Player1") && BollGameManager.Instance.currentTurn == 2)
        {
            BollGameManager.Instance.RecordHit(hitObject);
        }
    }
}
