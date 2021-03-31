/*****************************************************************************
// File Name :         Headbob.cs
// Author :            Kyle Grenier
// Creation Date :     03/11/2021
//
// Brief Description : Performs a headbob motion on the player's camera.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MouseLook))]
public class Headbob : MonoBehaviour
{
    private PlayerInput playerInput;
    private MouseLook mouseLook;

    private Transform head;
    private Vector3 headPosLocal;
    private float startingYPos;
    private Animator anim;

    // Keeps track of the time the player's been moving.
    // Used as input for Mathf.Sin().
    private float counter;

    [Tooltip("The frequency of the head's bobbing.")]
    [SerializeField] private float frequency = 5f;

    [Tooltip("The amplitude of the head's bobbing.")]
    [SerializeField] private float amplitude = 0.25f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        mouseLook = GetComponent<MouseLook>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        head = mouseLook.GetHead();
        headPosLocal = head.localPosition;
        startingYPos = headPosLocal.y;
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

    //private void IncrementCounter()
    //{
    //    counter += Time.deltaTime;

    //    // Just to make sure the counter doesn't count up too high,
    //    // we'll set it back to 0 if it reaches full circle, or 360.
    //    if (counter > 360f)
    //        counter = 0;
    //}
}