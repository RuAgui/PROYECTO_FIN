using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxInventory : MonoBehaviour
{
    [SerializeField] GameObject ingredientSlot;
    [SerializeField] FridgeConfig fridgeConfig;
    [SerializeField] int maxPick;
    [SerializeField] int pickedCount;

    private Transform gridBox;

    private void Awake()
    {
        pickedCount = 0;
        gridBox = GameObject.FindGameObjectWithTag("GridBox")?.transform;
    }

    public void AddIngredient(TextMeshProUGUI ingredientName, Image boxImage)
    {
        maxPick = fridgeConfig.selectionCount;
        if (pickedCount < maxPick)
        {
            GameObject pick = Instantiate(ingredientSlot, Vector3.zero, Quaternion.identity, gridBox);
            
            pick.GetComponent<Image>().sprite = boxImage.sprite;
            pick.GetComponentInChildren<TextMeshProUGUI>().text = ingredientName.text;

            pickedCount++;
            Debug.Log("Picked: " + pickedCount);

            //Cuando un ingrediente es seleccionado, deshabilitar su boton para que no pueda ser seleccionado de nuevo
            ingredientName.GetComponentInParent<Button>().interactable = false;

            //Cuando vuelvo a pulsar el boton del ingrediente seleccionado, se deselecciona y se habilita su boton de nuevo
            pick.GetComponent<Button>().onClick.AddListener(() =>
            {
                Destroy(pick);
                pickedCount--;
                Debug.Log("Picked: " + pickedCount);
                ingredientName.GetComponentInParent<Button>().interactable = true;
            });

        }
    }

}
