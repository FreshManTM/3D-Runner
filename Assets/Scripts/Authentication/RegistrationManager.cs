using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RegistrationManager : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] TMP_InputField repeatPasswordInput;

    [SerializeField] TextMeshProUGUI messageText;

    public void Register()
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }

        if(passwordInput.text == repeatPasswordInput.text)
        {
            var request = new RegisterPlayFabUserRequest
            {
                Email = emailInput.text,
                Username = nameInput.text,
                Password = passwordInput.text,
                RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
            SceneManager.LoadScene(1);
        }
        else
        {
            messageText.text = "Invalid password";
        }
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in!";
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = result.Username
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
        SceneManager.LoadScene(1);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Username updated");
    }

    void OnError(PlayFabError error)
    {
        messageText.text = ("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }
}
