using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField] private Image TimerIcon;
    [SerializeField] private TextMeshProUGUI TimerText;

    private float secounds = 0;
    private int minutes = 0;

    private void Start()
    {
        GameManager.Instance.OnGameStateChange += GameManager_OnGameStateChange;
        DeliveryManager.Instance.UpdateStats += DeliveryManager_UpdateStats;
        secounds = (int)GameManager.Instance.GetGameIsPlayingTimer();
        Hide();
    }

    private void DeliveryManager_UpdateStats(object sender, EventArgs e)
    {
        secounds += 10;
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

    private void Update()
    {
        UpdateTimerText();
        secounds -= Time.deltaTime;
        TimerIcon.fillAmount = GameManager.Instance.GetGameIsPlayingTimerNormalised();
        TimerText.text = minutes.ToString("00") + ":" + secounds.ToString("00");
    }

    private void UpdateTimerText()
    {
        if(secounds > 60)
        {
            minutes++;
            secounds = secounds - 60;
        }
        if(secounds <= 0 && minutes != 0)
        {
            minutes--;
            secounds = 59;
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
