/*****************************************************************************
// File Name :         Headbob.cs
// Author :            Kyle Grenier
// Creation Date :     03/31/2021
//
// Brief Description : Performs a headbob motion on the player's camera.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class Headbob : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator anim;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Bob the head if the player is moving.
        if (playerInput.IsMoving() && !anim.GetBool("Walking"))
        {
            //IncrementCounter();
            //headPosLocal.y = (Mathf.Sin(counter * frequency) * amplitude) + startingYPos;
            //head.localPosition = headPosLocal;
            anim.SetBool("Walking", true);
        }
        else if (!playerInput.IsMoving() && anim.GetBool("Walking"))
        {
            //headPosLocal.y = startingYPos;
            //head.localPosition = headPosLocal;
            anim.SetBool("Walking", false);
        }
    }
}