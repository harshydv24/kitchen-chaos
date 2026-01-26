using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private Counter_Stove stoveCounter;
    [SerializeField] private GameObject StoveOnVisual;
    [SerializeField] private GameObject FryingVisual;
    [SerializeField] private GameObject[] LockedVisualsList;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, Counter_Stove.OnStateChangedEventArgs e)
    {
        bool showVisuals = e.state == Counter_Stove.StoveState.Frying || e.state == Counter_Stove.StoveState.Fried;
        StoveOnVisual.SetActive(showVisuals);
        FryingVisual.SetActive(showVisuals);
    }

    private void Update()
    {
        if (stoveCounter.IsCounterLocked())
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
}
