using UnityEngine;
using System.Collections.Generic;

public class FridgeInventory : MonoBehaviour
{

    [SerializeField] private FridgeConfig fridgeConfig;
    [SerializeField] IngredientSO[] spawnIngredients;
    [SerializeField] GameObject ingredientPrefab;
    [SerializeField] PlayerMovement playerMovement;

    private Transform gridFridge;
    private GameObject panelFridge;
    private Ingredients ingredients;
    List<IngredientSO> ingredientsList;
    private void Awake()
    {
        if (panelFridge) panelFridge.SetActive(false);
        if (playerMovement) playerMovement.enabled = false;

        spawnIngredients = fridgeConfig.possibleIngredients.ToArray();
        ingredientsList = new List<IngredientSO>();
        ingredientsList.AddRange(spawnIngredients);
        gridFridge = GameObject.FindGameObjectWithTag("GridFridge")?.transform;
        panelFridge = GameObject.FindGameObjectWithTag("PanelFridge");

    }

    private void Start()
    {
        GenerateIngredients();
    }

    private void GenerateIngredients()
    {
        
        for (int i = 0; i < fridgeConfig.offerCount && ingredientsList.Count > 0; i++)
        {
            int index = Random.Range(0, ingredientsList.Count);

            GameObject spawnIngredient = Instantiate(ingredientPrefab, Vector3.zero, Quaternion.identity, gridFridge);

            ingredients = spawnIngredient.GetComponent<Ingredients>();
            ingredients.CreateIngredient(ingredientsList[index]);

            //Elimininar el ingrediente seleccionado de la lista para no repetirlo
            ingredientsList.RemoveAt(index);
            if (ingredientsList.Count == 0) break;
        }
    }
}
