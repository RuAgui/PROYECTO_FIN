using UnityEngine;
using UnityEngine.InputSystem;

public class OvenBehaviour : MonoBehaviour
{

    [SerializeField] Animator Anim;
    [SerializeField] string openDoor = "IsOpen";

    public void Start()
    {
        Anim = GetComponent<Animator>();
    }
    public void Toogle()
    {
        bool isOpen = Anim.GetBool(openDoor);
        Anim.SetBool(openDoor, !isOpen);
    }
}
