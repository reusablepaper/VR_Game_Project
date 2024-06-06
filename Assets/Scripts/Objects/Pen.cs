using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pen : MonoBehaviour
{
    private Material _material;
    private LevelController _levelContoller;
    private Palette _color;
    private Rigidbody _rigid;

    private Vector3 _originalPosition = new Vector3(0f, 0.104f, 0.2f);
    private Quaternion _originalRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));

    private WaitForSeconds _raycastTerm = new WaitForSeconds(0.1f);

    private void Awake()
    {
        Renderer r = GetComponent<Renderer>();
        _rigid = GetComponent<Rigidbody>();
        _material = new Material(r.material);
        r.material = _material;

        _rigid.useGravity = false;
    }

    public void Init(LevelController levelController)
    {
        _levelContoller = levelController;
        levelController.Subscribe(LevelState.Playing, () => gameObject.SetActive(false));

        InitalizePose();

        XRGrabInteractable _grab = GetComponent<XRGrabInteractable>();
        _grab.selectExited.AddListener((SelectExitEventArgs args) => InitalizePose());
        _grab.activated.AddListener((ActivateEventArgs args) =>
        {
            _levelContoller.BoardSpawner.SpawnBoard(this);
            StartCoroutine(nameof(RaycastForDrawing));
        });
        _grab.deactivated.AddListener((DeactivateEventArgs args) => StopCoroutine(nameof(RaycastForDrawing)));
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

    public void InitalizePose()
    {
        transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation);
    }
}
