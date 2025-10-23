using UnityEngine;

public class KitchenCounter : MonoBehaviour
{
    [SerializeField] Transform anchor;
    public IngredientSO currentSO;

    public bool IsEmpty => anchor && anchor.childCount == 0;

    public bool PlaceIngredient(IngredientSO so, GameObject itemGO)
    {
        if (!IsEmpty || !so || !itemGO) return false;

        itemGO.transform.SetParent(anchor, false);
        itemGO.transform.localPosition = Vector3.zero;
        itemGO.transform.localRotation = Quaternion.identity;
        itemGO.transform.localScale = Vector3.one;
        currentSO = so;
        return true;
    }

    public bool TakeIngredient(out IngredientSO so, out GameObject itemGO)
    {
        so = null; itemGO = null;
        if (IsEmpty) return false;

        itemGO = anchor.GetChild(0).gameObject;
        anchor.GetChild(0).SetParent(null, true);


        so = currentSO;
        currentSO = null;
        return true;
    }
}
