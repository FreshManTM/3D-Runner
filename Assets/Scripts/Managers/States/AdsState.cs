using UnityEngine;

public class AdsState : IState
{
    public void Enter(GameObject canvas)
    {
        AdsManager.Instance.ShowAd();
    }

    public void Exit() { }
}
