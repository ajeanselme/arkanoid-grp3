using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;

    private float movement;


    void Update()
    {
        if (isPlayer1)
        {
            movement = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            movement = Input.GetAxisRaw("Horizontal2");
        }

        rb.velocity = new Vector2(rb.velocity.y, movement * speed);
    }
}
