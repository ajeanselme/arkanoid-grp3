using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
<<<<<<< HEAD:Assets/Scripts/Ball/BallController.cs
    Rigidbody2D rb;
    public GameObject ballPrefab;
    public float speed = 600, ratioSpeed = 1.1f;

    bool canSpawn = true;
=======
    public Rigidbody2D rb;
    private GameManager gm;
>>>>>>> game_loop:Assets/Scripts/Ball/Ball.cs
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
     if((transform.position.x > 100 || transform.position.x > -100)|| (transform.position.y > 100 || transform.position.y > -100))
        {
            Destroy(gameObject);//If the ball glitches out of the box, it destroys himself to prevent lag. Can be changed/removed when adding win condition(s).
        }  
    }

    void OnCollisionEnter2D(Collision2D other){
        //SPAWN BONUS
        if(other.gameObject.CompareTag("brick")){
            print("bick touchaient");
            if(other.gameObject.GetComponent<DisplayBonus>() != null) //verify if it is a bonus brick
            {
                other.gameObject.GetComponent<DisplayBonus>().DropBonus(gameObject); // spawn bonus. Go to "DisplayBonus" script of the concerned brick.
                print("apply bonus");
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("detected bonus");
        //BONUS EFFECT
        if (other.gameObject.tag == "Bonus")
        {
            if (other.gameObject.name.StartsWith("Acc"))
            {
                speed *= ratioSpeed;
            }
            if (other.gameObject.name.StartsWith("Dec"))
            {
                speed /= ratioSpeed;
            }
            if (other.gameObject.name.StartsWith("Spl"))
            {
                if (canSpawn)
                {
                    GameObject newBall = Instantiate(gameObject, transform.position, Quaternion.identity);
                    newBall.name = "ball";
                    canSpawn = false;
                    StartCoroutine(SpawnDelay());
                }
                
                //newBall.GetComponent<Rigidbody2D>().velocity = rb.velocity;
            }
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
