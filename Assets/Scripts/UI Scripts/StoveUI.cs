using UnityEngine;
using UnityEngine.UI;

public class StoveUI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Counter_Stove counterStove;
    [SerializeField] private Transform WarningIcon;
    [SerializeField] private Transform ProgressBar; 
    [SerializeField] private Image ProgressBarImage;

    int IsBurningBool = Animator.StringToHash("IsBurning");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        counterStove.OnStoveProgressChanged += CounterStove_OnStoveProgressChanged;
        ProgressBarImage.fillAmount = 0f;
        HideProgressBar();
        HideWarningIcon();
        animator.SetBool(IsBurningBool, false);
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void CounterStove_OnStoveProgressChanged(object sender, Counter_Stove.OnStoveProgressChangedEventArgs e)
    {
        ProgressBarImage.fillAmount = e.StoveProgress;
        if(e.StoveProgress == 0f || e.StoveProgress == 1f)
        {
            HideProgressBar();
        }
        else
        {
            ShowProgressBar();
        }

        if(e.StoveProgress >= 0.5f && counterStove.IsFriedStateActive())
        {
            ShowWarningIcon();
            animator.SetBool(IsBurningBool, true);
        }
        else
        {
            HideWarningIcon();
            animator.SetBool(IsBurningBool, false);
        }
    }

    private void ShowProgressBar()
    {
        ProgressBar.gameObject.SetActive(true);
    }
    private void HideProgressBar()
    {
        ProgressBar.gameObject.SetActive(false);
    }

    private void ShowWarningIcon()
    {
        WarningIcon.gameObject.SetActive(true);
    }
    private void HideWarningIcon()
    {
        WarningIcon.gameObject.SetActive(false);
    }
}
