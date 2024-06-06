using UnityEngine;
using UnityEngine.InputSystem;

public class LeftHandController : MonoBehaviour
{
    [SerializeField] private GameObject _rightHandUIRay;
    [SerializeField] private InputActionReference _openUIReference;

    private MenuUI _menuUI;
 
    public void Init(PlayerController player)
    {
        _menuUI = Instantiate(ResourceManager.Instance.GetPrefab<MenuUI>(Const.Prefabs_UIs_MenuUI), player.LeftHand.transform);
        _menuUI.Init(player.LevelController, player.SceneController, player.PenController);

        _menuUI.gameObject.SetActive(false);
        _rightHandUIRay.SetActive(false);

        _openUIReference.action.performed += (InputAction.CallbackContext a) =>
        {
            bool isUIActive = _menuUI.gameObject.activeSelf;

            _menuUI.gameObject.SetActive(!isUIActive);
            _rightHandUIRay.SetActive(!isUIActive);
        };
    }
}
