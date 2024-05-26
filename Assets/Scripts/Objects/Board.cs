using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Board : MonoBehaviour
{
    [SerializeField] private Material _baseMaterial;
    [SerializeField] private GameObject _inputBox;

    private LineRenderer _line;

    private float _minDistance = 0.02f;
    private int _inkCount = 50;

    private bool _isFinished => _line.loop;


    private void Awake()
    {
        _line = gameObject.AddComponent<LineRenderer>();

        _line.material = new Material(_baseMaterial);
        _line.startWidth = 0.05f;
        _line.endWidth = 0.05f;
        _line.alignment = LineAlignment.TransformZ;
        _line.positionCount = 0;

        _line.material.color = Color.clear;
    }

    public void Init(Pen pen)
    {
        transform.position = pen.transform.position + pen.transform.forward * 0.3f;
        transform.LookAt(pen.transform.position);
        pen.GetComponent<XRGrabInteractable>().deactivated.AddListener((DeactivateEventArgs args) => Finish());

        _line.loop = false;
        _line.positionCount = 0;
        _inputBox.SetActive(true);
    }

    public void Draw(Vector3 pos, Color color)
    {
        if (_isFinished) return;
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
        if (_line.positionCount < 2)
        {
            gameObject.SetActive(false);
            return;
        }

        _line.loop = true;
        _inputBox.SetActive(false);

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        _line.BakeMesh(mesh, Camera.main, false); // Camera.main => Origin.Camera, Error Check... distance = 0.1f ... Error Mesh Overlap
        meshCollider.sharedMesh = mesh;
    }
}
