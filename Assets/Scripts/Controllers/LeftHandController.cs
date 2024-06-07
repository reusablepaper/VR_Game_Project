using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LeftHandController : MonoBehaviour
{
    [SerializeField] private GameObject _rightHandUIRay;
    [SerializeField] private InputActionReference _openUIReference;

    public MenuUI MenuUI { get; private set; }

    public UnityEvent OnClickEvent;
    public bool _isUIOpenable;

    public void Init(PlayerController player)
    {
        MenuUI = Instantiate(ResourceManager.Instance.GetPrefab<MenuUI>(Const.Prefabs_UIs_MenuUI), player.LeftHand.transform);
        MenuUI.Init(player.LevelController, player.SceneController, player.PenController);

        _isUIOpenable = false;

        OnClickEvent = new();

        MenuUI.gameObject.SetActive(false);
        _rightHandUIRay.SetActive(false);

        _openUIReference.action.performed += (InputAction.CallbackContext a) =>
        {

            if (_isUIOpenable)
            {
                bool isUIActive = MenuUI.gameObject.activeSelf;

                MenuUI.gameObject.SetActive(!isUIActive);
                _rightHandUIRay.SetActive(!isUIActive);

                OnClickEvent.Invoke();
            }
        };
    }

    public void SetUIOpenable(bool isUIOpenable)
    {
        _isUIOpenable = isUIOpenable;
    }
}
