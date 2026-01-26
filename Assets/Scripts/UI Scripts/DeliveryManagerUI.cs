using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform RecipeContainer;
    [SerializeField] private Transform RecipeVisualTemplate;

    private void Awake()
    {
        RecipeVisualTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        GameManager.Instance.OnGameStateChange += GameManager_OnGameStateChange;
        UpdateRecipeVisuals();
        Hide();
    }

    private void GameManager_OnGameStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameIsPlayingActive() && GameManager.Instance.IsGamePaused() == false)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateRecipeVisuals();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateRecipeVisuals();
    }

    private void UpdateRecipeVisuals()
    {   
        foreach(Transform child in RecipeContainer)
        {
            if(child == RecipeVisualTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(RecipesSO recipesSO in DeliveryManager.Instance.GetWaitingRecipesListSO())
        {
            Transform recipeVisualTransform = Instantiate(RecipeVisualTemplate, RecipeContainer);
            recipeVisualTransform.gameObject.SetActive(true);
            recipeVisualTransform.GetComponent<RecipeNameUI>().SetRecipeName(recipesSO);
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
