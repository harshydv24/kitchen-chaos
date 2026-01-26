using System;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject_Plate : KitchenObjects
{
    public event EventHandler<OnIngredientsAddedEventArgs> OnIngredientsAdded;
    public class OnIngredientsAddedEventArgs : EventArgs
    {
        public KitchenObjectsSO kitchenObjectsSO;
    }

    [SerializeField] private List<KitchenObjectsSO> ValidKitchenObjectSO;

    private List<KitchenObjectsSO> kitchenObjectsSOsList;

    private void Awake()
    {
        kitchenObjectsSOsList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredients(KitchenObjectsSO kitchenObjectsSO)
    {
        if(!ValidKitchenObjectSO.Contains(kitchenObjectsSO)) return false;

        if (kitchenObjectsSOsList.Contains(kitchenObjectsSO))
        {
            return false;
        }
        else
        {
            kitchenObjectsSOsList.Add(kitchenObjectsSO);
            OnIngredientsAdded?.Invoke(this, new OnIngredientsAddedEventArgs
            {
                kitchenObjectsSO = kitchenObjectsSO
            });
            return true;
        }
    }

    public List<KitchenObjectsSO> GetKitchenObjectListSO()
    {
        return kitchenObjectsSOsList;
    }
}
