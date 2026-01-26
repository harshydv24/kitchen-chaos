using System;
using UnityEngine;

public class Counter_Cuttings : Counter_Base
{
    // static event for playing cutting sound.
    public static event EventHandler OnAnyCut;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    // event for chaning the progress bar UI
    public event EventHandler<OnCuttingProgressChangedEventArgs> OnCuttingProgressChanged;
    public class OnCuttingProgressChangedEventArgs : EventArgs
    {
        public float CuttingProgressNormalized;
    }

    // event for triggering cutting counter animation
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] CuttingRecipeSOList;

    private bool Locked = false;    
    private int CurrentKnifeCuts;
    private int CounterPrice = 30;

    public override void CounterInteraction(Player_Interactions player)
    {
        if (!Locked)
        {
            if(!HasKitchenObject())
            {
                if (player.HasKitchenObject())
                {
                    if (RecipeExistsWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        CurrentKnifeCuts = 0;

                        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs
                        {
                            CuttingProgressNormalized = (float)CurrentKnifeCuts / cuttingRecipeSO.MaxCuts
                        });
                    }
                    else
                    {
                        Debug.Log("Object can not be cutted!");
                    }
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
                        Debug.Log("Counter if Full!");
                    }
                }
                else
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }
        else
        {
            if(MoneyManager.Instance.GetCurrentMoneyAmount() >= CounterPrice)
            {
                Locked = false;
                MoneyManager.Instance.DeductMoney(CounterPrice);
            }
            else
            {
                Debug.Log("Insufficient Money!");
            }
        }
    }

    public override void CounterInteractionAlternate(Player_Interactions player)
    {
        if(HasKitchenObject())
        {
            if (RecipeExistsWithInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                CurrentKnifeCuts++;
                // Firing the OnCut event to trigger cutting animation
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);
                
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                // Firing the OnCuttingProgressChanged event to update progress bar UI
                OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs
                {
                    CuttingProgressNormalized = (float)CurrentKnifeCuts / cuttingRecipeSO.MaxCuts
                });

                if(CurrentKnifeCuts >= cuttingRecipeSO.MaxCuts)
                { 
                    KitchenObjectsSO OutputKitchenObjectSO = RecipeInputToOutput(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                    KitchenObjects.SpawnKitchenObject(OutputKitchenObjectSO, this);
                }
            }
            else
            {
                Debug.Log("Object already cutted or can not be cutted!");
            }
        }
        else
        {
            Debug.Log("Need Kitchen Object to Cut!");
        }
    }

    private bool RecipeExistsWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectsSO RecipeInputToOutput(KitchenObjectsSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if(cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.OutputKitchenObjectSO;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in CuttingRecipeSOList)
        {
            if(cuttingRecipeSO.InputKitchenObjectSO == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

    public bool IsCounterLocked()
    {
        return Locked;
    }
}
