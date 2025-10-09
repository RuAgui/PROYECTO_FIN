using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Data/Ingredient")]
public class IngredientSO : ScriptableObject
{
    [SerializeField] public string ingredientName;
    [SerializeField] public Sprite icon;
    [SerializeField] private GameObject prefab;
}

