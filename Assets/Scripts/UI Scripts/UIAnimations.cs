using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    public static UIAnimations Instance {get; set;}

    void Awake()
    {
        Instance = this;
    }

    public void FadeInAnimation(CanvasGroup canvasGroup, float time)
    {
        canvasGroup.LeanAlpha(1f, time).setIgnoreTimeScale(true);
    }

    public void FadeOutAnimation(CanvasGroup canvasGroup, float time)
    {
        canvasGroup.LeanAlpha(0f, time).setIgnoreTimeScale(true);
    }

    public void PanelPopUpAnim(Transform transform, float time)
    {
        transform.LeanScaleX(1f, time).setEaseOutExpo().setIgnoreTimeScale(true);
        transform.LeanScaleY(1f, time).setEaseOutExpo().setIgnoreTimeScale(true);
    }

    public void PanelPopDownAnim(Transform transform, float Xscale, float Yscale, float time)
    {
        transform.LeanScaleX(Xscale, time).setEaseInExpo().setIgnoreTimeScale(true);
        transform.LeanScaleY(Yscale, time).setEaseInExpo().setIgnoreTimeScale(true);
    }

    private void ClosePanel()
    {
        transform.gameObject.SetActive(false);
    }

    private void OpenPanel(Transform transform)
    {
        transform.gameObject.SetActive(true);
    }
}
