using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TotalOrderDeliverdText;
    [SerializeField] private TextMeshProUGUI TotalMoneyEarnedText;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChange += GameManager_OnGameStateChange;
        canvasGroup.alpha = 0f;
        Hide();
    }

    private void GameManager_OnGameStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOverActive())
        {
            TotalOrderDeliverdText.text = DeliveryManager.Instance.GetTotalOrderDeliverd().ToString();
            TotalMoneyEarnedText.text = MoneyManager.Instance.GetTotalMoneyEarned().ToString();
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
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
}
