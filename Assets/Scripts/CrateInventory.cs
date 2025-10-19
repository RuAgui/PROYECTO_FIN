using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
public class CrateInventory : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] Transform gridCrate;
    [SerializeField] GameObject panelCrate;
    [SerializeField] GameObject ingredientPrefab;
    [SerializeField] PlayerMovement playerMovement;

    [Header("Mano player")]
    [SerializeField] Transform handPlayer;

    public List<IngredientSO> chosenIngredients = new List<IngredientSO>();

    public void SetIngredient(List<IngredientSO> select)
    {
        chosenIngredients = new List<IngredientSO>(select);
    }
    public void OpenCrateUI()
    {
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
        chosenIngredients.Remove(ingredient);
        CloseCrateUI();
        panelCrate.SetActive(false);
        if (playerMovement) playerMovement.enabled = true;
        EquipInHand(ingredient);
    }

    public void CloseCrateUI()
    {
        panelCrate.SetActive(false);
        if (playerMovement) playerMovement.enabled = true;
    }

    public void EquipInHand(IngredientSO ingredient)
    {
        if (!ingredient || !ingredient.prefab || !handPlayer) return;

        //instancio el ingrediente en la mano del player
       
        GameObject go = Instantiate(ingredient.prefab, handPlayer);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

    }

}
