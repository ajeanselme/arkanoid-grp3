using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 600;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.up * speed);
    }


    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("brick")){
            Destroy(other.gameObject);
        }
    }
}
