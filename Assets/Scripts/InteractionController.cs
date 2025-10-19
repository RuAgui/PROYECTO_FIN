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
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Crate")]
    [SerializeField] bool canOpenCrate;
    public CrateInventory crateInventory;

    private void Start()
    {
        canUseOven = false;
        ovenDoor = null;
        canCut = false;
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        //CORTAR
        if (context.performed && canCut && cuttingStation != null)
        {
            cuttingStation.AddCutProgress();
            Debug.Log("Player cut ingredient");

            return;
        }

        //HORNO
        if (context.performed && canUseOven && ovenDoor != null)
        {
            Debug.Log("Player use oven");
            ovenDoor.GetComponent<OvenBehaviour>().Toogle();
            return;
        }   
        
        //CRATE
        if (canOpenCrate && crateInventory != null)
        {
            Debug.Log("Player open crate");
            crateInventory.OpenCrateUI();
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
        //bool wasPicked = FridgeInventory.instance.Add(ingredient);
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

        //ENTEER CRATE
        if (other.CompareTag("Crate"))
        {
            Debug.Log("Player can open crate");
            canOpenCrate = true;
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

        //EXIT CRATE
        if (other.CompareTag("Crate"))
        {
            canOpenCrate = false;
        }

    }
}
