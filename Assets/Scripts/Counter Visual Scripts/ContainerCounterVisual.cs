using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private Counter_Container counterContainer;
    
    private Animator ContainerCounterAnimator;
    int OpenCloseTrigger = Animator.StringToHash("OpenClose");

    void Awake()
    {
        ContainerCounterAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        counterContainer.OnPlayerGrabbedObject += CounterContainer_OnPlayerGrabbedObject;
    }

    private void CounterContainer_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        ContainerCounterAnimation();
    }

    private void ContainerCounterAnimation()
    {
        ContainerCounterAnimator.SetTrigger(OpenCloseTrigger);
    }
}
