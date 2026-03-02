using System;
using UnityEngine;

public class AlertMessageUI : MonoBehaviour
{
    [SerializeField] private Vector3 DisableScale;
    [SerializeField] private Vector3 EnableScale;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Hide();
        canvasGroup.alpha = 0f;
        AlertMessagesManager.Instance.ShowAlertMessage += AlertMessagesManager_ShowAlertMessage;
        AlertMessagesManager.Instance.HideAlertMessage += AlertMessagesManager_HideAlertMessage;
    }


    private void AlertMessagesManager_ShowAlertMessage(object sender, EventArgs e)
    {
        EnableAnimation();
    }
    
    private void AlertMessagesManager_HideAlertMessage(object sender, EventArgs e)
    {
        DisableAnimation();
    }

    private void EnableAnimation()
    {
        Show();
        canvasGroup.LeanAlpha(1f, 0.2f).setIgnoreTimeScale(true);
        gameObject.transform.LeanScale(EnableScale, 0.3f).setEaseOutExpo().setIgnoreTimeScale(true);
    }

    private void DisableAnimation()
    {
        canvasGroup.LeanAlpha(0f, 0.3f).setIgnoreTimeScale(true);
        gameObject.transform.LeanScale(DisableScale, 0.2f).setEaseInExpo().setIgnoreTimeScale(true).setOnComplete(Hide);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
