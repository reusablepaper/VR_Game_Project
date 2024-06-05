using UnityEngine;
using UnityEngine.InputSystem;

public class LeftHandController : MonoBehaviour
{
    [SerializeField] private GameObject _rightHandUIRay;
    [SerializeField] private InputActionReference _openUIReference;
    [SerializeField] private GameObject _menuUI;
 
   

    private void Start()
    {
        _menuUI.SetActive(false);
        _rightHandUIRay.SetActive(false);

        _openUIReference.action.performed += (InputAction.CallbackContext a) =>
        {
            bool isUIActive=_menuUI.activeSelf;

            _menuUI.SetActive(!isUIActive);
            _rightHandUIRay.SetActive(!isUIActive);
        };
    }
}
