using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Material _baseMaterial;

    private GameObject _inputBox;
    private LineRenderer _line;

    private float _minDistance = 0.015f;
    private int _inkCount = 40;

    private bool IsFinished => _line.loop;


    private void Awake()
    {
        _inputBox = new GameObject("Input Box");
        _inputBox.transform.parent = gameObject.transform;
        _inputBox.layer = gameObject.layer;
        _inputBox.AddComponent<BoxCollider>().size = new Vector3(1.5f, 1f, 0.01f);

        _line = gameObject.AddComponent<LineRenderer>();

        _line.material = new Material(_baseMaterial);
        _line.startWidth = 0.01f;
        _line.endWidth = 0.01f;
        _line.alignment = LineAlignment.TransformZ;
        _line.positionCount = 0;

        _line.material.color = new Color(0, 0, 0, 0);
    }

    public void Draw(Vector3 pos, Color color)
    {
        if (IsFinished) return;
        if (_line.material.color != color)
        {
            _line.material.color = color;

            _line.positionCount = 0;
        }

        if(_line.positionCount == 0 || (_line.GetPosition(_line.positionCount - 1) - pos).magnitude > _minDistance)
        {
            _line.SetPosition(_line.positionCount++, pos);
        }

        if (_line.positionCount == _inkCount) Finish();
    }

    public void Finish()
    {
        _line.loop = true;
        _inputBox.SetActive(false);

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        _line.BakeMesh(mesh, Camera.main, false); // Camera.main => Origin.Camera, Error Check... distance = 0.1f ... Error Mesh Overlap
        meshCollider.sharedMesh = mesh;
    }
}
