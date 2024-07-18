using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : NetworkBehaviour
{
    public GameObject ballPrefab; // Assegna il prefab della palla dall'Inspector

    public override void OnStartServer()
    {
        base.OnStartServer();
        SpawnBall();
    }

    [Server]
    void SpawnBall()
    {
        // Crea la palla solo sul server
        GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.Spawn(ball);
    }
}
