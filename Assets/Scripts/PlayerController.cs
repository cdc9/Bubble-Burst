using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    //Player Config
    public float playerSpeed;

    //cached components
    Rigidbody2D myRigidBody;
    Collider2D myCollider;

    //Shoot code
    public GameObject projectile, projectileChain, gun;
    public bool isShotFired;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.freezeRotation = true;
        myCollider = GetComponent<Collider2D>();
        isShotFired = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }
    //NOTE: Go to project settings -> Input -> gravity to adjust how quickly the value of GetAxis drops to zero after letting go. Value 3 makes it feel slippery/laggy. Value 10 Stops almost immediately. Alternitively, user GetAxisRaw for -1,0,1 values. 
    //NOTE 2: Go to project settings -> Input -> sensitivity to adjust how quickly the value of X/Y accelerates
    public void Move()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is between -1 and +1 
        Vector2 playerVelocity = new Vector2(controlThrow * playerSpeed, 0); // vector2(x,y) where x is horizontal movement, and y is whatever the current y movement the player is going right now. if you put 0, player would stop all y axis movement
        myRigidBody.velocity = playerVelocity; // set the new velocity
    }

    public void Shoot()
    {
        //TODO add shoot code here
        if(Input.GetButtonDown("Fire1"))
        {
            //Limit the player to only one projectile on screen at a time
            if(isShotFired == false)
            {
                //Create a bullet based on whatever "projectile" the gameObject has assigned
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                newProjectile.transform.position = gun.transform.position;
                isShotFired = true;
            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag   == "Bubble")
        {
            Debug.Log("Game OVER!!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload the scene
        }
    }

}
