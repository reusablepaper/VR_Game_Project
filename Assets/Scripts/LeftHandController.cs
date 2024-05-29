using UnityEngine;
using UnityEngine.InputSystem;

public class LeftHandController : MonoBehaviour
{
    [SerializeField] private GameObject _rightHandUIRay;
    [SerializeField] private InputActionReference _openUIReference;
    [SerializeField] private GameObject _menuUI;
 
    private bool _isUIActive => _menuUI.activeSelf;

    private void Start()
    {
        _menuUI.SetActive(false);
        _rightHandUIRay.SetActive(false);

        _openUIReference.action.performed += (InputAction.CallbackContext a) =>
        {
            _menuUI.SetActive(!_isUIActive);
            _rightHandUIRay.SetActive(_isUIActive);
        };
    }
}
