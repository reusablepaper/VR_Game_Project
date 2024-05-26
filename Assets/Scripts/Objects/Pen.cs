using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pen : MonoBehaviour
{
    private Material _material;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private LevelController _levelContoller;

    private void Awake()
    {
        Renderer r = GetComponent<Renderer>();
        _material = new Material(r.material);
        r.material = _material;

        XRGrabInteractable _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.selectExited.AddListener((SelectExitEventArgs args) => transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation));
        _grabInteractable.activated.AddListener((ActivateEventArgs args) => {
            _grabInteractable.deactivated.RemoveAllListeners(); // T.T
            _levelContoller.BoardSpawner.SpawnBoard(this);
            });
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.3f, 1 << LayerMask.NameToLayer("Board")))
        {
            hit.transform.parent.GetComponent<Board>().Draw(transform.position + transform.forward * Vector3.Distance(transform.position, hit.point), _material.color);
        }
    }

    public void Init(LevelController levelController, PenColor color)
    {
        _levelContoller = levelController;
        levelController.Subscribe(LevelState.Playing, () => gameObject.SetActive(false));

        _originalPosition = transform.position;
        _originalRotation = transform.rotation;

        _material.color = color switch
        {
            PenColor.Black => Color.black,
            PenColor.Blue => Color.blue,
            PenColor.LightGreen => new Color(0.3f, 1f, 0f, 1f),
            PenColor.Green => Color.green,
            PenColor.Yellow => Color.yellow,
            PenColor.Orange => new Color(1f, 1f, 0f, 1f),
            PenColor.Gray => Color.gray,
            PenColor.Purple => new Color(1f, 0f, 1f, 1f),
            _ => Color.clear
        };
    }
}
