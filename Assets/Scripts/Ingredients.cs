using UnityEngine;

public class Ingredients : MonoBehaviour
{
   public enum State
   {
       Raw,
       Cooked,
       Burned,
       Cut,
   }

    public enum Type
    {
        Lettuce,
        Tomato,
        Cheese,
        Meat,
        Bread,
        Onion,
        Pickle,
        None
    }

    [SerializeField] public State currentState = State.Raw;

    public void SetCut()
    {
        currentState = State.Cut;
        Debug.Log($"{gameObject.name} is now Cut");
    }
}
