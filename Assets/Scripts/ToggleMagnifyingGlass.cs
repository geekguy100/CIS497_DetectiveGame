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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        magnifyingGlass.SetActive(!magnifyingGlass.activeInHierarchy);
    }
}
