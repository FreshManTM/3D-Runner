using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DeathAnim : MonoBehaviour
{
    CanvasGroup canvasGroup;
    TextMeshProUGUI gameOverText;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        gameOverText = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup.DOFade(1, 1.5f);
        Vector3 moveTextTo = new Vector3(gameOverText.transform.position.x, gameOverText.transform.position.y + 800f, gameOverText.transform.position.z);
        gameOverText.transform.DOMove(moveTextTo, 2f);
    }
}
