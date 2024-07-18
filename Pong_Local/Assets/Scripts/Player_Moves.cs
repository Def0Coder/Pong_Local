using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Moves : NetworkBehaviour
{
    [SerializeField]
    public float speed = 110;
    public Rigidbody2D rigidbody2d;

   
    void FixedUpdate()
    {
        
        if (isLocalPlayer)
            rigidbody2d.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
    }
}
