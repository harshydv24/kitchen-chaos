using UnityEngine;

public class CuttingsCounterVisual : MonoBehaviour
{
    [SerializeField] private Counter_Cuttings counterCutting;
    [SerializeField] private GameObject[] LockedVisualsList;
    
    private Animator CuttingCounterAnimator;
    int CutTrigger = Animator.StringToHash("Cut");

    void Awake()
    {
        CuttingCounterAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        counterCutting.OnCut += CounterContainer_OnCut;
    }

    private void Update()
    {
        if (counterCutting.IsCounterLocked())
        {
            foreach(GameObject Object in LockedVisualsList)
            {
                Object.SetActive(true);
            }
        }
        else
        {
            foreach(GameObject Object in LockedVisualsList)
            {
                Object.SetActive(false);
            }
        }
    }

    private void CounterContainer_OnCut(object sender, System.EventArgs e)
    {
        ContainerCounterAnimation();
    }

    private void ContainerCounterAnimation()
    {
        CuttingCounterAnimator.SetTrigger(CutTrigger);
    }
}
