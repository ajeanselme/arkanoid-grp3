using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBonus : MonoBehaviour
{
    public GameObject accBonus, decBonus, splBonus;
    public void DropBonus(GameObject ball)
    {
        int randomBonusNumber = Random.Range(0, 3);
        print(randomBonusNumber);
        switch (randomBonusNumber)
        {
            case 0:
                //speed upgrade
                Instantiate(accBonus, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            case 1:
                //downspeed upgrade
                Instantiate(decBonus, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            case 2:
                //split upgrade
                Instantiate(splBonus, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
        }
    }

}
