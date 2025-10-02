using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotInventory : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] private IngredientSO ingredient;

    public void SetSlot(IngredientSO newIngredient)
    {
        image.sprite = newIngredient.Icon;
        text.text = newIngredient.ingredientName;
        ingredient = newIngredient;
    }
}
