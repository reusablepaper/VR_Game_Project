using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _lobbyButton;
    [SerializeField] private Button _toggleButton;

    public Pen pen;
    private LevelController _lc;
    private SceneController _sc;
    private int _index;

    private void Awake()
    {

        _lc = FindObjectOfType<LevelController>();
        _sc = FindObjectOfType<SceneController>();

        pen.GetComponent<Rigidbody>().useGravity = false;
        _index = 0;


        _leftButton.onClick.AddListener(() =>
        {
            _index--;
                

        });

        _rightButton.onClick.AddListener(() =>
        {
            _index++;
        });

        _startButton.onClick.AddListener(() =>
        {
            Debug.Log("로비 버튼 눌림");
            //_lc.SetState(LevelState.Playing);
        });

        _lobbyButton.onClick.AddListener(() =>
        {
            Debug.Log("로비 버튼 눌림");
            //_lc.SetState(LevelState.Fail);
            //_sc.ChangeScene("LobbyScene");
       

        });

        _toggleButton.onClick.AddListener(() =>
        {
            Debug.Log("토글 버튼 눌림");
            RectTransform buttonRectTransform = _toggleButton.GetComponent<RectTransform>();
            Vector3 currentScale = buttonRectTransform.localScale;
            currentScale.x = -currentScale.x;
            buttonRectTransform.localScale = currentScale;
               
            //여기에 토글 벽 투명하게 만드는 코드 추가

        });

    }

}
