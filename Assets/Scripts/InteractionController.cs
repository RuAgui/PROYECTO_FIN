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
    public bool HasItemInHand => (handPlayer && handPlayer.childCount > 0);

    [Header("Current Ingredient")]
    public IngredientSO currentSO;
    public GameObject currentGO;

    [Header("Raycast")]
    [SerializeField] RaycastInteract ray;

    public IngredientSO so;

    private void Start()
    {
        canUseOven = false;
        ovenDoor = null;
        canCut = false;
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log("Interactuo");

        // CORTAR
        if (canCut && cuttingStation != null)
        {
            if (HasItemInHand && !cuttingStation.HasItemOnStation)
            {
                if (cuttingStation.PlaceIngredient(currentGO))
                {
                    Debug.Log("[Cut] Ingrediente colocado");

                    // mano vacía
                    currentGO = null;
                    currentSO = null;
                    if (handPlayer)
                    {
                        for (int i = handPlayer.childCount - 1; i >= 0; i--)
                            Destroy(handPlayer.GetChild(i).gameObject);
                    }
                }
                else
                {
                    Debug.Log("[Cut] No se pudo colocar (¿anchor asignado? ¿ya hay algo?).");
                }
                return;
            }

            if (!HasItemInHand && cuttingStation.HasItemOnStation)
            {
                if (cuttingStation.TakeIngredient(out var itemGO))
                {
                    // Saca el SO real desde el componente Ingredients del propio objeto
                    var ing = itemGO.GetComponentInChildren<Ingredients>(true);
                    var soTaken = ing != null ? ing.SO : null;

                    EquipInHand(soTaken, itemGO); // reparenta el MISMO GO
                    Debug.Log("[Cut] Recogido de la estación de corte.");
                }
                else
                {
                    Debug.Log("[Cut] No hay nada para recoger.");
                }
                return;
            }

            if (cuttingStation.HasItemOnStation)
            {
                cuttingStation.AddCutProgress();
                Debug.Log("Cortando ingrediente...");
                return;
            }

            Debug.Log("Mesa vacía y no llevas nada.");
            return;
        }

        // HORNO
        if (canUseOven && ovenDoor != null)
        {
            Debug.Log("Player use oven");
            ovenDoor.GetComponent<OvenBehaviour>().Toogle();
            return;
        }

        // CRATE
        if (canOpenCrate && crateInventory != null)
        {
            if (HasItemInHand)
            {
                Debug.Log("No puedes abrir la caja con un ingrediente en la mano");
                return;
            }
            if (crateInventory.IsEmpty)
            {
                Debug.Log("La caja está vacía.");
                return;
            }
            Debug.Log("Player open crate");
            crateInventory.OpenCrateUI();
            return;
        }

        //ENCIMERA por raycast
        if (ray && ray.TryHit<KitchenCounter>(out var counter))
        {
            if (HasItemInHand)
            {
                if (counter.PlaceIngredient(currentSO, currentGO))
                {
                    Debug.Log("Colocado en: " + counter.name);
                    currentSO = null;
                    currentGO = null; // mano vacía

                    if (handPlayer)
                    {
                        for (int i = handPlayer.childCount - 1; i >= 0; i--)
                            Destroy(handPlayer.GetChild(i).gameObject);
                    }
                }
                else
                {
                    Debug.Log("Encimera ocupada o datos nulos.");
                }
            }
            else
            {
                if (counter.TakeIngredient(out var soFromCounter, out var itemGO))
                {
                    Debug.Log("Recogido de: " + counter.name);
                    EquipInHand(soFromCounter, itemGO); // reparenta, sin clonar
                }
                else
                {
                    Debug.Log("No hay nada en esa encimera.");
                }
            }
            return;
        }
    }

    public void OnCut(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        Debug.Log("CUT CUT CUT");
        if (!canCut || cuttingStation == null) return;

        cuttingStation.AddCutProgress();
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

        for (int i = handPlayer.childCount - 1; i >= 0; i--)
        {
            if (handPlayer.GetChild(i).gameObject != go)
                Destroy(handPlayer.GetChild(i).gameObject);
        }

        go.transform.SetParent(handPlayer, false);

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

    public void PlaceOnCounter()
    {
        if (currentSO == null || currentGO == null) 
        { 
            Debug.Log("No hay ingrediente en la mano para colocar en la encimera.");
            return;
        }

        if (!ray.TryHit<KitchenCounter>(out var counter))
        {
            Debug.Log("No se ha detectado ninguna encimera frente al jugador.");
            return;
        }

        if (!counter.IsEmpty)
        {
            Debug.Log("La encimera está ocupada.");
            return;
        }

        bool placed = counter.PlaceIngredient(currentSO, currentGO);
        if (!placed)
        {
            Debug.Log("No se ha podido colocar el ingrediente en la encimera.");
            return;
        }

        currentSO = null;
        currentGO = null;

        //Por si algun prefab queda suelto, lo destruyo.
        if (handPlayer && handPlayer.childCount > 0)
        {
            for (int i = handPlayer.childCount - 1; i >= 0; i--)
            {
                Destroy(handPlayer.GetChild(i).gameObject);
            }
        }
        Debug.Log("Ingrediente colocado en la encimera correctamente.");
    }

    public void TakeFromCounter()
    {
        if (currentGO != null || currentSO != null) return;
        if (!ray.TryHit<KitchenCounter>(out var counter))
        {
            Debug.Log("No se ha detectado ninguna encimera.");
            return;
        }

        if (counter.IsEmpty)
        {
            Debug.Log("La encimera está vacía.");
            return;
        }

        if (counter.TakeIngredient(out var so, out var itemGO))
        {
            EquipInHand(so, itemGO);
            Debug.Log("Ingrediente recogido de la encimera correctamente.");
            return;
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
            crateInventory = other.GetComponentInParent<CrateInventory>();

            if (crateInventory != null && !crateInventory.IsEmpty)
            {
                Debug.Log("Player can open crate");
                canOpenCrate = true;
            }
            else
            {
                canOpenCrate = false;
            }
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
            crateInventory = null;
        }
    }
}