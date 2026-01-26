using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private Image LevelProgressBar;
    [SerializeField] private TextMeshProUGUI ProgressText;

    private void Awake()
    {
        Time.timeScale = 1f;
        LoadingScreen.SetActive(false);
    }

    public void LoadNextLevel(int buildIndex)
    {
        LoadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync(buildIndex));
    }

    private IEnumerator LoadLevelAsync(int buildIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(buildIndex);
        while (!asyncOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            LevelProgressBar.fillAmount = progressValue;
            ProgressText.text = (progressValue * 100f + "%").ToString();
            yield return null;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
