using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject RespectedGameObject;
    }

    [SerializeField] private KitchenObject_Plate kitchenObjectPlate;
    [SerializeField] private List<KitchenObjectSO_GameObject> KitcheObjectSOGameObjectList;
 

    private void Start()
    {
        kitchenObjectPlate.OnIngredientsAdded += PlateKitchenObject_OnIngredientsAdded;

        foreach(KitchenObjectSO_GameObject Object in KitcheObjectSOGameObjectList)
        {
            Object.RespectedGameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientsAdded(object sender, KitchenObject_Plate.OnIngredientsAddedEventArgs e)
    {
        foreach(KitchenObjectSO_GameObject Object in KitcheObjectSOGameObjectList)
        {
            if(e.kitchenObjectsSO == Object.kitchenObjectsSO)
            {
                Object.RespectedGameObject.SetActive(true);
            }
        }
    }
}
