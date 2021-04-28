using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("GameObjects")]
    private GameObject player;
    public GameObject head;

    [Header("Floats")]
    public float camX;
    public float camY;

    bool death;
    

    // Start is called before the first frame update
    void Start()
    {
        //References player
        player = GameObject.FindGameObjectWithTag("Player");
        //Starts player as alive
        death = false;
    }


    // Update is called once per frame
    void Update()
    {
        //Checks if player is alive
        if(death == false)
        {
            //Set camera to follow player
            transform.position = new Vector3(player.transform.position.x + camX, player.transform.position.y + camY, transform.position.z);
        }

        //checks if player is dead
        if(death == true)
        {
            //Zooms smoothly in on players head
            transform.position = new Vector3(head.transform.position.x, head.transform.position.y, transform.position.z);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 1.0f, Time.deltaTime);
        }
    }


    public void DeathCam()
    {
        death = true;
    }
}
