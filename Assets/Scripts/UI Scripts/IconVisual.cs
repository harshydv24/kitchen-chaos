using UnityEngine;
using UnityEngine.UI;

public class IconVisual : MonoBehaviour
{
    [SerializeField] private Image Icon;

    public void SetKitchenOjectIcons(KitchenObjectsSO kitchenObjectsSO)
    {
        Icon.sprite = kitchenObjectsSO.ObjectIcon;
    }
}
