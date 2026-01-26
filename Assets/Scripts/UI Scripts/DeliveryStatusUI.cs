using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryStatusUI : MonoBehaviour
{
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Image IconImage;
    [SerializeField] private TextMeshProUGUI StatusText;
    [SerializeField] private Color SuccessColor;
    [SerializeField] private Color FailedColor;
    [SerializeField] private Sprite SuccessIcon;
    [SerializeField] private Sprite FailedIcon;
    [SerializeField] private float targetPos;
    [SerializeField] private float DefaultPos;

    private CinemachineImpulseSource impulseSource;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySucess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        Hide();
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        Show();
        ShowDeliveryStatus(SuccessColor, SuccessIcon, "Delivery\nSuccess");
        StartCoroutine(DeliveryStatusAnimation());
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        Show();
        ShowDeliveryStatus(FailedColor, FailedIcon, "Delivery\nFailed");
        StartCoroutine(DeliveryStatusAnimation());
        impulseSource.GenerateImpulse();
    }

    private void ShowDeliveryStatus(Color activeColor, Sprite activeIcon, string statusMessage)
    {
        BackgroundImage.color = activeColor;
        IconImage.sprite = activeIcon;
        StatusText.text = statusMessage;
    }

    private IEnumerator DeliveryStatusAnimation()
    {
        transform.LeanMoveY(targetPos, 0.5f).setEaseOutExpo().setIgnoreTimeScale(true);
        canvasGroup.LeanAlpha(1f, 0.3f).setIgnoreTimeScale(true);

        yield return new WaitForSeconds(3.5f);

        canvasGroup.LeanAlpha(0f, 0.3f).setIgnoreTimeScale(true);
        transform.LeanMoveY(DefaultPos, 0.5f).setEaseInExpo().setOnComplete(Hide).setIgnoreTimeScale(true);
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
