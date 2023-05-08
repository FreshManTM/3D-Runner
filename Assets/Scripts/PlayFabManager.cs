using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance;

    [SerializeField] TextMeshProUGUI messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    [SerializeField] GameObject loginCanvas;
    [SerializeField] GameObject registrationCanvas;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Login();
    }

    public void RegisterButton()
    {
        loginCanvas.SetActive(false);
        registrationCanvas.SetActive(true);

    }
    public void Register()
    {
        if (passwordInput.text.Length < 6)
            messageText.text = "Password too short!";

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }


    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in!";
        SceneManager.LoadScene(1);
    }
    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in!";
        SceneManager.LoadScene(1);
    }
    void OnError(PlayFabError error)
    {
        messageText.text = ("Error while logging in/creating account!");
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

    public void GetLeaderboard()
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
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}
