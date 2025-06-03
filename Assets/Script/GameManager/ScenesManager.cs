using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }
    public void GoToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PersonalMenu()
    {
        SceneManager.LoadScene("Personal");
    }
    public void StoreMenu()
    {
        SceneManager.LoadScene("StoreMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
