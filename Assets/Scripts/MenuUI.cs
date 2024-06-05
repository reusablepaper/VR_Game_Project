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
            _lc.SetState(LevelState.Playing);
        });
        _lobbyButton.onClick.AddListener(() =>
        { 
            _lc.SetState(LevelState.Fail);
            _sc.ChangeScene("LobbyScene");
            Debug.Log("로비 버튼 눌림");
            //pen.SetColor(lc.Level.UseablePens[_index % lc.Level.UseablePens.Count]);
        });
        _toggleButton.onClick.AddListener(() =>
        {
 
            RectTransform buttonRectTransform = _toggleButton.GetComponent<RectTransform>();

            // 버튼의 스케일을 조정하여 좌우 반전합니다.
            if (buttonRectTransform != null)
            {
                Vector3 currentScale = buttonRectTransform.localScale;
                currentScale.x = -currentScale.x;
                buttonRectTransform.localScale = currentScale;
            }
            else
            {
                Debug.LogError("Button RectTransform component is not found.");
            }
            //여기에 버튼의 이미지를 좌우반전 시키는 코드 작성

        });

    }
    
}
