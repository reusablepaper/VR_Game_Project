using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Camera _textureCamera;
    [SerializeField] private Shader _shader;
    [SerializeField] private GameObject _backside;

    private RenderTexture _renderTexture;
    private Material _material;

    private void Awake()
    {
        GenerateRenderTexture();
        GenerateMaterial();

        _backside.GetComponent<Renderer>().material = _material;
    }

    private void GenerateRenderTexture()
    {
        _renderTexture = new RenderTexture(4096, 4096, 1);

        _renderTexture.filterMode = FilterMode.Point;
        _renderTexture.graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
        _textureCamera.targetTexture = _renderTexture;
    }

    private void GenerateMaterial()
    {
        _material = new Material(_shader);

        SetAlpha(0.2f);
        _material.SetTexture("_Cam_Texture", _renderTexture);
        _material.SetFloat("_Alpha_Threshold", 0.01f);

        GetComponent<Renderer>().material = _material;
    }

    public void SetAlpha(float alpha)
    {
        _material.SetColor("_Color", new Color(0, 0, 0, alpha));
    }

    public void FinishDraw()
    {
        // 00000000 00000000 00000000 00000000
        // 1111 color
        //     1111 11111111 11111111 11111111
    }
}
