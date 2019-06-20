using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject winLabel;
    public int bubbleCount;
    private bool allBubblesGone;

    //Use this for initialization
    void Start()
    {
        bubbleCount = 0; // Start the game with zero bubbles counted
        allBubblesGone = false;

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
        if(bubbleCount == 0 && allBubblesGone == true)
        {
            HandleWinCondition();
        }
    }

    //Load the next level in the build index
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HandleWinCondition()
    {
        LoadNextLevel();
    }
}
