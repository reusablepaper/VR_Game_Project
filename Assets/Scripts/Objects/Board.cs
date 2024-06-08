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

        XRGrabInteractable penGrab = pc.Pen.GetComponent<XRGrabInteractable>();
        penGrab.deactivated.AddListener((DeactivateEventArgs args) => Finish());
        penGrab.selectExited.AddListener((SelectExitEventArgs args) => Finish());

        _line.loop = false;
        _line.positionCount = 0;
        _inputBox.SetActive(true);

        _collider.isTrigger = true;
        _collider.enabled = false;
        SoundManager.Instance.PlaySFX(SFX.BoardPop);
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
        if (_isFinished) return;
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
        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("Ball"))) {
            Vector3 vel;
            Rigidbody rigid = other.GetComponent<Rigidbody>();

            switch (_color)
            {
                case Palette.Black:
                    _collider.isTrigger = false;
                    SoundManager.Instance.PlaySFX(SFX.General);
                    break;
                case Palette.Blue:
                    vel = rigid.velocity;
                    rigid.velocity = Vector3.Reflect(vel, _normal);
                    SoundManager.Instance.PlaySFX(SFX.Elasticity);
                    break;
                case Palette.LightGreen:
                    other.transform.localScale = other.transform.localScale * 0.8f;
                    SoundManager.Instance.PlaySFX(SFX.BoardPop);
                    break;
                case Palette.Green:
                    other.transform.localScale = other.transform.localScale * 1.2f;
                    SoundManager.Instance.PlaySFX(SFX.BoardPop);
                    break;
                case Palette.Yellow:
                    vel = rigid.velocity;
                    rigid.velocity = vel * 1.5f;
                    SoundManager.Instance.PlaySFX(SFX.Teleport);
                    break;
                case Palette.Orange:
                    vel = rigid.velocity;
                    rigid.velocity = vel * 0.5f;
                    SoundManager.Instance.PlaySFX(SFX.General);
                    break;
            }
        }
    }
}
