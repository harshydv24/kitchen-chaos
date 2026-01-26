using UnityEngine;

public class Counter_Delivery : Counter_Base
{
    // Creating the instance of this because there is going to be only one delivery counter in the game as of now.
    public static Counter_Delivery Instance{get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public override void CounterInteraction(Player_Interactions player)
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out KitchenObject_Plate kitchenObjectPlate))
            {
                DeliveryManager.Instance.DeliverRecipe(kitchenObjectPlate);
                player.GetKitchenObject().DestroySelf();
            }
            else
            {
                Debug.Log("Cannot Deliver this!");
            }
        }
        else
        {
            Debug.Log("Nothing to Dilvery!");
        }
    }
}
