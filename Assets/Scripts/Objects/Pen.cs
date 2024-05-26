using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pen : MonoBehaviour
{
    private Material _material;
    private LevelController _levelContoller;

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    private WaitForSeconds _raycastTerm = new WaitForSeconds(0.1f);

    private void Awake()
    {
        Renderer r = GetComponent<Renderer>();
        _material = new Material(r.material);
        r.material = _material;

        XRGrabInteractable _grab = GetComponent<XRGrabInteractable>();
        _grab.selectExited.AddListener((SelectExitEventArgs args) => transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation));
        _grab.activated.AddListener((ActivateEventArgs args) =>
        {
            _grab.deactivated.RemoveAllListeners();
            _levelContoller.BoardSpawner.SpawnBoard(this);
            StartCoroutine(nameof(RaycastForDrawing));
            _grab.deactivated.AddListener((DeactivateEventArgs args) => StopCoroutine(nameof(RaycastForDrawing)));
        });
        
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
            PenColor.LightGreen => new Color(0.7f, 1f, 0.2f, 1f),
            PenColor.Green => Color.green,
            PenColor.Yellow => Color.yellow,
            PenColor.Orange => new Color(1f, 1f, 0f, 1f),
            PenColor.Gray => Color.gray,
            PenColor.Purple => new Color(1f, 0f, 1f, 1f),
            _ => Color.clear
        };
    }

    private IEnumerator RaycastForDrawing()
    {
        Board board;

        while(true)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.3f, 1 << LayerMask.NameToLayer("Board")))
            {
                if (hit.transform.parent.TryGetComponent(out board))
                {
                    board.Draw(hit.point, _material.color);
                }
            }
            yield return _raycastTerm;
        }
    }
}
