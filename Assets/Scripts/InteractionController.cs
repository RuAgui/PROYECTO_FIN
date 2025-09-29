using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [Header("Oven")]
    [SerializeField] private GameObject ovenDoor;
    [SerializeField] private bool canUseOven;

    [Header("Cutting")]
    [SerializeField] private GameObject cutStation;
    [SerializeField] public bool canCut;
    [SerializeField] CuttingStation cuttingStation;

    [SerializeField] public IngredientSO ingredient;

    private void Start()
    {
        canUseOven = false;
        ovenDoor = null;
        canCut = false;
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && canCut && cuttingStation != null)
        {
            cuttingStation.AddCutProgress();
            Debug.Log("Player cut ingredient");

            return;
        }

        if (context.performed && canUseOven && ovenDoor != null)
        {
            Debug.Log("Player use oven");
            ovenDoor.GetComponent<OvenBehaviour>().Toogle();
            return;
        }
    }

    public void CanCut()
    {
        if (cuttingStation != null)
        {
            canCut = true;
            cuttingStation.EnterStation();
        }
    }

    public void PickUp()
    {
        bool wasPicked = FridgeInventory.instance.Add(ingredient);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Estacion corte
        if (!canCut && other.CompareTag("CutStation"))
        {
            Debug.Log("Player can cut");
            cuttingStation = other.GetComponentInParent<CuttingStation>();

            CanCut();
        }
        //Si player entra en trigger de la puerta del horno
        if (other.CompareTag("Oven"))
        {
            Debug.Log("Player can use oven");
            canUseOven = true;
            ovenDoor = other.gameObject;
        }

        if (other.CompareTag("Ingredient"))
        {
            Debug.Log("Player can pick up ingredient");
            ingredient = other.GetComponent<IngredientSO>();
            PickUp();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Estacion corte
        if (other.CompareTag("CutStation"))
        {
            Debug.Log("Player can not cut");
            canCut = false;
            if (cuttingStation) cuttingStation.ExitStation();
            cuttingStation = null;
        }
        //Si player sale del trigger de la puerta del horno
        if (other.CompareTag("Oven"))
        {
            Debug.Log("Player can not use oven");
            canUseOven = false;
            ovenDoor = null;
        }
    }
}
