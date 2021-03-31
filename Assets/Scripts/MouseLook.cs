/*****************************************************************************
// File Name :         MouseLook.cs
// Author :            Kyle Grenier
// Creation Date :     03/11/2021
//
// Brief Description : Enables character roation with the mouse.
*****************************************************************************/
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 90f;
    [SerializeField] private float headUpperAngleLimit = 85f;
    [SerializeField] private float headLowerAngleLimit = -80f;

    private float yaw = 0f;
    private float pitch = 0f;

    private Quaternion bodyStartRotation;
    private Quaternion headStartRotation;

    //Taken from the child of this object.
    private Transform head;

    private void Awake()
    {
        head = GetComponentInChildren<Camera>().transform;
        bodyStartRotation = transform.localRotation;
        headStartRotation = head.localRotation;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        float h = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        float v = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        yaw += h;
        pitch -= v;

        //Clamp pitch so we can't look directly up or down.
        pitch = Mathf.Clamp(pitch, headLowerAngleLimit, headUpperAngleLimit);

        //Create rotations based on angle and axis.
        Quaternion bodyRotation = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion headRotation = Quaternion.AngleAxis(pitch, Vector3.right);

        transform.localRotation = bodyRotation * bodyStartRotation;
        head.localRotation = headRotation * headStartRotation;
    }

    public Transform GetHead()
    {
        return head;
    }
}
