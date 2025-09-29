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

    //Creamos el delegado que vamos a llamar cuando se modifique el inventario
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    [SerializeField] private int space = 6; //Espacio por defecto en la nevera
    [SerializeField] private List<IngredientSO> items = new List<IngredientSO>(); //Lista de ingredientes en la nevera

    public bool Add(IngredientSO item)
    {
        if (!item) return false; //Si el item es nulo, no se añade
        if (items.Count >= space)
        {
            Debug.Log("Not enough space in the fridge");
            return false; //No hay espacio
        }
        items.Add(item); //Añadimos el item a la lista
        onItemChangedCallback?.Invoke(); //Llamamos al delegado si no es nulo
        return true; //Item añadido correctamente
    }

    public void Remove(IngredientSO item)
    {
        if (item == null) return; //Si el item es nulo, no se elimina
        items.Remove(item); //Eliminamos el item de la lista
        onItemChangedCallback?.Invoke(); //Llamamos al delegado si no es nulo
    }
}
