using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Ingredients : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ingredientName;
    [SerializeField] private Image ingredientImage;
    [SerializeField] private IngredientSO ingredientSO;
    [SerializeField] Button buttonIngredient;

    [SerializeField] public GameObject[] states;
    [SerializeField] private int stateIndex = 0;

    private BoxInventory boxInventory;
    public IngredientSO SO => ingredientSO;

    public bool lastState => states != null && stateIndex >= states.Length - 1;
    public bool IsCuttable => ingredientSO != null && ingredientSO.canBeCut && states != null && states.Length > 1;
    public bool LastState => states != null && states.Length > 0 && stateIndex >= states.Length - 1;

    public int CutsOverride => ingredientSO != null ? Mathf.Max(0, ingredientSO.cutsPerStep) : 0;


    private void Awake()
    {
        boxInventory = FindFirstObjectByType<BoxInventory>();
        buttonIngredient = GetComponent<Button>();
        ShowState(stateIndex);
    }
    public void CreateIngredient (IngredientSO ingredient)
    {
        ingredientSO = ingredient; //Guardo la referencia al ScriptableObject del ingrediente
        ingredientName.text = ingredient.ingredientName;
        ingredientImage.sprite = ingredient.icon;

        ShowState(0);
    }

    public void OnClickIngredient()
    {
        boxInventory.AddIngredient(ingredientSO, buttonIngredient);
    }

    public void ShowState(int i)
    {
        if (states == null || states.Length == 0) return;
        stateIndex = Mathf.Clamp(i, 0, states.Length - 1);

        for (int s = 0; s < states.Length; s++)
            if (states[s] != null) states[s].SetActive(s == stateIndex);
    }

    public void ResetToFirstState()
    {
        ShowState(0);
    }

    public bool NextState()
    {
        if (!IsCuttable) return false;
        if (lastState) return false;

        ShowState(stateIndex + 1);
        return true;
    }
}
