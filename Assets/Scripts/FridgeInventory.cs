using UnityEngine;
using System.Collections.Generic;

public class FridgeInventory : MonoBehaviour
{
    #region Singleton
        public static FridgeInventory instance;
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("More than one instance of FridgeInventory found!");
                return;
            }
            instance = this;
    }
    #endregion
    private void Start()
    {
        //Inventario rellenamos con los objetos que nos da la variable FridgeConfig//
        items = fridgeConfig.possibleIngredients;
    }

    //Creamos el delegado que vamos a llamar cuando se modifique el inventario
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedInvoke;

    [SerializeField] public int space = 6; //Espacio por defecto en la nevera
    [SerializeField] public List<IngredientSO> items = new List<IngredientSO>(); //Lista de ingredientes en la nevera
    [Header("DATA")]
    [SerializeField] private FridgeConfig fridgeConfig;

    //public bool Add(IngredientSO item)
    //{
       
    //}

}
