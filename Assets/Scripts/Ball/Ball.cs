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
        } else if(other.gameObject.CompareTag("Limit")){
            if(other.gameObject.name.Equals("Limit1")){
                GameManager.instance.WaitForPlayer(1);
            } else if(other.gameObject.name.Equals("Limit2")){
                GameManager.instance.WaitForPlayer(2);
            } else {
                Debug.Log("Limit not defined");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Limit")){
            if(other.gameObject.name.Equals("Limit1")){
                GameManager.instance.WaitForPlayer(1);
            } else if(other.gameObject.name.Equals("Limit2")){
                GameManager.instance.WaitForPlayer(2);
            } else {
                Debug.Log("Limit not defined");
            }
        }

        IEnumerator SpawnDelay()
        {
            yield return new WaitForEndOfFrame();
            canSpawn = true;
            print(canSpawn);
        }
    }
}
