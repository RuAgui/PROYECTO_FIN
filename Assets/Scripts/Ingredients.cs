using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Ingredients : MonoBehaviour
{
    
    [SerializeField] private State currentState = State.Raw;
    [SerializeField] private TextMeshProUGUI ingredientName;
    [SerializeField] private Image ingredientImage;

    private BoxInventory boxInventory;

    private void Awake()
    {
        boxInventory = FindFirstObjectByType<BoxInventory>();
    }

    public enum State
    {
        Raw,
        Cooked,
        Burned,
        Cut,
    }

    public bool IsCut => currentState == State.Cut;

    public void SetCut()
    {
        currentState = State.Cut;
    }

    public void CreateIngredient (IngredientSO ingredient)
    {
        ingredientName.text = ingredient.ingredientName;
        ingredientImage.sprite = ingredient.icon;
    }

    public void OnClickIngredient()
    {
        boxInventory.AddIngredient(ingredientName,ingredientImage);
    }

}
