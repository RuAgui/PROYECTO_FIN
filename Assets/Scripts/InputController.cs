using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private  PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
}
