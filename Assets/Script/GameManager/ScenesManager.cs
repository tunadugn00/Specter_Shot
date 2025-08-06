using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void LoadSceneWithLoading(string sceneName)
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("NextScene", sceneName);
        SceneManager.LoadScene("LoadingScene");
    }
    public void MainMenu()
    {
        LoadSceneWithLoading("MainMenu");
    }

    public void LevelMenu()
    {
        LoadSceneWithLoading("LevelMenu");
    }
    public void GoToLevel(string levelName)
    {
        LoadSceneWithLoading(levelName);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        LoadSceneWithLoading(SceneManager.GetActiveScene().name);
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
