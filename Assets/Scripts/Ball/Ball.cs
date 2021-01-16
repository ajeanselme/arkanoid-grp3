using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    private GameManager gm;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("brick")){
            Destroy(other.gameObject);
        } else if(other.gameObject.CompareTag("goal_brick")){
            GameManager.instance.RemoveGoalBrick(other.gameObject);
        }
    }
}
