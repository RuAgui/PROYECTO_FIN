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

    [Header("Hand Player")]
    [SerializeField] public Transform handPlayer;
    public bool HasItemInHand => (handPlayer && handPlayer.childCount > 0) || currentGO != null;

    [Header("Current Ingredient")]
    public IngredientSO currentSO;
    public GameObject currentGO;

    [SerializeField] RaycastInteract ray;

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
        if (context.performed && canOpenCrate && crateInventory != null)
        {
            if (HasItemInHand)
            {
                Debug.Log("No puedes abrir la caja con un ingrediente en la mano");
                return;
            }

            Debug.Log("Player open crate");
            crateInventory.OpenCrateUI();
            return;
        }

        //KITCHEN COUNTER
        if (context.performed && ray && ray.TryHit<KitchenCounter>(out var lookCounter))
        {
            if (HasItemInHand)
            {
                if (lookCounter.TryPlaceIngredient(currentSO, currentGO))
                {
                    Debug.Log("Placed on counter via raycast: " + lookCounter.name);
                    currentSO = null;
                    currentGO = null; // mano vacía
                }
                else
                {
                    Debug.Log("Counter ocupada o datos nulos.");
                }
            }
            else
            {
                if (lookCounter.TryTakeIngredient(out var so, out var itemGO))
                {
                    Debug.Log("Took from counter via raycast: " + lookCounter.name);
                    EquipInHand(so, itemGO); // reparenta, sin clonar
                }
                else
                {
                    Debug.Log("No hay nada en esa encimera.");
                }
            }
            return;
        }
    }

    public void EquipInHand(IngredientSO ingredient, GameObject existing = null)
    {
        if (handPlayer == null)
        {
            Debug.LogError("No hay 'handPlayer' asignado en el Inspector.");
            return;
        }

        GameObject go = existing;
        if (go == null) 
        { 
            go = Instantiate(ingredient.prefab, handPlayer);
        }


        if (go.transform.parent != handPlayer)
        {
            for (int i = handPlayer.childCount - 1; i >= 0; i--)
            {
                Destroy(handPlayer.GetChild(i).gameObject);
            }
        }

        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        currentSO = ingredient;
        currentGO = go;

    }

    public void CanCut()
    {
        if (cuttingStation != null)
        {
            canCut = true;
            cuttingStation.EnterStation();
        }
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
