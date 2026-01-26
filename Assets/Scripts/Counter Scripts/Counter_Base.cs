using System;
using UnityEngine;

public class Counter_Base : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnDropSomething;

    public static void ResetStaticData()
    {
        OnDropSomething = null;
    }

    [SerializeField] private Transform ObjectSpawnPoint;

    private KitchenObjects kitchenObject;
    

    public virtual void CounterInteraction(Player_Interactions player)
    {
        // This method is meant to be overridden in derived classes.
    }

    public virtual void CounterInteractionAlternate(Player_Interactions player)
    {
        // This method is meant to be overridden in derived classes.
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return ObjectSpawnPoint;
    }

    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null) OnDropSomething?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObjects GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
