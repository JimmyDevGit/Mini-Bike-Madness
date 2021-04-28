using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject flag;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Calls HasCoin function in Flag script
        flag.GetComponent<Flag>().HasCoin();
        //Plays coin sound
        AudioManager.instance.Play("Coin");
        //Destroy this coin
        Destroy(gameObject);
    }
}
