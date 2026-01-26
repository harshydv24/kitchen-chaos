using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Toggle SkipTutorialToggle;
    [SerializeField] private GameObject TutorialScreenComps;

    private int ShowTutorialScreen;
    private CanvasGroup canvasGroup;
    private Animator animator;
    private float time = 0.3f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChange += GameManager_OnGameStateChange;
        ShowTutorialScreen = PlayerPrefs.GetInt("TutorialScreen", 1);
        if(ShowTutorialScreen == 1)
        {
            SkipTutorialToggle.isOn = true;
            TutorialScreenComps.SetActive(true);
            animator.SetBool("TScreenActive", false);
        }
        else
        {
            SkipTutorialToggle.isOn = false;
            TutorialScreenComps.SetActive(false);
        }
    }

    private void GameManager_OnGameStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    public void SkipTutorialScreen()
    {
        if (SkipTutorialToggle.isOn)
        {
            ShowTutorialScreen = 1;
            TutorialScreenComps.SetActive(true);
            animator.SetBool("TScreenActive", false);
        }
        else
        {
            ShowTutorialScreen = 0;
            TutorialScreenComps.SetActive(false);
        }

        PlayerPrefs.SetInt("TutorialScreen", ShowTutorialScreen);
        PlayerPrefs.Save();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        animator.SetBool("TScreenActive", false);
    }

    private void Hide()
    {
        animator.SetBool("TScreenActive", true);
        canvasGroup.LeanAlpha(0f, time).setOnComplete(HideObject).setIgnoreTimeScale(true);
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }
}
