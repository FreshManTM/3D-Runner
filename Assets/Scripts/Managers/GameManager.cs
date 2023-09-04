using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Diagnostics;
using PlayerMovement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject player;

    [SerializeField] GameObject MenuCanvas;
    [SerializeField] GameObject DeathCanvas;
    [SerializeField] GameObject StartScreenCanvas;
    [SerializeField] GameObject AdsCanvas;
    int score;
    int coins;

    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI scoreText;

    StateMachine stateMachine;
    private void Awake()
    {
        Instance = this; 
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new StartScreenState(), StartScreenCanvas);
    }

    private void Update()
    {
        SetScore();

    }
    void SetScore()
    {
        score = (int)player.transform.position.z / 2;
        scoreText.text = score.ToString();
    }

    public void AddCoins()
    {
        coins++;
        coinsText.text = "Coins: " + coins.ToString();
    }
    public int GetScore()
    {
        return score;
    }
    public void Death()
    {
        stateMachine.ChangeState(new DeathState(), DeathCanvas);
        LeaderboardManager.Instance.SendLeaderboard(score);
    }

    //Buttons
    public void OnPlayButton()
    {
        stateMachine.ChangeState(new GameState(), null);
    }
    public void ShowAdButton()
    {
        stateMachine.ChangeState(new AdsState(), AdsCanvas);
    }
    public void CloseAdButton()
    {
        StateMachine stateMachine = new StateMachine();
        stateMachine.ChangeState(new GameState(), null);
        player.GetComponent<PlayerController>().ResumePosition();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void MenuButton()
    {
        stateMachine.ChangeState(new MenuState(), MenuCanvas);
    }

    public void ResumeButton()
    {
        stateMachine.ChangeState(new GameState(), null);
    }

    public void LogOutButton()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
