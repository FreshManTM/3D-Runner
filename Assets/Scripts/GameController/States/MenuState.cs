using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : IState
{
    GameObject canvas;
    public void Enter(GameObject startScreenCanvas)
    {
        Debug.Log("MenuScreenEnter");

        canvas = startScreenCanvas;
        Time.timeScale = 0;
        canvas.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("MenuScreenExit");
        Time.timeScale = 1;

        canvas.SetActive(false);
    }
}
