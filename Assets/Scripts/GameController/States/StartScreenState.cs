using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenState : IState
{
    GameObject canvas;
    public void Enter(GameObject startScreenCanvas)
    {
        Debug.Log("StartScreenEnter");

        canvas = startScreenCanvas;
        Time.timeScale = 0;
        canvas.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("StartScreenExit");
        Time.timeScale = 1;

        canvas.SetActive(false);
    }
}
