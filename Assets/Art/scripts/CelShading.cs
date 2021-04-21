using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(CelShadingRenderer), PostProcessEvent.BeforeStack, "Custom/Cell Shading")]
public sealed class CelShading : PostProcessEffectSettings
{
    [Range(0f, 1f)] public FloatParameter shadowOpacity = new FloatParameter {value = 0.8f};
    
    public TextureParameter mask = new TextureParameter();
}

public sealed class CelShadingRenderer : PostProcessEffectRenderer<CelShading>
{
    private Shader _celShader;

    public override void Init()
    {
        _celShader = Shader.Find("SS Cel Shader");
        base.Init();
    }

    [ImageEffectOpaque]
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(_celShader);
        var mat = new Material(_celShader);
        mat.SetFloat("_ShadowOpacity", settings.shadowOpacity);
        mat.SetTexture("_Mask", settings.mask);
        context.command.BuiltinBlit(context.source, context.destination, mat, 0);
    }

}