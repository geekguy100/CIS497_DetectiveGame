/*****************************************************************************
// File Name : ReplacementShader.cs
// Author: Nathan Cover
// Creation Date: Febuary 24, 2020
//
// Brief Description : Script attached to a camera to generate specific 
// object with a replacement shader to be overlayed onto the main game
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ReplacementShader : MonoBehaviour
{
    [SerializeField]
    private Shader replacmentShader;


    private Camera _stencilCam;
    [SerializeField]
    private string _shaderTag = "RenderType";

    private void Start()
    {
        _stencilCam = GetComponent<Camera>();
    }
    private void OnEnable()
    {
        _stencilCam = GetComponent<Camera>();
        if (replacmentShader != null)
            _stencilCam.SetReplacementShader(replacmentShader, _shaderTag);
    }

    private void OnDisable()
    {
        GetComponent<Camera>().ResetReplacementShader();
    }
}
