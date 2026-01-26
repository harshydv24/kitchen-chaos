using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CountdownText;

    private CanvasGroup canvasGroup;
    private Animator animator;
    private int PrvCountdownNumber;
    private int CountdownAnimTrigger = Animator.StringToHash("Countdown");

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChange += GameManager_OnGameStateChange;
        canvasGroup.alpha = 0f;
        Hide();
    }

    private void GameManager_OnGameStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
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
        int CurrCountdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownTimer());
        CountdownText.text = CurrCountdownNumber.ToString();
        if(PrvCountdownNumber != CurrCountdownNumber)
        {
            PrvCountdownNumber = CurrCountdownNumber;
            animator.SetTrigger(CountdownAnimTrigger);
            SoundManager.Instance.PlayCountdownTimerSound();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.LeanAlpha(1f, 0.35f).setIgnoreTimeScale(true);
    }

    private void Hide()
    {
        canvasGroup.LeanAlpha(0f, 0.4f).setOnComplete(HideObject).setIgnoreTimeScale(true);
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }
}
