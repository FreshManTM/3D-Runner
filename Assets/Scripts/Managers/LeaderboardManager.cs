using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    [SerializeField] GameObject LeaderboardCanvas;
    [SerializeField] GameObject rowPrefab;
    [SerializeField] Transform rowsParent;

    [SerializeField]List<GameObject> rows = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }
    public void LeaderboardButton()
    {
        LeaderboardCanvas.SetActive(true);
        GetLeaderboard();
    }
    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                 new StatisticUpdate
                 {
                     StatisticName = "PlayerScore", Value = GameManager.Instance.GetScore()
                 }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent");
    }

    void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlayerScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {

        foreach (var item in result.Leaderboard)
        {
            GameObject newObj = Instantiate(rowPrefab, rowsParent);
            rows.Add(newObj);
            TextMeshProUGUI[] texts = newObj.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

        }
    }

    public void BackButton()
    {
        foreach (var row in rows)
        {
            Destroy(row);
        }
        rows.Clear();
        LeaderboardCanvas.SetActive(false);
    }
}
