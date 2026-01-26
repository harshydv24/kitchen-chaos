using System;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoneyText;

    private void Start()
    {
        GameManager.Instance.OnGameStateChange += GameManager_OnGameStateChange;
        Hide();
    }

    private void Update()
    {
        MoneyText.text = MoneyManager.Instance.GetCurrentMoneyAmount().ToString();
    }

    private void GameManager_OnGameStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameIsPlayingActive() && GameManager.Instance.IsGamePaused() == false)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
