using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{

    [SerializeField] private GameObject ovenDoor;
    [SerializeField] private bool canUseOven;

    private void Start()
    {
        canUseOven = false;
        ovenDoor = null;
    }
    public void InteractOven(InputAction.CallbackContext context)
    {
        if (context.performed && canUseOven && ovenDoor != null)
        {
            Debug.Log("Player use oven");
            ovenDoor.GetComponent<OvenBehaviour>().Toogle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Si player entra en trigger de la puerta del horno
        if (other.CompareTag("Oven"))
        {
            Debug.Log("Player can use oven");
            canUseOven = true;
            ovenDoor = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Si player sale del trigger de la puerta del horno
        if (other.CompareTag("Oven"))
        {
            Debug.Log("Player can not use oven");
            canUseOven = false;
            ovenDoor = null;
        }
    }

}
