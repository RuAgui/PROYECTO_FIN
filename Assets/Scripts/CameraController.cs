using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayableDirector start;
    [SerializeField] PlayableDirector exit;
    [SerializeField] PlayerMovement playerMovement;

    public CrateInventory crateInventory;
    public BoxInventory boxInventory;

    public void OnConfirm()
    {
        start.Stop();

        crateInventory.SetIngredient(boxInventory.chosenIngredients);

        exit.time = 0;
        exit.Evaluate();
        exit.Play();
        playerMovement.enabled = true;
    }

}
