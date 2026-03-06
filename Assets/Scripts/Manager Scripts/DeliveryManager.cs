using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DeliveryManager : NetworkBehaviour
{
    public event EventHandler<UpdateMoneyAmountEventArgs> UpdateMoneyAmount;
    public class UpdateMoneyAmountEventArgs : EventArgs
    {
        public int MoneyAmount;
    }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailed;

    public static DeliveryManager Instance{get; private set;}

    [SerializeField] private RecipesListSO RecipesList;
    private List<RecipesSO> WaitingRecipesListSO;

    private float RecipeSpawnTime;
    private float RecipeSpawnTimeMax = 3f;
    private int MaxWaitingRecipes = 3;
    private int TotalOrdersDeliverd;
    private int DecMoneyAmount = -5;


    private void Awake()
    {
        Instance = this;
        WaitingRecipesListSO = new List<RecipesSO>();
    }

    private void Update()
    {
        RecipeSpawnTime -= Time.deltaTime;
        if(RecipeSpawnTime <= 0f)
        {
            RecipeSpawnTime = RecipeSpawnTimeMax;
            if(GameManager.Instance.IsGameIsPlayingActive() && WaitingRecipesListSO.Count < MaxWaitingRecipes)
            {    
                int watiningRecipesSOIndex = UnityEngine.Random.Range(0, RecipesList.recipesListSO.Count); 
                SpawnNewWaitingRecipeClientRpc(watiningRecipesSOIndex);
            }
        }
    }

    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int watiningRecipesSOIndex)
    {
        RecipesSO CurrentRecipesSO = RecipesList.recipesListSO[watiningRecipesSOIndex];
        WaitingRecipesListSO.Add(CurrentRecipesSO);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        Debug.Log(CurrentRecipesSO.RecipeName);
    }

    public void DeliverRecipe(KitchenObject_Plate kitchenObjectPlate)
    {
        for(int i = 0; i < WaitingRecipesListSO.Count; i++)
        {
            RecipesSO waitingRecipeSO = WaitingRecipesListSO[i];

            if(waitingRecipeSO.kitchenObjectsSOList.Count == kitchenObjectPlate.GetKitchenObjectListSO().Count)
            {
                // Has the same number of ingredients.
                bool PlateContentMatchesRecipe = true;

                foreach(KitchenObjectsSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectsSOList)
                {
                    // Cycling through all elements of recipes.
                    bool IngredientsFound = false;

                    foreach(KitchenObjectsSO plateKitchenObjectSO in kitchenObjectPlate.GetKitchenObjectListSO())
                    {
                        // Cycling through all elements of plate.
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // Ingredients Matches!
                            IngredientsFound = true;
                            break;
                        }
                    }
                    if (!IngredientsFound)
                    {
                        PlateContentMatchesRecipe = false;
                    }
                }
                if (PlateContentMatchesRecipe)
                {
                    // Delivery Sucess.
                    DeliverCorrectRecipeServerRpc(i);
                    return;
                }
            }
        }
        // Delivery Failed.
        DeliverIncorrectRecipeServerRpc();
    }

    // Syncing the delivery success to all clients.
    [ServerRpc(RequireOwnership = false)]
    private void DeliverCorrectRecipeServerRpc(int WaitingRecipesSOListIndex)
    {
        DeliverCorrectRecipeClientRpc(WaitingRecipesSOListIndex);
    }

    [ClientRpc]
    private void DeliverCorrectRecipeClientRpc(int WaitingRecipesSOListIndex)
    {
        TotalOrdersDeliverd++;
        UpdateMoneyAmount?.Invoke(this, new UpdateMoneyAmountEventArgs
        {
            MoneyAmount = WaitingRecipesListSO[WaitingRecipesSOListIndex].RecipePrice
        });
        WaitingRecipesListSO.RemoveAt(WaitingRecipesSOListIndex);
        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
    }

    // Syncing the delivery failed to all clients.
    [ServerRpc(RequireOwnership = false)]
    private void DeliverIncorrectRecipeServerRpc()
    {
        DeliverIncorrectRecipeClientRpc();
    }

    [ClientRpc]
    private void DeliverIncorrectRecipeClientRpc()
    {
        UpdateMoneyAmount?.Invoke(this, new UpdateMoneyAmountEventArgs
        {
            MoneyAmount = DecMoneyAmount
        });
        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipesSO> GetWaitingRecipesListSO()
    {
        return WaitingRecipesListSO;
    }

    public int GetTotalOrderDeliverd()
    {
        return TotalOrdersDeliverd;
    }
}
