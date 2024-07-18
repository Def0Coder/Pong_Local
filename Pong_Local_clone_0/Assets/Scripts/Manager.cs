using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : NetworkBehaviour
{
    public GameObject ballPrefab;

    [SyncVar(hook = nameof(OnScoreP1Changed))]
    public int scoreP1 = 0;

    [SyncVar(hook = nameof(OnScoreP2Changed))]
    public int scoreP2 = 0;

    public TextMeshProUGUI Score1Text;
    public TextMeshProUGUI Score2Text;

    private GameObject currentBall;

    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    void OnScoreP1Changed(int oldScore, int newScore)
    {
        Score1Text.text = "P1: " + newScore;
        Debug.Log($"Punteggio Player 1 aggiornato: {newScore}");
    }

    void OnScoreP2Changed(int oldScore, int newScore)
    {
        Score2Text.text = "P2: " + newScore;
        Debug.Log($"Punteggio Player 2 aggiornato: {newScore}");
    }

    [Server]
    public void UpdateScore(int player)
    {
        if (player == 1)
        {
            scoreP1++;
        }
        else if (player == 2)
        {
            scoreP2++;
        }

        // Distrugge la palla corrente
        if (currentBall != null)
        {
            NetworkServer.Destroy(currentBall);
        }

        // Spawna una nuova palla
        SpawnBall();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            SpawnBall();
        }
    }

    [Server]
    void SpawnBall()
    {
        if (currentBall == null)
        {
            currentBall = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
            NetworkServer.Spawn(currentBall);
        }
    }
}
