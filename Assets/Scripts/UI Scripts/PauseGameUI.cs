using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResume += GameManager_OnGameResume;
        canvasGroup.alpha = 0f;
        Hide();
    }

    private void GameManager_OnGameResume(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.LeanAlpha(1f, 0.3f).setIgnoreTimeScale(true);
    }

    private void Hide()
    {
        canvasGroup.LeanAlpha(0f, 0.3f).setOnComplete(HideObject).setIgnoreTimeScale(true);
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
