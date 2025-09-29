using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CuttingStation : MonoBehaviour
{

    [SerializeField] private Image progressFill;
    [SerializeField] private GameObject cuttingProgress;
    [SerializeField] private float cuttingTap = 0.20f;
    private float cutting;

    [SerializeField] private Ingredients targetIngredient;
    [SerializeField] private Ingredients lastIngredient;

    private void Awake()
    {
        cutting = 0f;
        if (progressFill) progressFill.fillAmount = 0f;
        if (cuttingProgress) cuttingProgress.SetActive(false);
    }

    public void AddCutProgress()
    {
        if (!targetIngredient || !progressFill) return; //Si no hay ingrediente o barra, salgo.

        cutting = Mathf.Clamp01(cutting + cuttingTap);
        progressFill.fillAmount = cutting;


        if (cutting >= 1f)
        {
            targetIngredient.SetCut();
            ResetProgress();
            progressFill.fillAmount = 0f;

            if (cuttingProgress) cuttingProgress.SetActive(false);
            return;
        }
    }

    private void ResetProgress()
    {
        cutting = 0f;
        if (progressFill) progressFill.fillAmount = 0f;
    }

    public void EnterStation() 
    { 
        //Si el ingrediente es distinto al ultimo, resetear.
        if (cuttingProgress) cuttingProgress.SetActive(true);
        if (targetIngredient != lastIngredient)
        {
            lastIngredient = targetIngredient;
            ResetProgress();
        }
        //Refresco la barra de progreso, en caso de volver a entrar.
        if (progressFill) progressFill.fillAmount = cutting;

    }
    public void ExitStation() { if (cuttingProgress) cuttingProgress.SetActive(false); } //Oculto la barra al salir.

}
