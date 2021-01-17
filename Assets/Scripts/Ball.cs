using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float ratioSpeed = 1.1f;

    public Rigidbody2D rb;
    
    float pickupCooldown = .5f;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(pickupCooldown > 0){
            pickupCooldown -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        //SPAWN BONUS
        if(other.gameObject.CompareTag("brick")){
            pickupCooldown = .5f;
            GameManager.instance.DropBonus(gameObject.transform);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("goal_brick")){

            GameManager.instance.RemoveGoalBrick(other.gameObject);

        } else if(other.gameObject.CompareTag("Limit")){
            if(GameManager.instance.balls.Count > 1){
                GameManager.instance.balls.Remove(gameObject);
                Destroy(gameObject);
            } else if(other.gameObject.name.Equals("Limit1")){
                GameManager.instance.WaitForPlayer(1);
            } else if(other.gameObject.name.Equals("Limit2")){
                GameManager.instance.WaitForPlayer(2);
            } else {
                Debug.Log("Limit not defined");
            }
        } else if(other.gameObject.CompareTag("Bonus") && pickupCooldown <= 0f){
            if (other.gameObject.name.StartsWith("Acc"))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity *= ratioSpeed;
                Debug.Log("Speed buff");
            }
            if (other.gameObject.name.StartsWith("Dec"))
            {                
                gameObject.GetComponent<Rigidbody2D>().velocity /= ratioSpeed;
                Debug.Log("Speed debuff");
            }
            if (other.gameObject.name.StartsWith("Spl"))
            {
                    GameObject newBall = Instantiate(gameObject, transform.position, Quaternion.identity);
                    newBall.name = "ball";
                    GameManager.instance.balls.Add(newBall);
                    float x = Random.Range (-.2f, -1f);
                    float y = Random.Range (-.75f, .75f);

                    newBall.GetComponent<Rigidbody2D>().AddForce((new Vector2(x,y)).normalized * 600);

                    Debug.Log("Split");
                    //newBall.GetComponent<Rigidbody2D>().velocity = rb.velocity;
            }
            Destroy(other.gameObject);
        }
    }
}

