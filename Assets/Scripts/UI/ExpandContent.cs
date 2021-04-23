/*****************************************************************************
// File Name :         ExpandContent.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : Expands downwards depending on the number of children.
*****************************************************************************/
using UnityEngine;

public class ExpandContent : MonoBehaviour
{
    [SerializeField] private int childThreshold;
    [SerializeField] private float expansionAmount;
    private Vector2 initialSizeDelta;

    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        initialSizeDelta = rt.sizeDelta;
    }

    void OnEnable()
    {
        EventManager.OnClueFound += Expand;
        Expand(new Clue());
    }

    private void OnDisable()
    {
        EventManager.OnClueFound -= Expand;
    }

    private void Expand(Clue c)
    {
        int diff = transform.childCount - childThreshold;

        // If we're at or over the threshold, expand some amount.
        if (diff >= 0)
            rt.sizeDelta = initialSizeDelta + new Vector2(0, expansionAmount * (diff + 1));
    }
}
