using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pen : MonoBehaviour
{
    private Material _material;
    private LevelController _levelContoller;
    private Palette _color;

    private Vector3 _originalPosition = new Vector3(0f, 0.104f, 0.2f);
    private Quaternion _originalRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));

    private WaitForSeconds _raycastTerm = new WaitForSeconds(0.1f);
    private LeftHandController _leftHandController;

    private bool _isGrabed;

    private void Awake()
    {
        Renderer r = GetComponent<Renderer>();
        _material = new Material(r.material);
        r.material = _material;

        _isGrabed = false;
    }

    public void Init(LevelController levelController, LeftHandController leftHandController)
    {
        _levelContoller = levelController;
        _leftHandController = leftHandController;

        levelController.Subscribe(LevelState.Playing, () => gameObject.SetActive(false));
        leftHandController.OnClickEvent.AddListener(() =>
        {
            if (!_isGrabed)
            {
                gameObject.SetActive(leftHandController.MenuUI.gameObject.activeSelf);
            }
        });

        InitalizePose();

        XRGrabInteractable _grab = GetComponent<XRGrabInteractable>();
        _grab.selectEntered.AddListener((SelectEnterEventArgs args) => { _isGrabed = true; });
        _grab.selectExited.AddListener((SelectExitEventArgs args) =>
        {
            _isGrabed = false;
            InitalizePose();
        });
        _grab.activated.AddListener((ActivateEventArgs args) =>
        {
            _isGrabed = true;
            _levelContoller.BoardSpawner.SpawnBoard();
            StartCoroutine(nameof(RaycastForDrawing));
        });
        _grab.deactivated.AddListener((DeactivateEventArgs args) =>
        {
            _isGrabed = false;
            StopCoroutine(nameof(RaycastForDrawing));
        });

    }

    private IEnumerator RaycastForDrawing()
    {
        Board board;

        while(true)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.1f, 1 << LayerMask.NameToLayer("Board")))
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

    public void InitalizePose()
    {
        transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation);

        gameObject.SetActive(_leftHandController.MenuUI.gameObject.activeSelf);
    }
}
