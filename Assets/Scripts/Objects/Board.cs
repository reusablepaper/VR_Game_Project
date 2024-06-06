using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Board : MonoBehaviour
{
    [SerializeField] private Material _baseMaterial;
    [SerializeField] private GameObject _inputBox;
    [SerializeField] private MeshCollider _collider;

    private LineRenderer _line;
    private Palette _color;

    private float _minDistance = 0.02f;
    private int _inkCount = 50;

    private bool _isFinished => _line.loop;

    private Vector3 _normal;


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

    public void Init(PenController pc)
    {
        transform.position = pc.Pen.transform.position + pc.Pen.transform.forward * 0.3f;
        transform.LookAt(pc.Pen.transform);

        pc.Pen.GetComponent<XRGrabInteractable>().deactivated.AddListener((DeactivateEventArgs args) => Finish());

        _line.loop = false;
        _line.positionCount = 0;
        _inputBox.SetActive(true);

        _collider.isTrigger = true;
        _collider.enabled = false;
    }

    public void Draw(Vector3 pos, Palette color)
    {
        if (_isFinished) return;

        if (_color != color)
        {
            SetColor(color);

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

        _normal = transform.forward;

        Mesh mesh = new Mesh();
        _line.BakeMesh(mesh, true); // Camera.main => Origin.Camera, Error Check... distance = 0.1f ... Error Mesh Overlap
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        _collider.sharedMesh = mesh;
        _collider.enabled = true;
    }

    private void SetColor(Palette color)
    {
        _color = color;
        _line.material.color = Util.GetColor(_color);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Ball ball)) {
            Vector3 vel;

            switch (_color)
            {
                case Palette.Black:
                    _collider.isTrigger = false;
                    break;
                case Palette.Blue:
                    vel = ball.Rigidbody.velocity;
                    ball.Rigidbody.velocity = Vector3.Reflect(vel, _normal);
                    break;
                case Palette.LightGreen:
                    //in
                    break;
                case Palette.Green:
                    //out
                    break;
                case Palette.Yellow:
                    vel = ball.Rigidbody.velocity;
                    ball.Rigidbody.velocity = vel * 1.5f;
                    break;
                case Palette.Orange:
                    vel = ball.Rigidbody.velocity;
                    ball.Rigidbody.velocity = vel * 0.5f;
                    break;
                case Palette.Gray:
                    SetColor(Palette.Black);
                    break;
            }
        }
    }
}
