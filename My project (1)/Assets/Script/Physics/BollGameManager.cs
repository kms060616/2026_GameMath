using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BollGameManager : MonoBehaviour
{
    public static BollGameManager Instance;

    public int player1Score = 0;
    public int player2Score = 0;
    public int currentTurn = 1;
    public bool isBallMoving = false;
    public bool isGameOver = false;

    [Header("UI References")]
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;

    [Header("Physics Settings")]
    public float stopVelocityThreshold = 0.05f;

    [Header("Turn Camera Settings")]
    public Transform player1Ball;
    public Transform player2Ball;
    public CamearOrbit cameraOrbit;

    private Rigidbody[] allRigidbodies;
    private bool hasHitEnemyThisTurn = false;
    private HashSet<int> hitTargetIDs = new HashSet<int>();
    private int totalTargetCountInScene = 0;

    private float turnActionTime = 0f;
    private bool isCheckingStopCondition = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        List<Rigidbody> validBalls = new List<Rigidbody>();
        foreach (Rigidbody rb in FindObjectsByType<Rigidbody>(FindObjectsSortMode.None))
        {
            if (rb.CompareTag("Player1") || rb.CompareTag("Player2") || rb.CompareTag("Target"))
            {
                rb.WakeUp();
                validBalls.Add(rb);
            }
        }
        allRigidbodies = validBalls.ToArray();

        winText.gameObject.SetActive(false);
        CountTargetsInScene();

        if (cameraOrbit != null && player1Ball != null)
        {
            cameraOrbit.ChangeTarget(player1Ball);
        }

        UpdateUI();
    }

    void Update()
    {
        if (isGameOver) return;

        if (isBallMoving && isCheckingStopCondition)
        {
            if (AreAllBallsStopped())
            {
                EndTurnProcessing();
            }
        }
        else if (isBallMoving && !isCheckingStopCondition)
        {
            if (Time.time - turnActionTime > 0.2f)
            {
                isCheckingStopCondition = true;
            }
        }
    }

    public void StartTurnAction()
    {
        isBallMoving = true;
        isCheckingStopCondition = false;
        turnActionTime = Time.time;

        hitTargetIDs.Clear();
        hasHitEnemyThisTurn = false;

        foreach (Rigidbody rb in allRigidbodies)
        {
            if (rb != null) rb.WakeUp();
        }

        Debug.Log($"[턴 시작] {currentTurn}P가 공을 쳤습니다. 충돌 기록을 시작합니다.");
    }

    void CountTargetsInScene()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        totalTargetCountInScene = targets.Length;
        Debug.Log($"현재 필드에 있는 총 Target 공 개수: {totalTargetCountInScene}개");
    }

    bool AreAllBallsStopped()
    {
        bool allStopped = true;

        foreach (Rigidbody rb in allRigidbodies)
        {
            if (rb == null) continue;

            float speed = rb.linearVelocity.magnitude;
            float rotSpeed = rb.angularVelocity.magnitude;

            if (speed > 0f && speed <= stopVelocityThreshold && rotSpeed <= stopVelocityThreshold)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                rb.Sleep();
            }
            else if (speed > stopVelocityThreshold || rotSpeed > stopVelocityThreshold)
            {
                allStopped = false;
            }
        }

        return allStopped;
    }

    public void RecordHit(GameObject hitObject)
    {
        if (!isBallMoving) return;

        string hitTag = hitObject.tag;

        if (hitTag == "Target")
        {
            int targetID = hitObject.GetInstanceID();
            if (!hitTargetIDs.Contains(targetID))
            {
                hitTargetIDs.Add(targetID);
                Debug.Log($"[적중 로그] Target 공('{hitObject.name}') 맞춤! 현재 턴 맞춘 개수: {hitTargetIDs.Count} / {totalTargetCountInScene}");
            }
        }
        else if ((currentTurn == 1 && hitTag == "Player2") || (currentTurn == 2 && hitTag == "Player1"))
        {
            hasHitEnemyThisTurn = true;
            Debug.Log("[경고 로그] 상대방 플레이어의 공을 맞췄습니다!");
        }
    }



    void EndTurnProcessing()
    {
        isBallMoving = false;
        isCheckingStopCondition = false;

        CountTargetsInScene();

        if (hasHitEnemyThisTurn)
        {
            if (currentTurn == 1) player1Score = Mathf.Max(0, player1Score - 1);
            else player2Score = Mathf.Max(0, player2Score - 1);
            Debug.Log("결과: 상대 공 충돌로 인한 감점 또는 점수 유지");
        }
        else if (hitTargetIDs.Count >= totalTargetCountInScene && totalTargetCountInScene > 0)
        {
            if (currentTurn == 1) player1Score++;
            else player2Score++;
            Debug.Log($"결과: {currentTurn}P 득점 성공! (맞춘 개수: {hitTargetIDs.Count}/{totalTargetCountInScene})");
        }
        else
        {
            Debug.Log($"결과: 득점 실패 (맞춘 개수: {hitTargetIDs.Count} / 전체 개수: {totalTargetCountInScene})");
        }

        if (player1Score >= 5 || player2Score >= 5)
        {
            isGameOver = true;
            winText.text = (player1Score >= 5) ? "1P 플레이어 승리!" : "2P 플레이어 승리!";
            winText.gameObject.SetActive(true);
            return;
        }

        currentTurn = (currentTurn == 1) ? 2 : 1;

        if (cameraOrbit != null)
        {
            Transform nextBall = (currentTurn == 1) ? player1Ball : player2Ball;
            if (nextBall != null) cameraOrbit.ChangeTarget(nextBall);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        turnText.text = $"현재 턴: {currentTurn}P";
        scoreText.text = $"1P: {player1Score}점  |  2P: {player2Score}점";
    }
}
