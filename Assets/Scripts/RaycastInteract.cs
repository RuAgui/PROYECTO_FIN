using UnityEngine;
using static UnityEngine.UI.Image;

public class RaycastInteract : MonoBehaviour
{
   [Header("Raycast")]
   // [SerializeField] Camera cam;
    [SerializeField] float interactDistance = 2f;
    [SerializeField] LayerMask layerMask;


    public void Update()
    {
       Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.green);
    }
    public bool TryHit<T>(out T target) where T : Component
    {
        target = null;

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out var hit, interactDistance, layerMask))
        {
            Debug.DrawLine(origin, hit.point, Color.yellow);
            Debug.DrawRay(hit.point, hit.normal * 0.2f, Color.cyan);

            target = hit.collider.GetComponentInParent<T>();
            return target != null;
        }
        else
        {
            Debug.DrawLine(origin, origin + direction * interactDistance, Color.red);
        }
        return false;
    }

}
