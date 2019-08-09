using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectiles : MonoBehaviour
{
    
    //Projectile Configs
    public int damage;
    public float projectileSpeed = -10;
    public bool destroyOnHit = false;
    public ParticleSystem explosionParticle;

    //Forces
    public Vector2 startingForce = new Vector2(0f, 0f);

    //cached components
    Rigidbody2D myRigidBody;
    Collider2D myCollider;

    //References to other objects
    

    // Start is called before the first frame update
    void Start()
    {
        //Set components attached to game objects to the variables
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();

        myRigidBody.AddForce(startingForce, ForceMode2D.Impulse); //This is the starting direction of the bubble bouncing
    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        //If the bubble collides with player projectile, split the bubble in two and reduce bubble count;
        if (collision.gameObject.tag.Trim().Equals("Projectile"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyOnHit == true)
        {
            Instantiate(explosionParticle,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
