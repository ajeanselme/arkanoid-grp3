using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;

    bool blockTop, blockBot;

    


    void Update()
    {
        if (isPlayer1)
        {
            if(Input.GetKey(KeyCode.Z)){
                if(!blockTop){
                    transform.position += Vector3.up * speed * Time.deltaTime;
                }
                if(blockBot){
                    blockBot = false;
                }
            } else if(Input.GetKey(KeyCode.S)) {
                if(!blockBot) {
                    transform.position += Vector3.down * speed * Time.deltaTime;
                }
                
                if(blockTop){
                    blockTop = false;
                }
            }
        }
        else
        {
            if(Input.GetKey(KeyCode.UpArrow)){
                if(!blockTop){
                    transform.position += Vector3.up * speed * Time.deltaTime;
                }
                if(blockBot){
                    blockBot = false;
                }
            } else if(Input.GetKey(KeyCode.DownArrow)) {
                if(!blockBot) {
                    transform.position += Vector3.down * speed * Time.deltaTime;
                }
                
                if(blockTop){
                    blockTop = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D hit){
        if(hit.gameObject.name.Equals("ColliderTop")){
            blockTop = true;
        } else if(hit.gameObject.name.Equals("ColliderBottom")){
            blockBot = true;
        }
    }
}
