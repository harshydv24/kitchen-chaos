using UnityEngine;

public class Counter_Clear : Counter_Base
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;


    public override void CounterInteraction(Player_Interactions player)
    {
        if(!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                Debug.Log("Counter is Empty!");
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if(player.GetKitchenObject().TryGetPlate(out KitchenObject_Plate kitchenObjectPlate))
                {
                    if (kitchenObjectPlate.TryAddIngredients(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    if(GetKitchenObject().TryGetPlate(out kitchenObjectPlate))
                    {
                        if (kitchenObjectPlate.TryAddIngredients(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                    else
                    {
                        Debug.Log("Counter if Full!");    
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
