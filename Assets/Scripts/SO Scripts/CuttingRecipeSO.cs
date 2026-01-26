using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectsSO InputKitchenObjectSO;
    public KitchenObjectsSO OutputKitchenObjectSO;
    public int MaxCuts;
}
