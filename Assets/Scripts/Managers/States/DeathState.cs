using UnityEngine;
using DG.Tweening;
using TMPro;

public class DeathState : IState
{
    GameObject canvas;
    CanvasGroup canvasGroup;
    TextMeshProUGUI gameOverText;
    public void Enter(GameObject deathCanvas)
    {
        canvas = deathCanvas;
        canvas.SetActive(true);
        CanvasFade();
        AdsManager.Instance.AdsInitialize();
    }

    public void Exit()
    {
        canvasGroup.alpha = 0;
        Vector3 moveTextTo = new Vector3(gameOverText.transform.position.x, gameOverText.transform.position.y - 850f, gameOverText.transform.position.z);
        gameOverText.transform.DOMove(moveTextTo, 0f);
        canvas.SetActive(false);
    }

    void CanvasFade()
    {
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        gameOverText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        gameOverText.transform.localPosition = new Vector3(0f, -850f, 0f);
        canvasGroup.DOFade(1, 1.5f);
        Vector3 moveTextTo = new Vector3(gameOverText.transform.position.x, gameOverText.transform.position.y + 850f, gameOverText.transform.position.z);
        gameOverText.transform.DOMove(moveTextTo, 1.5f);
    }
}
