using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitPoint = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ApplyCollisionLogic();
        }
    }

    private void ApplyCollisionLogic()
    {
        hitPoint--;

        if (hitPoint <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<BricksManager>().RemoveBrick(this);
        }
    }
}
