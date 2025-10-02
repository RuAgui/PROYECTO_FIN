using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FridgeInventoryUI : MonoBehaviour
{
    [Header("DATA")]
    [SerializeField] private FridgeConfig fridgeConfig;

    [Header("UI")]
    [SerializeField] private Transform gridFridge; //Padre de los slots de la nevera
    [SerializeField] private Button btnConfirm;
    [SerializeField] private List<SlotInventory> slotsInventory;

    FridgeInventory[] slots;

    private void Start()
    {
        for (int i = 0; i < slotsInventory.Count; i++)
        {
            if (i < fridgeConfig.possibleIngredients.Count)
            {
                slotsInventory[i].SetSlot(fridgeConfig.possibleIngredients[i]);
            }
        }
    }

    void UdpateUI()
    {
        //for(int i=0; i< slots.Length; i++)
        //{
        //    if (i < fridgeConfig.possibleIngredients.Count)
        //    {
        //        slots[i].Add(fridgeConfig.possibleIngredients[i]);
        //    }
        //    //else
        //    //{
        //    //    slots[i].Remove();
        //    //}
        //}
    }
}
