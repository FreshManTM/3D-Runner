using UnityEngine;

public class AdsState : IState
{
    GameObject _canvas;
    public void Enter(GameObject canvas)
    {
        _canvas = canvas;
        _canvas.SetActive(true);
        //Enable for Android Build
        //AdsManager.Instance.ShowAd();
    }

    public void Exit() 
    {
        _canvas.SetActive(false);
    }
}
