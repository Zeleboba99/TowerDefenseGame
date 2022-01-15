using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUi;
    public GameObject winUi;
    public static bool GameIsOver;

    private void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver) 
            return;
        if (PlayerStats.Lives <= 0)
        {
            EndGame(gameOverUi);    
        }

        if (PlayerStats.isWin)
        {
            EndGame(winUi);
        }
    }

    private void EndGame(GameObject ui)
    {
        GameIsOver = true;
        ui.SetActive(true);
    }
}
