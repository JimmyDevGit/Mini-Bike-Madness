using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    bool hasCoin;
    public int currentScene;


    private void Start()
    {
        hasCoin = false;
        //Gets current scene number
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if player already got the coin
        if(hasCoin == true)
        {
            //Plays flag/finish level sound
            AudioManager.instance.Play("Flag");
            //Stops motorbike noises
            AudioManager.instance.Stop("Motorbike Accelerate");

            if(currentScene < 6)
            {
                //Loads next scene
                SceneManager.LoadScene(currentScene + 1);
            }

            if(currentScene >= 6)
            {
                //Loads level select scene
                SceneManager.LoadScene(0);
            }
        }
    }


    public void HasCoin()
    {
        hasCoin = true;
    }
}
