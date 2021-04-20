using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(ClueVisionRenderer), PostProcessEvent.AfterStack, "Custom/Clue Vision")]
public sealed class ClueVision : PostProcessEffectSettings
{
    public ColorParameter tint = new ColorParameter {value = Color.white};
    public TextureParameter mask = new TextureParameter();
}

public sealed class ClueVisionRenderer : PostProcessEffectRenderer<ClueVision>
{
    private Shader _clueVisionShader;

    public override void Init()
    {
        _clueVisionShader = Shader.Find("ClueVision");
        base.Init();
    }

    [ImageEffectOpaque]
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(_clueVisionShader);
        var mat = new Material(_clueVisionShader);
        mat.SetColor("_Tint", settings.tint);
        
        if(settings.mask != null)
            mat.SetTexture("_Mask", settings.mask);
        
        context.command.BuiltinBlit(context.source, context.destination, mat, 0);
    }

}