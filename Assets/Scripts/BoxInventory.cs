using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BoxInventory : MonoBehaviour
{
    [SerializeField] GameObject ingredientSlot;
    [SerializeField] FridgeConfig fridgeConfig;
    [SerializeField] int maxPick;
    [SerializeField] int pickedCount;
    [SerializeField] public List<IngredientSO> chosenIngredients = new List<IngredientSO>();
    [SerializeField] private TextMeshProUGUI ingredientName;
    [SerializeField] private Image ingredientImage;


    private Transform gridBox;

    private void Awake()
    {
        pickedCount = 0;
        gridBox = GameObject.FindGameObjectWithTag("GridBox")?.transform;
    }

    public void AddIngredient(IngredientSO ingredient, Button ButtonBox)
    {
        maxPick = fridgeConfig.selectionCount;
        if (pickedCount < maxPick)
        {
            GameObject pick = Instantiate(ingredientSlot, Vector3.zero, Quaternion.identity, gridBox);

            pick.GetComponent<Image>().sprite = ingredient.icon;
            pick.GetComponentInChildren<TextMeshProUGUI>().text = ingredient.ingredientName;

            pickedCount++;
            Debug.Log("Picked: " + pickedCount);

            if (ButtonBox) ButtonBox.interactable = false; //Deshabilito el boton del ingrediente seleccionado para que no se pueda volver a seleccionar
            chosenIngredients.Add(ingredient); //Aqui guardo los ingredientes elegidos.

            
            //Cuando vuelvo a pulsar el boton del ingrediente seleccionado, se deselecciona y se habilita su boton de nuevo
            pick.GetComponent<Button>().onClick.AddListener(() =>
            {
                Destroy(pick);
                pickedCount--;
                Debug.Log("Picked: " + pickedCount);
                chosenIngredients.Remove(ingredient);
                if (ButtonBox) ButtonBox.interactable = true;

            });
        }
    }
}
