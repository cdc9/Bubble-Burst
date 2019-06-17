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


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }

    //Make bullet move
    public void Fly()
    {
        //myRigidBody.velocity = new Vector2(0, projectileSpeed);
        transform.localScale += new Vector3(0F, .25f, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
   
}
