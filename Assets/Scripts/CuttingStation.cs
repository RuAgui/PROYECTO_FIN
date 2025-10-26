using UnityEngine;
using UnityEngine.UI;

public class CuttingStation : MonoBehaviour
{
    [SerializeField] private Transform anchor;
    [SerializeField] private Image progressFill;
    [SerializeField] private GameObject cuttingProgress;
    [SerializeField] private int cutsPerStep = 6;

    private int cuts = 0;
    private Ingredients currentIng;
    public bool HasItemOnStation => anchor != null && anchor.childCount > 0;

    private void Awake()
    {
        if (progressFill) progressFill.fillAmount = 0f;
        if (cuttingProgress) cuttingProgress.SetActive(false);
    }

    public void AddCutProgress()
    {

        if (currentIng == null)
        {
            Debug.Log("[Cut] No hay ingrediente en la estación.");
            return;
        }

        if (!currentIng.IsCuttable)
        {
            Debug.Log("[Cut] Este ingrediente no tiene más estados (no se corta).");
            return;
        }

        if (currentIng.lastState)
        {
            if (cuttingProgress) cuttingProgress.SetActive(false);
            Debug.Log("No se puede cortar más");
            return;
        }

        cuts = Mathf.Clamp(cuts + 1, 0, cutsPerStep);
        if (progressFill) progressFill.fillAmount = cuts / (float)cutsPerStep;

        if (cuts >= cutsPerStep)
        {
            bool advanced = currentIng.NextState();
            cuts = 0;

            if (currentIng.LastState)
            {
                if (progressFill) progressFill.fillAmount = 0f;
                if (cuttingProgress) cuttingProgress.SetActive(false); else cuts++;
            }
        }

    }

    public bool PlaceIngredient (GameObject itemGO)
    {
        if (!anchor || !itemGO) return false;
        if (anchor.childCount > 0) return false;

        itemGO.transform.SetParent(anchor, false);
        itemGO.transform.localPosition = Vector3.zero;
        itemGO.transform.localRotation = Quaternion.identity;
        itemGO.transform.localScale = Vector3.one;

        currentIng = itemGO.GetComponentInChildren<Ingredients>(true);
        cuts = 0;
        if (progressFill) progressFill.fillAmount = 0f;
        if (cuttingProgress) cuttingProgress.SetActive(currentIng != null && currentIng.IsCuttable);
        return true;
    }

    public bool TakeIngredient (out GameObject itemGO)
    {
        itemGO = null;
        if (!anchor || anchor.childCount == 0) return false;
        itemGO = anchor.GetChild(0).gameObject;
        anchor.GetChild(0).SetParent(null, true);
        currentIng = null;
        cuts = 0;
        if (progressFill) progressFill.fillAmount = 0f;
        if (cuttingProgress) cuttingProgress.SetActive(false);
        return true;

    }

    public void EnterStation() 
    { 
        if (cuttingProgress) cuttingProgress.SetActive(true);
        if (progressFill) progressFill.fillAmount = cuts / (float)cutsPerStep;
    }
    public void ExitStation()
    {
        if (cuttingProgress) cuttingProgress.SetActive(false);
    }
}
