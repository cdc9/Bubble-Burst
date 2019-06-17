using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D myRigidBody;
    Collider2D myCollider;

    //Bubble index
    public GameObject[] bubbles;
    public int currentBubbleIndex;

    //Forces
    public Vector2 startingForce = new Vector2(10f, 1f);

    void Start()
    {
        currentBubbleIndex = 0;
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();

        //myRigidBody.velocity = new Vector2(3f, 1f);
        myRigidBody.AddForce(startingForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Trim().Equals("Projectile")) ;
        {
            Split();
            Destroy(gameObject);
        }
    }

    public void Split()
    {
        if (!gameObject.name.Contains("Green Bubble"))
        {
            GameObject newBubble1 = Instantiate(bubbles[currentBubbleIndex + 1], transform.position, Quaternion.identity) as GameObject;
            newBubble1.GetComponent<Bubble>().startingForce = new Vector2(4f, 8f);
            GameObject newBubble2 = Instantiate(bubbles[currentBubbleIndex + 1], transform.position, Quaternion.identity) as GameObject;
            newBubble2.GetComponent<Bubble>().startingForce = new Vector2(-4f, 8f);
        }
        else
        {
            Destroy(gameObject);
        }


    }
}
