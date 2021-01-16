using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{

    public int rows;
    public int colums;
    [Range(0, 100)]
    public int bonusDropRatio;
    public float spacing;
    public GameObject brickPrefab;
    public GameObject bonusBrickPrefab;

    private List<GameObject> bricks = new List<GameObject>();

    void Start()
    {
        ResetLevel();
    }


    void Update()
    {
        
    }

    public void ResetLevel()
    {
        foreach (GameObject brick in bricks)
        {
            Destroy(brick);
        }
        bricks.Clear();



        for (int x = 0; x < colums; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector2 spawnPos = (Vector2)transform.position + new Vector2(
                    x * (1 + spacing),
                    -y * (0.5f + spacing));
                int randomPercent = Random.Range(0, 100);
                if(randomPercent < bonusDropRatio)
                {
                    GameObject bonusBrick = Instantiate(bonusBrickPrefab, spawnPos, Quaternion.identity);//bonus brick
                    bricks.Add(bonusBrick);
                }
                else
                {
                    GameObject brick = Instantiate(brickPrefab, spawnPos, Quaternion.identity); //classic brick
                    bricks.Add(brick);
                }
                
                
            }
        }
    }

    public void RemoveBrick(Brick brick)
    {
        bricks.Remove(brick.gameObject);
        if (bricks.Count <= 0)
        {
            ResetLevel();
        }
    }
}
