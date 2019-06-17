using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBubbles : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] bubbles;
    public int currentBubbleIndex;
    void Start()
    {
        GameObject newBubble = Instantiate(bubbles[currentBubbleIndex], transform.position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Split()
    {
        GameObject newBubble = Instantiate(bubbles[currentBubbleIndex], transform.position, Quaternion.identity) as GameObject;
    }
}
