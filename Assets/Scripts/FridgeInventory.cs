using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Animations;
using System.Runtime.CompilerServices;

public class FridgeInventory : MonoBehaviour
{

    [SerializeField] private FridgeConfig fridgeConfig;
    [SerializeField] IngredientSO[] possibleIngredients;
    [SerializeField] List<IngredientSO> ingredientsList;
    [SerializeField] GameObject ingredientPrefab;
    [SerializeField] PlayerMovement playerMovement;

    private Transform gridFridge;
    private GameObject panelFridge;
    private Ingredients ingredients;
    private void Awake()
    {
        possibleIngredients = fridgeConfig.possibleIngredients.ToArray();
        ingredientsList.AddRange(possibleIngredients);
        gridFridge = GameObject.FindGameObjectWithTag("GridFridge")?.transform;
        panelFridge = GameObject.FindGameObjectWithTag("PanelFridge");
        panelFridge.SetActive(false);
        playerMovement.enabled = false;

    }

    private void Start()
    {
        GenerateIngredients();
    }

    private void GenerateIngredients()
    {
        
        for (int i = 0; i <= fridgeConfig.offerCount; i++)
        {
            int index = Random.Range(0, ingredientsList.Count);

            GameObject spawnIngredient = Instantiate(ingredientPrefab, Vector3.zero, Quaternion.identity, gridFridge);

            ingredients = spawnIngredient.GetComponent<Ingredients>();
            ingredients.CreateIngredient(ingredientsList[index]);

            //Elimininar el ingrediente seleccionado de la lista para no repetirlo
            ingredientsList.Remove(ingredientsList[index]);
            if (ingredientsList.Count == 0) break;
        }
    }
}
