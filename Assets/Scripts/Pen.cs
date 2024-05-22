using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class Pen : MonoBehaviour
{
    [SerializeField] private Material _material;

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    private void Awake()
    {
        _material = new Material(_material);
        GetComponent<Renderer>().material = _material;

        XRGrabInteractable _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.selectExited.AddListener((SelectExitEventArgs args) => transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation));

    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.3f, 1 << LayerMask.NameToLayer("Board")))
        {
            hit.transform.parent.GetComponent<Board>().Draw(transform.position, _material.color);
        }
    }

    private void Init(Color color)
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;

        _material.color = color;
    }
}
