using UnityEngine;

public interface IState 
{
    void Enter(GameObject canvas);
    void Exit();
}
