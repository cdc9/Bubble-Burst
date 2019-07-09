using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Projectile Configs
    public int damage;
    public float projectileSpeed = 5;

    //cached components
    Rigidbody2D myRigidBody;
    Collider2D myCollider;

    //References to other objects
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }

    //Make bullet move
    public void Fly()
    {
        transform.localScale += new Vector3(0F, .15f, 0f); //Stretch the length of the projectile
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        player.isShotFired = false; //Give player the ability to shoot again
    }
   
}
