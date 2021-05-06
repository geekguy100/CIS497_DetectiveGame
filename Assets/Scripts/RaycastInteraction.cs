/*****************************************************************************
// File Name :         RaycastInteraction.cs
// Author :            Kyle Grenier
// Creation Date :     04/07/2021
//
// Brief Description : Performs a raycast to obtain any interactables in front of an origin.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(IInteractor))]
public class RaycastInteraction : MonoBehaviour
{
    [Tooltip("The origin of the raycast.")]
    [SerializeField] private Transform origin;

    [Tooltip("The length of the ray extending from the origin.")]
    [SerializeField] private float rayLength;

    [Tooltip("The layer all interactables reside in.")]
    [SerializeField] private LayerMask whatIsInteractable;

    private IInteractor interactor;

    private void Awake()
    {
        interactor = GetComponent<IInteractor>();
    }

    private void Update()
    {
        CheckForInteractables();
    }

    /// <summary>
    /// Raycasts from the origin for any IInteractables. If one is found it is assigned to the IInteractor.
    /// </summary>
    private void CheckForInteractables()
    {
        bool raycastHit = Physics.Raycast(origin.position, origin.forward, out RaycastHit hit, rayLength);

        if (raycastHit)
        {
            // Make sure we hit something on our interactable layer. If not, unassign any IInteractable we have currently assigned.
            if (whatIsInteractable != (whatIsInteractable | 1 << hit.transform.gameObject.layer))
            {
                if(interactor.GetInteractable() != null)
                    interactor.UnassignInteractable();

                return;
            }

            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable == null)
            {
                Debug.Log("RAYCAST INTERACTION: " + hit.transform.name + " is on an interactable layer but has no IInteractable component!");
                return;
            }

            if (interactor.GetInteractable() != interactable)
            {
                interactor.SetInteractable(interactable);
            }
        }
        // If we didn't hit anything but still have an IInteractable assigned, unassign it.
        else if (interactor.GetInteractable() != null)
            interactor.UnassignInteractable();
    }
}
