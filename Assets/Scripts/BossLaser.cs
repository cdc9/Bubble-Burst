using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{


    public Rigidbody2D myRigidBody;
    public Boss boss;
    public bool bottomBool;

    private bool extendLaser;
    public float attackDuration;

    // Start is called before the first frame update
    void Start()
    {
        extendLaser = true;
        attackDuration = 4f;

        //Set components attached to game objects to the variables
        myRigidBody = GetComponent<Rigidbody2D>();
        boss = FindObjectOfType<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        Extend();
        AttackDuration();
    }

    //Determine how long the laser stays on screen before disappearing
    private void AttackDuration()
    {
        attackDuration -= Time.deltaTime;
        if(attackDuration < 0f)
        {
            if(bottomBool)
                transform.localScale -= new Vector3(.05F, 0f, 0f); //Stretch the length of the projectile for bottom half of laser
            else
                transform.localScale -= new Vector3(.2F, 0f, 0f); //Stretch the length of the projectile for top half of laser
        }
        if(transform.localScale.x < 0)
        {
            Destroy(gameObject);
        }
    }

    public void Extend()
    {
        if(extendLaser == true && bottomBool == true)
        transform.localScale += new Vector3(0F, .05f, 0f); //Stretch the length of the projectile
        transform.position = boss.gun.transform.position;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        extendLaser = false;
    }
}
