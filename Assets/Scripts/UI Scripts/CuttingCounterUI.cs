using UnityEngine;
using UnityEngine.UI;

public class CuttingCounterUI : MonoBehaviour
{
    [SerializeField] private Counter_Cuttings counterCuttings;
    [SerializeField] private GameObject ProgressBar;
    [SerializeField] private Image ProgressBarImage;

    private void Start()
    {
        counterCuttings.OnCuttingProgressChanged += CounterCuttings_OnCuttingProgressChanged;
        ProgressBarImage.fillAmount = 0f;
        HideProgressBar();
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void CounterCuttings_OnCuttingProgressChanged(object sender, Counter_Cuttings.OnCuttingProgressChangedEventArgs e)
    {
        ProgressBarImage.fillAmount = e.CuttingProgressNormalized;
        if(e.CuttingProgressNormalized == 0f || e.CuttingProgressNormalized == 1f)
        {
            HideProgressBar();
        }
        else
        {
            ShowProgressBar();
        }
    }

    private void ShowProgressBar()
    {
        ProgressBar.SetActive(true);
    }
    private void HideProgressBar()
    {
        ProgressBar.SetActive(false);
    }
}
