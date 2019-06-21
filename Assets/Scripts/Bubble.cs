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

    //Outside references
    LevelManager levelManager;

    void Start()
    {
        currentBubbleIndex = 0;
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        levelManager = FindObjectOfType<LevelManager>();

        
        myRigidBody.AddForce(startingForce, ForceMode2D.Impulse); //This is the starting direction of the bubble bouncing
        levelManager.bubbleCount++; //Keep track of how many bubbles are on screen in the level manager

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
            Split();
            levelManager.totalLevelBubbles--;
            levelManager.HandleWinCondition();
            Destroy(gameObject);
        }
    }

    //If the bubble is not a green bubble (smallest possible bubble), then spawn the next two smaller bubbles and shoot them in opposing directions
    public void Split()
    {
        if (!gameObject.name.Contains("Green Bubble"))
        {
            GameObject newBubble1 = Instantiate(bubbles[currentBubbleIndex + 1], transform.position, Quaternion.identity) as GameObject;
            newBubble1.GetComponent<Bubble>().startingForce = new Vector2(4f, 6f);
            GameObject newBubble2 = Instantiate(bubbles[currentBubbleIndex + 1], transform.position, Quaternion.identity) as GameObject;
            newBubble2.GetComponent<Bubble>().startingForce = new Vector2(-4f, 6f);
        }
        else
        {
            
            Destroy(gameObject);
            
        }


    }
}
