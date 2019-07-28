using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    //cached components
    Rigidbody2D myRigidbody;
    Collider2D myCollider;
    Animator myAnimator;

    //Boss Configs
    public int health;
    public float bossSpeed;
    public GameObject gun;
    public GameObject[] projectiles;
    public int currentBubbleIndex;
    public float timeTillNextAttack;

    private Vector2 bossVelocity;
    private int laserMeter;

    // Start is called before the first frame update
    void Start()
    {
        //Set components attached to game objects to the variables
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();

        bossVelocity = new Vector2(bossSpeed, 0); // vector2(x,y) where x is horizontal movement, and y is whatever the current y movement the player is going right now. if you put 0, player would stop all y axis movement
        timeTillNextAttack = 2f;
        currentBubbleIndex = 0;
        laserMeter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    private void Attack()
    {
        timeTillNextAttack -= Time.deltaTime;
        if(timeTillNextAttack <= 0f)
        {

            if (laserMeter >= 4)
            {
                GameObject newLaser = Instantiate(projectiles[2], transform.position, Quaternion.identity) as GameObject;
                newLaser.transform.position = gun.transform.position;
                laserMeter = 0;
                currentBubbleIndex = 0;
                timeTillNextAttack = 5f;
                return;
            }
            if (currentBubbleIndex == 1 && laserMeter < 4)
            {
                GameObject newBubble1 = Instantiate(projectiles[currentBubbleIndex], transform.position, Quaternion.identity) as GameObject;
                newBubble1.GetComponent<BossProjectiles>().destroyOnHit = true;
                laserMeter++;
                currentBubbleIndex--;
                timeTillNextAttack = 3f;
                return;
            }
            if (currentBubbleIndex == 0 && laserMeter < 4)
            {
                //Create a bullet based on whatever "projectile" the gameObject has assigned
                GameObject newProjectile = Instantiate(projectiles[0], transform.position, Quaternion.identity) as GameObject;
                newProjectile.transform.position = gun.transform.position;
                laserMeter++;
                currentBubbleIndex++;
                timeTillNextAttack = 1.5f;
                return;
            }

        }
    }

    public void Move()
    {
        myRigidbody.velocity = bossVelocity; // set the new velocity
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Trim().Equals("Foreground"))
        {
            bossVelocity *= -1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //If the bubble collides with player projectile, split the bubble in two and reduce bubble count;
        if (collision.gameObject.tag.Trim().Equals("Projectile"))
        {
            health--;
            //levelManager.HandleWinCondition();
            if(health <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
