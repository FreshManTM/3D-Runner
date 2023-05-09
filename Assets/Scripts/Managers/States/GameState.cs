using UnityEngine;

public class GameState : IState
{
    public void Enter(GameObject canvas)
    {
        Time.timeScale = 1;
    }

    public void Exit() { }

}
