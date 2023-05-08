using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{  
    IState currentState;

    public void ChangeState(IState newState, GameObject canvas)
    {
        if(currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter(canvas);
    }
}
