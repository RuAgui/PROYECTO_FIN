using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Data/Ingredient")]
public class IngredientSO : ScriptableObject
{
    [SerializeField] public string ingredientName;
    public string Name => ingredientName;

    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;

    [SerializeField] private GameObject prefab;
    public GameObject Prefab => prefab;
}

