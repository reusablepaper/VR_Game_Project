using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pen : MonoBehaviour
{
    private Material _material;
    private LevelController _levelContoller;
    private Palette _color;

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

    public void Init(LevelController levelController)
    {
        _levelContoller = levelController;
        levelController.Subscribe(LevelState.Playing, () => gameObject.SetActive(false));

        _originalPosition = transform.position;
        _originalRotation = transform.rotation;

        SetColor(Palette.Black);
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
                    board.Draw(hit.point, _color);
                }
            }
            yield return _raycastTerm;
        }
    }
    public void SetColor(Palette color)
    {
        _color = color;

        _material.color = Util.GetColor(_color);
    }
}
