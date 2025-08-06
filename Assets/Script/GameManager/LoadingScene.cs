using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingPanel;
    public RectTransform bulletContainer;
    public GameObject bulletPrefabs;

    public int totalBullets = 20;
    public string sceneToLoad;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("NextScene"))
        {
            return;
        }
        sceneToLoad = PlayerPrefs.GetString("NextScene");
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    IEnumerator LoadSceneAsync (string sceneName)
    {
        loadingPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        int bulletsSpawned = 0;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            int targetBullets = Mathf.FloorToInt(progress * totalBullets);

            while(bulletsSpawned < targetBullets)
            {
                Instantiate(bulletPrefabs, bulletContainer);
                bulletsSpawned++;
                yield return new WaitForSecondsRealtime(0.02f);
            }

            if (operation.progress >= 0.9f && bulletsSpawned >= totalBullets)
            {
                //yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation  = true;
            }
            yield return null;
        }
    }
}
