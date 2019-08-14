using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    //Player Config
    public float playerSpeed;
    public bool harpoonProjectile;
    public bool arrowProjectile;
    public int projectileCount;
    public Sprite deathSprite;

    private bool paused;
    private bool dying;
    private bool isFacingRight;
    [SerializeField] AudioClip shootingSFX;
    [SerializeField] AudioClip deathSFX;

    //cached components
    Rigidbody2D myRigidbody;
    Collider2D myCollider;
    Animator myAnimator;
    

    //Shoot code
    public GameObject projectile, projectileChain, gun;
    public GameObject[] projectileType;


    // Start is called before the first frame update
    void Start()
    {
        //Set all of the components attached to the object to our variables
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.freezeRotation = true;
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();

        //Projectile starting values
        projectile = projectileType[0];
        harpoonProjectile = true;
        arrowProjectile = false;
        projectileCount = 0;
        paused = false;
        dying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dying)
        {
            Move();
            FlipSprite();
            Shoot();
            SetProjectile();
        }

    }



    //NOTE: Go to project settings -> Input -> gravity to adjust how quickly the value of GetAxis drops to zero after letting go. Value 3 makes it feel slippery/laggy. Value 10 Stops almost immediately. Alternitively, user GetAxisRaw for -1,0,1 values. 
    //NOTE 2: Go to project settings -> Input -> sensitivity to adjust how quickly the value of X/Y accelerates
    //Player movement
    public void Move()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is between -1 and +1 
        Vector2 playerVelocity = new Vector2(controlThrow * playerSpeed, 0); // vector2(x,y) where x is horizontal movement, and y is whatever the current y movement the player is going right now. if you put 0, player would stop all y axis movement
        myRigidbody.velocity = playerVelocity; // set the new velocity

        //Determine if the player''s running animation should be playing. If velocity is 0, stop running.
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }
    //Player shooting projectiles 
    public void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //Limit the player to only one projectile on screen at a time for harpoons
            if(projectileCount < 1 && harpoonProjectile == true)
            {
                AudioSource.PlayClipAtPoint(shootingSFX, Camera.main.transform.position); //Play the popping sound effect
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;  //Create a bullet based on whatever "projectile" the gameObject has assigned
                newProjectile.transform.position = gun.transform.position; //Set the position equal to the "gun" object attached to player object
                projectileCount++; //Add to total number of projectiles on screen
            }

            //Limit the player to only two projectile on screen at a time for Arrows
            if (projectileCount < 2 && arrowProjectile == true)
            {
                AudioSource.PlayClipAtPoint(shootingSFX, Camera.main.transform.position); //Play the popping sound effect
                //Create a bullet based on whatever "projectile" the gameObject has assigned
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                newProjectile.transform.position = gun.transform.position;
                projectileCount++;
            }
            

        }

        if (Input.GetButtonDown("Jump"))
        {
            if(paused)
            {
                Time.timeScale = 1;
            }
            if(!paused)
            {
                Time.timeScale = 0;
            }

            paused = !paused;

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //When player colliders with a bubble tagged object, restart the current level
        if (collision.collider.tag   == "Bubble")
        {
            Debug.Log("Game OVER!!");
            StartCoroutine("Dying");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When player colliders with a bubble tagged object, restart the current level
        if (collision.GetComponent<Collider2D>().tag == "Bubble")
        {
            Debug.Log("Game OVER!!");
            StartCoroutine("Dying");
        }
    }

    private void FlipSprite()
    {
        //If the player is moving horizontally
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        //Determine which way the player should face depending on his +/- x speed. As well as check to see if he's sliding
        if (playerHasHorizontalSpeed == true)
        {
            // reverse the current scaling if the x myRigidbody
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
            //Determine if the player should face left or right. This is checking if the player's velocity is positive or negative. 
            if ((Mathf.Sign(myRigidbody.velocity.x) > Mathf.Epsilon))
                isFacingRight = true;
            else if ((Mathf.Sign(myRigidbody.velocity.x) < Mathf.Epsilon))
                isFacingRight = false;
        }
    }

    //Developer cheat to quickjly switch between projectile types
    private void SetProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            projectile = projectileType[0];
            harpoonProjectile = true;
            arrowProjectile = false;
            Debug.Log("Projectile has been changed to " + projectileType[0]);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            projectile = projectileType[1];
            harpoonProjectile = false;
            arrowProjectile = true;
            Debug.Log("Projectile has been changed to " + projectileType[1]);
        }
    }

    //Coroutine for dealing with the playing dying
    IEnumerator Dying()
    {
        myAnimator.SetBool("isDead", true); //Turn the player red in the animation
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position); //Play the death sound effect
        Time.timeScale = 0; //pause time
        dying = true; //Set to true to stop player inputs 
        yield return new WaitForSecondsRealtime(3); //Pause for three seconds, this will ignore the fact that time is stopped for everyone else
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload the scene
        Time.timeScale = 1; //Turn the time back to normal
    }

}
