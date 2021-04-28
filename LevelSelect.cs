using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    //Loads level based on which button is clicked
    public void PollutedBeach()
    {
        SceneManager.LoadScene(1);
    }
    public void BackflipAlley()
    {
        SceneManager.LoadScene(2);
    }
    public void DeadEnd()
    {
        SceneManager.LoadScene(3);
    }
    public void SandCastleh()
    {
        SceneManager.LoadScene(4);
    }
    public void Garage()
    {
        SceneManager.LoadScene(5);
    }
    public void KitchenSink()
    {
        SceneManager.LoadScene(6);
    }
}
