using UnityEngine;

public class Ingredients : MonoBehaviour
{
    [SerializeField] private State currentState = State.Raw;
    public enum State
    {
        Raw,
        Cooked,
        Burned,
        Cut,
    }

    public bool IsCut => currentState == State.Cut;

    public void SetCut()
    {
        currentState = State.Cut;
    }
}
