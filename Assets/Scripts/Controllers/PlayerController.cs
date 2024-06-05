using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private int _level;
    private GameObject _levelController;
    private GameObject _sceneController;

    [SerializeField] private GameObject _leftHand;
    public GameObject LeftHand => _leftHand;

    [SerializeField] private GameObject _rightHand;
    public GameObject RightHand => _rightHand;

    private void Awake()
    {
        _level = PlayerPrefs.GetInt("Level");
    }


}
