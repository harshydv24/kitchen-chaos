using System;
using UnityEngine;

public class Counter_Trash : Counter_Base
{
    public static event EventHandler OnObjectTrashed;

    new public static void ResetStaticData()
    {
        OnObjectTrashed = null;
    }

    public override void CounterInteraction(Player_Interactions player)
    {
        if (player.HasKitchenObject())
        {
            OnObjectTrashed?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestroySelf();
        }
        else
        {
            Debug.Log("Nothing to trash!");
        }
    }
}
