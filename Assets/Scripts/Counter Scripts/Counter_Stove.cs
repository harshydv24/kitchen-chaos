using System;
using UnityEngine;

public class Counter_Stove : Counter_Base
{
    public event EventHandler<OnStoveProgressChangedEventArgs> OnStoveProgressChanged;
    public class OnStoveProgressChangedEventArgs : EventArgs
    {
        public float StoveProgress;
    }

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public StoveState state;
    }

    public enum StoveState
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOList;

    private bool Locked = false;
    private StoveState state;
    private float CurrentFryingTime;
    private float CurrentBurningTime;
    private FryingRecipeSO fryingRecipeSO;
    private FryingRecipeSO burningRecipeSO;
    private int CounterPrice = 120;


    private void Start()
    {
        state = StoveState.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject()){
            switch(state)
            {
                case StoveState.Idle:
                    break;
                case StoveState.Frying:
                    CurrentFryingTime += Time.deltaTime;

                    OnStoveProgressChanged?.Invoke(this, new OnStoveProgressChangedEventArgs
                    {
                        StoveProgress = CurrentFryingTime / fryingRecipeSO.MaxFryingTime
                    });

                    if(CurrentFryingTime > fryingRecipeSO.MaxFryingTime)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObjects.SpawnKitchenObject(fryingRecipeSO.OutputFryingKitchenObjectSO, this);

                        state = StoveState.Fried;
                        CurrentBurningTime = 0f;
                        burningRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case StoveState.Fried:
                    CurrentBurningTime += Time.deltaTime;

                    OnStoveProgressChanged?.Invoke(this, new OnStoveProgressChangedEventArgs
                    {
                        StoveProgress = CurrentBurningTime / fryingRecipeSO.MaxBurningTime
                    });

                    if(CurrentBurningTime > burningRecipeSO.MaxFryingTime)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObjects.SpawnKitchenObject(burningRecipeSO.OutputFryingKitchenObjectSO, this);
                        state = StoveState.Burned;

                        OnStoveProgressChanged?.Invoke(this, new OnStoveProgressChangedEventArgs
                        {
                            StoveProgress = 0f
                        });

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case StoveState.Burned:
                    break;
            }
        }
    }

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
                        fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        state = StoveState.Frying;
                        CurrentFryingTime = 0f;

                        OnStoveProgressChanged?.Invoke(this, new OnStoveProgressChangedEventArgs
                        {
                            StoveProgress = CurrentFryingTime / fryingRecipeSO.MaxFryingTime
                        });

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    else
                    {
                        Debug.Log("Object can not be Fried!");
                    }
                }
                else
                {
                    Debug.Log("Nothing to fry!");
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

                            state = StoveState.Idle;

                            OnStoveProgressChanged?.Invoke(this, new OnStoveProgressChangedEventArgs
                            {
                                StoveProgress = 0f
                            });

                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = state
                            });
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
                    state = StoveState.Idle;

                    OnStoveProgressChanged?.Invoke(this, new OnStoveProgressChangedEventArgs
                    {
                        StoveProgress = 0f
                    });

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
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

    private bool RecipeExistsWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOList)
        {
            if(fryingRecipeSO.InputFryingKitchenObjectSO == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    public bool IsFriedStateActive()
    {
        return state == StoveState.Fried;
    }

    public bool IsCounterLocked()
    {
        return Locked;
    }
}
