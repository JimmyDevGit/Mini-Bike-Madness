using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Plays bounce sound when hitting bubbles
        AudioManager.instance.Play("Bounce");
    }
}
