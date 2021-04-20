/*****************************************************************************
// File Name :         GameManager.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool paused = false;

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        paused = false;
        Time.timeScale = 1;
    }
}
