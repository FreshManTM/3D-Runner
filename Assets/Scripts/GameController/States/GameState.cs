using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : IState
{
    public void Enter(GameObject canvas)
    {
        Time.timeScale = 1;
        Debug.Log("GameStateEnter");
    }

    public void Exit()
    {

    }

}
