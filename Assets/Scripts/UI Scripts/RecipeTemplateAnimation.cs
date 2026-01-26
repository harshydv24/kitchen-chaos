using UnityEngine;

public class RecipeTemplateAnimation : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private void Start()
    {
        canvasGroup.LeanAlpha(1f, 0.2f).setIgnoreTimeScale(true);
        transform.LeanScaleX(1f, 0.3f).setEaseOutExpo().setIgnoreTimeScale(true);
        transform.LeanScaleY(1f, 0.3f).setEaseOutExpo().setIgnoreTimeScale(true);
    }

    private void OnDestroy()
    {
        canvasGroup.LeanAlpha(0f, 0.3f).setIgnoreTimeScale(true);
        transform.LeanScaleX(0.5f, 0.2f).setEaseOutExpo().setIgnoreTimeScale(true);
        transform.LeanScaleY(0.5f, 0.2f).setEaseOutExpo().setIgnoreTimeScale(true);
    }
}
