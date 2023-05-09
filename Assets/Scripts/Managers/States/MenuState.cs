using UnityEngine;

public class MenuState : IState
{
    GameObject canvas;
    public void Enter(GameObject startScreenCanvas)
    {
        canvas = startScreenCanvas;
        Time.timeScale = 0;
        canvas.SetActive(true);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);
    }
}
