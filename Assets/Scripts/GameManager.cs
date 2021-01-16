using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    


    List<GameObject> balls = new List<GameObject>();

    public GameObject ballPrefab;

    public GameObject player1;
    public GameObject player2;

    int lives_1 = 3, lives_2 = 3;

    bool waiting1, waiting2;


    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        WaitForPlayer(1);
    }


    void Update(){
        if(waiting1){
            if(Input.GetKey(KeyCode.Space)){
                waiting1 = false;
                float y = Random.Range (-.75f, .75f);

                balls[0].GetComponent<Rigidbody2D>().AddForce((new Vector2(1f,y)).normalized * 600);
            }
            balls[0].transform.position = new Vector2(player1.transform.position.x + 0.35f, player1.transform.position.y);
        } else if(waiting2){
            if(Input.GetKey(KeyCode.Space)){
                waiting2 = false;
                float y = Random.Range (-.75f, .75f);

                balls[0].GetComponent<Rigidbody2D>().AddForce((new Vector2(-1f,y)).normalized * 600);
            }
            balls[0].transform.position = new Vector2(player2.transform.position.x - 0.35f, player2.transform.position.y);
        }
    }

    void WaitForPlayer(int playerIndex){
        waiting1 = false;
        waiting2 = false;

        Transform transform;

        if(playerIndex == 1){
            transform = player1.transform;
        } else {
            transform = player2.transform;
        }
        
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        balls.Clear();
        
        GameObject newBall = Instantiate(ballPrefab, transform);
        balls.Add(newBall);
        newBall.transform.SetParent(null);
        newBall.transform.localScale = new Vector3(0.35f,0.35f,0.35f);

        if(playerIndex == 1){
            waiting1 = true;
        } else {            
            waiting2 = true;
        }
    }


    public void RemoveGoalBrick(GameObject brick){
        if(brick.transform.parent.name.Equals("Goal_1")){
            lives_1 -= 1;

            if(lives_1 <= 0){
                Debug.Log("Player 1 lost");
            }
            WaitForPlayer(1);
        } else if(brick.transform.parent.name.Equals("Goal_2")){
            lives_2 -= 1;

            if(lives_2 <= 0){
                Debug.Log("Player 2 lost");
            }
            WaitForPlayer(2);
        } else {
            Debug.Log("Error removing a goal brick: hierarchy not correct");
        }
        
        brick.SetActive(false);
    }

    
}
