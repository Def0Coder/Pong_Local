
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Movement : NetworkBehaviour
{
    public float speed = 30;
    public Rigidbody2D rigidbody2d;
    private Manager manager;

    public override void OnStartServer()
    {
        base.OnStartServer();
        rigidbody2d.simulated = true;
        Vector2 randomDirection = Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;
        rigidbody2d.velocity = randomDirection * speed;
    }

    void Start()
    {
        manager = FindObjectOfType<Manager>();

        if (manager == null)
        {
            Debug.LogError("Manager non trovato!");
        }
    }

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    [ServerCallback]
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.GetComponent<Player_Moves>())
        {
            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            float x = col.relativeVelocity.x > 0 ? 1 : -1;
            Vector2 dir = new Vector2(x, y).normalized;
            rigidbody2d.velocity = dir * speed;
        }

        if (col.gameObject.tag == "WL")
        {
            manager.UpdateScore(2); // Player 2 scores
            NetworkServer.Destroy(gameObject);
        }
        else if (col.gameObject.tag == "WR")
        {
            manager.UpdateScore(1); // Player 1 scores
            NetworkServer.Destroy(gameObject);
        }
        //else if (col.gameObject.tag == "Player1Goal" "Player2Goal")
        //{
        //    if (manager != null)
        //    {
        //        manager.UpdateScore(col.transform.position.x > 0 ? 1 : 2);
        //    }
        //    NetworkServer.Destroy(gameObject);
        //}
    }
}


