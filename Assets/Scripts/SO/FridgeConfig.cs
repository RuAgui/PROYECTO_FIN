using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FridgeConfig", menuName = "Data/FridgeConfig")]
public class FridgeConfig : ScriptableObject
{
    [Header("Ingredientes posibles")]
    public List<IngredientSO> possibleIngredients = new List<IngredientSO>();

    [Header("Oferta y seleccion")]
    [Tooltip("Cuantos ingredientes se ofrecen al jugador para elegir")]
    public int offerCount = 6;

    [Tooltip("Cuantos ingredientes puede seleccionar el jugador")]
    public int selectionCount = 3;
}
