using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public List<GameObject> balls = new List<GameObject>();

    public GameObject ballPrefab;
    public GameObject brickPrefab;
    
    public GameObject accBonus, decBonus, splBonus;

    public GameObject player1;
    public GameObject player2;

    public Text endText;

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
        endText.gameObject.SetActive(false);
        WaitForPlayer(1);
    }


    void Update(){
        if(waiting1){
            if(Input.GetKey(KeyCode.Space)){
                waiting1 = false;
                float x = Random.Range (.2f, 1f);
                float y = Random.Range (-.75f, .75f);

                balls[0].GetComponent<Rigidbody2D>().AddForce((new Vector2(x,y)).normalized * 600);
            }
            balls[0].transform.position = new Vector2(player1.transform.position.x + 0.35f, player1.transform.position.y);
        } else if(waiting2){
            if(Input.GetKey(KeyCode.Space)){
                waiting2 = false;
                float x = Random.Range (-.2f, -1f);
                float y = Random.Range (-.75f, .75f);

                balls[0].GetComponent<Rigidbody2D>().AddForce((new Vector2(x,y)).normalized * 600);
            }
            balls[0].transform.position = new Vector2(player2.transform.position.x - 0.35f, player2.transform.position.y);
        }
    }

    public void WaitForPlayer(int playerIndex){
        if(balls.Count > 1){
            return;
        }
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
                EndGame(2);
            }
            WaitForPlayer(1);
        } else if(brick.transform.parent.name.Equals("Goal_2")){
            lives_2 -= 1;

            if(lives_2 <= 0){                
                EndGame(1);
            }
            WaitForPlayer(2);
        } else {
            Debug.Log("Error removing a goal brick: hierarchy not correct");
        }
        
        brick.SetActive(false);
    }

    public void SpawnBrick(){
        float rand = Random.value;
        if(rand > 0.5f){
            float x = Random.Range (-5f, 5f);
            float y = Random.Range (-4f, 4f);

            GameObject brick = Instantiate(brickPrefab, new Vector3(x,y,0f), Quaternion.identity);

            brick.transform.SetParent(null);
            brick.transform.localScale = new Vector2(.2468185f, 0.3723528f);
            brick.transform.eulerAngles = new Vector3(
                brick.transform.eulerAngles.x,
                brick.transform.eulerAngles.y,
                brick.transform.eulerAngles.z - 90
            );
        }
    }
    
    
    public void DropBonus(Transform ballTr)
    {
        float rand = Random.value;
        if(rand < 0.5f){

        } else if(rand < 0.73){
                //downspeed upgrade
                Instantiate(decBonus, ballTr.position, Quaternion.identity);
        }else if(rand < 0.86){
                //speed upgrade
                Instantiate(accBonus, ballTr.position, Quaternion.identity);
        } else {
                //split upgrade
                Instantiate(splBonus, ballTr.position, Quaternion.identity);
        }
    }

    void EndGame(int winner){
        endText.text = "Player " + winner + " wins!";
        endText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    
}
