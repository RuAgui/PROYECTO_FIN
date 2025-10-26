using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Data/Ingredient")]
public class IngredientSO : ScriptableObject
{
    [Header("Ingredient")]
    [SerializeField] public string ingredientName;
    [SerializeField] public Sprite icon;
    [SerializeField] public GameObject prefab;

    [Header("Kitchen")]
    [SerializeField] public bool canBeCut;
    [SerializeField] public bool canBeBoiled;
    [SerializeField] public bool canBeBaked;

    public int cutsPerStep = 0;

}

