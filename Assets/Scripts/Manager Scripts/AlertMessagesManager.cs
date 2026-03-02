using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AlertMessagesManager : MonoBehaviour
{
    public static AlertMessagesManager Instance { get; private set; }

    public event EventHandler ShowAlertMessage;
    public event EventHandler HideAlertMessage;

    [SerializeField] private TextMeshProUGUI AlertMessageText;
    [SerializeField] private float AlertMessageDuration = 2.5f;

    private void Awake()
    {
        Instance = this;
    }

    public void DisplayAlertMessage(string alertMessage)
    {
        StartCoroutine(AlertMessageCoroutine(alertMessage));
    }

    private IEnumerator AlertMessageCoroutine(string alertMessage)
    {
        AlertMessageText.text = alertMessage;
        SoundManager.Instance.playAlertMessageSound();
        ShowAlertMessage?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSecondsRealtime(AlertMessageDuration);
        HideAlertMessage?.Invoke(this, EventArgs.Empty);
    }
}
