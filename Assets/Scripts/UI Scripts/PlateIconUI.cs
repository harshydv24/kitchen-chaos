using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private Transform IconTemplate;
    [SerializeField] private KitchenObject_Plate kitchenObjectPlate;


    private void Awake()
    {
        IconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        kitchenObjectPlate.OnIngredientsAdded += PlateKitchenObject_OnIngredientsAdded;
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void PlateKitchenObject_OnIngredientsAdded(object sender, KitchenObject_Plate.OnIngredientsAddedEventArgs e)
    {
        // foreach(Transform child in transform)
        // {
        //     if(child == IconTemplate) continue;
        //     Destroy(child.gameObject);
        // }

        Transform iconTransform = Instantiate(IconTemplate, transform);
        iconTransform.gameObject.SetActive(true);
        iconTransform.GetComponent<IconVisual>().SetKitchenOjectIcons(e.kitchenObjectsSO);
    }
}
