using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Source: http://willweissman.com/unity-outlines

public class PostProcCamera : MonoBehaviour
{
    public Shader SolidShader;
    public Shader OutlineShader;
    Material outlineMat;
    Camera tempCamera;
    public double kernelStrength;
    public int kernelSize;
    float[] kernel;

    private void Start()
    {
        outlineMat = new Material(OutlineShader);

        tempCamera = new GameObject().AddComponent<Camera>(); // initialize temporary camera to render outlined objects

        kernel = GaussianKernel.Calculate(kernelStrength, kernelSize);

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        tempCamera.CopyFrom(Camera.current);
        tempCamera.backgroundColor = Color.black;
        tempCamera.clearFlags = CameraClearFlags.Color;

        tempCamera.cullingMask = 1 << LayerMask.NameToLayer("Outline"); // only allows objects in outline layer, culls others

        RenderTexture rt = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.R8); // memory allocation for texture

        tempCamera.targetTexture = rt; // set camera to render new texture

        tempCamera.RenderWithShader(SolidShader, ""); //redraw objects with solid color

        // passing information to the shader
        outlineMat.SetFloatArray("kernel", kernel);
        outlineMat.SetInt("_kernelWidth", kernel.Length);
        outlineMat.SetTexture("_SceneTex", source);

        rt.filterMode = FilterMode.Point;

        Graphics.Blit(rt, destination, outlineMat);

        RenderTexture.ReleaseTemporary(rt);
    }
}
