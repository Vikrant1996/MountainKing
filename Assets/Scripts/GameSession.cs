using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public int playerLives = 5;
    


    void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
        {
            Destroy (gameObject);
        }
        else
        {
            DontDestroyOnLoad (gameObject);
        }
    }
    
    
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TaleLife();
        }
        else
        {
            ResetGameSession();
        }
    }



    void TaleLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    

    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy (gameObject);
    }

    void Update()
    {
    }
}
