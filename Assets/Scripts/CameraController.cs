using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayableDirector start;
    [SerializeField] PlayableDirector exit;
    [SerializeField] PlayerMovement playerMovement;

    public void OnConfirm()
    {
        start.Stop();

        exit.time = 0;
        exit.Evaluate();
        exit.Play();
        playerMovement.enabled = true;
    }

}
