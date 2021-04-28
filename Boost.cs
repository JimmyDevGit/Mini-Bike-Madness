using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Calls boost function in Movement script
        GameObject.Find("Player").GetComponent<Movement>().BoostFunction();
    }
}
