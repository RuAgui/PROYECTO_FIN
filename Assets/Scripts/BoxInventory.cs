using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxInventory : MonoBehaviour
{
    [SerializeField] GameObject ingredientBox;
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
            GameObject pick = Instantiate(ingredientBox, Vector3.zero, Quaternion.identity, gridBox);
            //Ingredients ingredients = pick.GetComponent<Ingredients>();
            Image image = pick.GetComponent<Image>();
            image.sprite = boxImage.sprite;
            pick.GetComponentInChildren<TextMeshProUGUI>().text = ingredientName.text;

            pickedCount++;
            Debug.Log("Picked: " + pickedCount);
        }
    }

}
