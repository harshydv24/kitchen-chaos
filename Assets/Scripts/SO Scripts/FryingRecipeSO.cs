using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectsSO InputFryingKitchenObjectSO;
    public KitchenObjectsSO OutputFryingKitchenObjectSO;
    public float MaxFryingTime;
    public float MaxBurningTime;
}
