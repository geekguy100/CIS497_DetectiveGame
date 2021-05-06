/*****************************************************************************
// File Name :         ToggleMagnifyingGlass.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class ToggleMagnifyingGlass : MonoBehaviour
{
    [SerializeField] private GameObject magnifyingGlass;
    private MagnifyingGlassInteractor interactor;

    [Header("Settings")]
    [SerializeField] private string buttonName;
    [SerializeField] private bool toggle;

    private void Awake()
    {
        interactor = magnifyingGlass.GetComponent<MagnifyingGlassInteractor>();
    }

    private void Update()
    {
        if (GameManager.Instance.GameOver)
            return;

        if (Input.GetButtonDown(buttonName) && !UIManager.Instance.IsPanelOpen())
        {
            Toggle();

            Tutorial.Instance.ContinueOnMagnify();
        }

        if (!toggle && Input.GetButtonUp(buttonName) && !UIManager.Instance.IsPanelOpen())
            Toggle();
    }

    private void Toggle()
    {
        interactor.UnassignInteractable();
        SFXManager.Instance.source.PlayOneShot(SFXManager.Instance.equipMag);
        magnifyingGlass.SetActive(!magnifyingGlass.activeInHierarchy);
    }
}
