using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeNameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecipeNameText;
    [SerializeField] private TextMeshProUGUI RecipePriceText;
    [SerializeField] private Transform IconsContainer;
    [SerializeField] private Transform RecipeIconTemplate;

    private void Awake()
    {
        RecipeIconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeName(RecipesSO recipesSO)
    {
        RecipeNameText.text = recipesSO.RecipeName;
        RecipePriceText.text = recipesSO.RecipePrice.ToString();
        SetRecipeIcons(recipesSO);
    }

    private void SetRecipeIcons(RecipesSO recipesSO)
    {
        foreach(Transform child in IconsContainer)
        {
            if(child == RecipeIconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectsSO kitchenObjectsSO in recipesSO.kitchenObjectsSOList)
        {
            Transform iconTransform = Instantiate(RecipeIconTemplate, IconsContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectsSO.ObjectIcon;
        }
    }
}
