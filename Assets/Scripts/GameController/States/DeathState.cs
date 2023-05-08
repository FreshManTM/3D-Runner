using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState
{
    public void Enter(GameObject deathCanvas)
    {
        Debug.Log("DeathEnter");
        Debug.Log(GameManager.Instance);
        deathCanvas.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("DeathExit");

    }
}
