using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Transform visual;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] float moveSpeed = 5f;

    Rigidbody rb;
    Vector2 direction;
    public bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //rb.AddForce(new Vector3(direction.x, 0f, direction.y) * 10f);
        rb.linearVelocity = new Vector3(direction.x, 0f, direction.y) * moveSpeed;
        anim.SetFloat("Run", rb.linearVelocity.magnitude);
        Debug.Log(rb.linearVelocity.magnitude);

    }

    private void Update()
    {
        LookAtMov();
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        
        Debug.Log(context.ReadValue<Vector2>());

    }

    public void LookAtMov()
    {
        if (direction != Vector2.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.y));
            visual.rotation = Quaternion.Slerp(visual.rotation, newRotation, turnSpeed * Time.deltaTime);
        }
    }
}
