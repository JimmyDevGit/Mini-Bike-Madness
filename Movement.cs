using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Header("Rigid Bodies")]
    public Rigidbody2D backTire;
    public Rigidbody2D frontTire;
    public Rigidbody2D player;

    [Header("SpriteRenderers")]
    public SpriteRenderer frameSprite;
    public SpriteRenderer backSuspentionSprite;
    public SpriteRenderer frontSuspentionSprite;
    public CircleCollider2D head;

    [Header("Floats")]
    public float speed;
    public float rotatePlayer;

    bool facingRight;
    bool boost;
    private int currentScene;
    

    // Start is called before the first frame update
    void Start()
    {
        //Player facing left or right
        facingRight = true;
        boost = false;
        //Plays engine idle sound on start
        AudioManager.instance.Play("Motorbike Idle");
        //Get current scene number
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }


    // Update is called once per frame
    void Update()
    {
        //Checks distance between player and front wheel
        float dist = Vector3.Distance(GameObject.Find("Front Wheel").transform.position, transform.position);
        //Checks distance between player and back wheel
        float distBack = Vector3.Distance(GameObject.Find("Back Wheel").transform.position, transform.position);


        if (dist > 5.3)
        {
            //Plays spring sound
            AudioManager.instance.Play("Spring1");
        }

        if (distBack > 1.9)
        {
            //Plays spring sound
            AudioManager.instance.Play("Spring2");
        }


        //If player presses space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Calls Flip function
            Flip();
        }

        //If player presses S
        if (Input.GetKey(KeyCode.S))
        {
            //Stops back tyre rotating
            backTire.freezeRotation = true;
            //Stops front tyre rotating
            frontTire.freezeRotation = true;
        }

        //If player is not pressing S
        else
        {
            //Allows back tyre rotation
            backTire.freezeRotation = false;
            //Allows front tyre rotation
            frontTire.freezeRotation = false;
        }

        //If player presses W
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Plays accelerate sound
            AudioManager.instance.Play("Motorbike Accelerate");
            //Stops idle sound
            AudioManager.instance.Stop("Motorbike Idle");
        }

        //If player releases W key
        if (Input.GetKeyUp(KeyCode.W))
        {
            //Plays idle sound
            AudioManager.instance.Play("Motorbike Idle");
            //Stops accelerate sound
            AudioManager.instance.Stop("Motorbike Accelerate");
        }
    }


    private void FixedUpdate()
    {
        //If player presses W
        if (Input.GetKey(KeyCode.W))
        {
            //If player is facing right
            if (facingRight == true)
            {
                //spins back tyre clockwise
                backTire.AddTorque(-speed * Time.fixedDeltaTime);
            }

            //If player is facing left
            if (facingRight == false)
            {
                //spins front tyre counter clockwise
                frontTire.AddTorque(speed * Time.fixedDeltaTime);
            }
        }

        //If player presses A
        if (Input.GetKey(KeyCode.A))
        {
            //Rotate player counter clockwise
            player.AddTorque(rotatePlayer * Time.fixedDeltaTime);
        }
        
        //If player presses D
        if (Input.GetKey(KeyCode.D))
        {
            //Rotate player clockwise
            player.AddTorque(-rotatePlayer * Time.fixedDeltaTime);
        }

        //If player is in boost mode
        if(boost == true)
        {
            //Shoots player to the right relative to world
            player.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
        }
    }
    

    //Sets player to boost mode
    public void BoostFunction()
    {
        boost = true;
        //Calls Boost timer
        StartCoroutine(Boost());
    }

    //Boost timer
    public IEnumerator Boost()
    {
        //Waits half a second
        yield return new WaitForSeconds(0.5f);
        //Stops boost mode
        boost = false;
    }


    //Flips player between facing left and facing right
    void Flip()
    {
        //If player is facing left
        if (facingRight == false)
        {
            //Don't flip sprite
            frameSprite.flipX = false;
            //Show back suspention
            backSuspentionSprite.enabled = true;
            //Hide front suspention
            frontSuspentionSprite.enabled = false;
            //Sets new location for heads circle collider
            head.offset = new Vector2(head.offset.x + 0.79f, head.offset.y);
            //Player is now facing right
            facingRight = true;
        }

        //If player is facing right
        else if (facingRight == true)
        {
            //Flip sprite
            frameSprite.flipX = true;
            //Hide back suspention
            backSuspentionSprite.enabled = false;
            //Show front suspention
            frontSuspentionSprite.enabled = true;
            //Sets new location for heads circle collider
            head.offset = new Vector2(head.offset.x - 0.79f, head.offset.y);
            //Player is now facing left
            facingRight = false;
        }
    }


    //Check if players head hits something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If head hits something that is not the flag or coin
        if (!collision.gameObject.CompareTag("Flag") && !collision.gameObject.CompareTag("Coin"))
        {
            //Calls DeathCam function from CameraFollow script
            Camera.main.GetComponent<CameraFollow>().DeathCam();
            //Stops player rotating
            player.freezeRotation = true;
            //Changes player to kinematic and stops all movement
            player.isKinematic = true;
            //Stops all velocity
            player.velocity = Vector3.zero;
            //Plays Death scream sound
            AudioManager.instance.Play("Death");
            //Starts death timer
            StartCoroutine(DeathTimer());
        }
    }


    //Called on death
    IEnumerator DeathTimer()
    {
        //Waits for three seconds
        yield return new WaitForSeconds(3);
        //Stops accelerating sound
        AudioManager.instance.Stop("Motorbike Accelerate");
        //Stops death scream sound
        AudioManager.instance.Stop("Death");
        //Reloads the current scene
        SceneManager.LoadScene(currentScene); 
    }
}
