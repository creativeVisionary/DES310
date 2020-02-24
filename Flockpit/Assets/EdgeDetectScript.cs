using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EdgeDetectScript : MonoBehaviour
{
    public Material mat;
    // Start is called before the first frame update
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int test1 = source.width;
        int test2 = source.height;
        Graphics.Blit(source, destination,mat);
    }
}
