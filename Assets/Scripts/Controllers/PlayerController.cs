using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LevelController LevelController { get; private set; }
    public SceneController SceneController { get; private set; }
    public PenController PenController { get; private set; }

    [SerializeField] private GameObject _leftHand;
    public GameObject LeftHand => _leftHand;

    [SerializeField] private GameObject _rightHand;
    public GameObject RightHand => _rightHand;

    [SerializeField] private Camera _mainCamera;
    public Camera MainCamera => _mainCamera;

    private void Awake()
    {
        LevelController = Util.GetOrAddComponent<LevelController>(gameObject);
        SceneController = Util.GetOrAddComponent<SceneController>(gameObject);
        PenController = Util.GetOrAddComponent<PenController>(gameObject);
    }


}
