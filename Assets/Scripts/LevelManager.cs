using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject winLabel;
    public GameObject[] levelBubbles;
    public int bubbleCount;
    public int totalLevelBubbles;


    //Use this for initialization
    void Start()
    {
        levelBubbles = GameObject.FindGameObjectsWithTag("Bubble");

        foreach(GameObject bubble in levelBubbles)
        {
            if(bubble.name.Contains("Green"))
            {
                totalLevelBubbles +=  1;
            }
            if (bubble.name.Contains("Red"))
            {
                totalLevelBubbles += 3;
            }
            if (bubble.name.Contains("Yellow"))
            {
                totalLevelBubbles += 7;
            }
            if (bubble.name.Contains("Blue"))
            {
                totalLevelBubbles += 15;
            }
        }
        Debug.Log(totalLevelBubbles);

        //Find the you win text banner and set it to false
        FindYouWin();
        winLabel.SetActive(false);
    }

    //Find the you win banner, if it doesn't exist, log warning
    void FindYouWin()
    {
        winLabel = GameObject.Find("You Win");

        if (!winLabel)
        {
            Debug.LogWarning("Please create You Win game object!");
        }
    }

    //Load a level of a particular name
    public void LoadLevel(string name)
    {
        Debug.Log("Level requested for " + name);
        SceneManager.LoadScene(name);
    }
    //Quit the game method
    public void QuitLevel()
    {
        Application.Quit();
    }

    //Count all remaining bubbles, if 0, win the level
    void Update()
    {
        Debug.Log(totalLevelBubbles);
    }

    //Load the next level in the build index
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HandleWinCondition()
    {
        if (totalLevelBubbles == 0)
        {
            Debug.Log("You win!");
            LoadNextLevel();
        }
        
    }
}
