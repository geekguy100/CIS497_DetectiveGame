/*****************************************************************************
// File Name :         FindTutorial.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class FindTutorial : MonoBehaviour
{
    private void Start()
    {
        print("RUnning");
        GetComponent<Toggle>().onValueChanged.AddListener(FindObjectOfType<Tutorial>().SkipTutorial);
    }
}