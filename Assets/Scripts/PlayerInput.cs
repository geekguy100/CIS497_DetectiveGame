/*****************************************************************************
// File Name :         PlayerInput.cs
// Author :            Kyle Grenier
// Creation Date :     03/09/2021
//
// Brief Description : Handles getting player input.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerInput : MonoBehaviour
{
    //The CharacterMovement script to handle moving the character.
    private CharacterMovement characterMovement;

    //The player's input.
    private Vector3 input = Vector3.zero;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        input = new Vector3(h, 0, v);
        input = transform.TransformDirection(input);

        characterMovement.Move(input.normalized);
    }

    /// <summary>
    /// Is the player controlling the character?
    /// </summary>
    /// <returns>True if the player is controlling the character.</returns>
    public bool IsMoving()
    {
        return (input != Vector3.zero);
    }
}
