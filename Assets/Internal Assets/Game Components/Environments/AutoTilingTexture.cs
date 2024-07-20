using UnityEngine;

public class AutoTilingTexture : MonoBehaviour
{
    public bool isCube;
    
    private void Start()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        var localScale = transform.localScale;
        var scaleX = localScale.x;
        var scaleY = isCube ? localScale.y : localScale.z;
        var mainTex = Shader.PropertyToID("_MainTex");
        meshRenderer.material.SetTextureScale(mainTex, new Vector2(scaleX, scaleY));
    }
}
