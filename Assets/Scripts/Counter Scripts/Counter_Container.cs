using System;
using UnityEngine;

public class Counter_Container : Counter_Base
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;


    public override void CounterInteraction(Player_Interactions player)
    {
        if(!player.HasKitchenObject())
        {
            KitchenObjects.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Player is already holding something!");
        }
    }
}
