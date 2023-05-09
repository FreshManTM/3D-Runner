using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    [SerializeField] GameObject loginCanvas;
    [SerializeField] GameObject registrationCanvas;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("firstTime"))
        {
            PlayerPrefs.SetInt("firstTime", 0);
            registrationCanvas.SetActive(true);
        }
        else
        {
            loginCanvas.SetActive(true);
        }
    }
    public void RegisterButton()
    {
        loginCanvas.SetActive(false);
        registrationCanvas.SetActive(true);

    }
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
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
}
