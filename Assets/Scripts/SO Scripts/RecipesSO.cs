using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipesSO : ScriptableObject
{
    public string RecipeName;
    public int RecipePrice;
    public List<KitchenObjectsSO> kitchenObjectsSOList;
}
