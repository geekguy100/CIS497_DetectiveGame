/*****************************************************************************
// File Name :         Newspaper.cs
// Author :            Kyle Grenier
// Creation Date :     04/25/2021
//
// Brief Description : Holds which scenario the newspaper is associated with.
*****************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class Newspaper : MonoBehaviour
{
    [SerializeField] private string character;
    public string Character { get { return character; } }

    private void Awake()
    {
        GetComponentInChildren<Button>().onClick.AddListener(() => { GameManager.Instance.Restart(); });
    }
}
