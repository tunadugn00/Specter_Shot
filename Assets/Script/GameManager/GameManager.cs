using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Upgrading,
    Menu
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState = GameState.Playing;

    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject joyStick;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetState(currentState);
    }
    public  void SetState(GameState newState)
    {
        currentState = newState;

        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        joyStick.SetActive(false);

        Time.timeScale = (currentState == GameState.Playing) ? 1f : 0f;

        switch (newState)
        {
            case GameState.Paused:
                pauseMenu.SetActive(true); break;
            case GameState.GameOver:
                gameOverMenu.SetActive(true); break;
            case GameState.Playing:
                joyStick.SetActive(true); break;
        }
    }


    public void TogglePause()
    {
        if(currentState == GameState.Playing)
        {
            SetState(GameState.Paused);
        }
        else if (currentState == GameState.Paused)
        {
            SetState(GameState.Playing);
        }
        
    }
   
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }
    
}
