using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    // When this is called for the first time, check to see if there are other game session scripts running. If there are, delete this new one. This is to make sure only 1 game session persists between player lives and reloading scene
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.activeSceneChanged += DestroyOnMenuScreen;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyOnMenuScreen(Scene oldScene, Scene newScene)
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        string sceneName = "Level6";
        if (SceneManager.GetActiveScene().name == sceneName)  //could compare Scene.name instead
        {
            if(gameObject != null)
            Destroy(gameObject); //change as appropriate
        }
    }
}
