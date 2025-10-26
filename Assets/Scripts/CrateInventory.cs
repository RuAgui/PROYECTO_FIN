using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CrateInventory : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] Transform gridCrate;
    [SerializeField] GameObject panelCrate;
    [SerializeField] GameObject ingredientPrefab;
    [SerializeField] PlayerMovement playerMovement;

    public InteractionController interactionController;
    public List<IngredientSO> chosenIngredients = new List<IngredientSO>();

    public bool IsEmpty => chosenIngredients.Count == 0;

    public void SetIngredient(List<IngredientSO> select)
    {
        chosenIngredients = new List<IngredientSO>(select);
    }
    public void OpenCrateUI()
    {
        if (interactionController && interactionController.HasItemInHand)
        {
            Debug.Log("No puedes abrir la caja con un ingrediente en la mano");
            return;
        }

        if (IsEmpty)
        {
            panelCrate.SetActive(false);
            return;
        }

        panelCrate.SetActive(true);
        if (playerMovement) playerMovement.enabled = false;

        //Limpio la grid antes de llenarla
        foreach (Transform child in gridCrate)
        {
            Destroy(child.gameObject);
        }

        foreach (IngredientSO ingredient in chosenIngredients)
        {
            GameObject go = Instantiate(ingredientPrefab, gridCrate);
            go.GetComponent<Image>().sprite = ingredient.icon;
            go.GetComponentInChildren<TextMeshProUGUI>().text = ingredient.ingredientName;

            
            //Al pulsar slot de ingriediente, se desactiva de la lista, se equipa en la mano y se cierra el panel

            Button btn = go.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    ChooseFromCrate(ingredient);
                });
            }
        }
    }

    public void ChooseFromCrate (IngredientSO ingredient)
    {
        if (interactionController && interactionController.HasItemInHand) return; //Por si acaso
 
        chosenIngredients.Remove(ingredient);
        if (interactionController) interactionController.EquipInHand(ingredient);

        CloseCrateUI();
    }

    public void CloseCrateUI()
    {
        panelCrate.SetActive(false);
        if (playerMovement) playerMovement.enabled = true;
    }
}
