using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private Counter_Base counterBase;
    [SerializeField] private GameObject[] SelectedCounterVisualGameObjects;
    //[SerializeField] private GameObject UnlockDialogBox;

    private void Start()
    {
        Player_Interactions.Instance.OnSelectedCounterChanged += Player_Interactions_OnSelectedCounterChanged;
    }

    private void Player_Interactions_OnSelectedCounterChanged(object sender, Player_Interactions.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == counterBase)
        {
            EnableSelectedCounterVisual();
        }
        else
        {
            DisableSelectedCounterVisual();
        }
    }

    private void EnableSelectedCounterVisual()
    {
        foreach(GameObject Object in SelectedCounterVisualGameObjects)
        {
            Object.SetActive(true);
        }
    }
    private void DisableSelectedCounterVisual()
    {
        foreach(GameObject Object in SelectedCounterVisualGameObjects)
        {
            Object.SetActive(false);
        }
    }
}
