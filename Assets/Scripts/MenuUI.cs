using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _lobbyButton;
    [SerializeField] private Button _toggleButton;

    private LevelController _lc;
    private SceneController _sc;
    private PenController _pc;
    private int _index;
    private bool _isTransparent=true;

    public void Init(LevelController lc, SceneController sc, PenController pc)
    {
        _lc = lc;
        _sc = sc;
        _pc = pc;

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
            Debug.Log("시작 버튼 눌림");
            _lc.SetState(LevelState.Playing);

        });

        _lobbyButton.onClick.AddListener(() =>
        {
            Debug.Log("로비 버튼 눌림");
            _lc.SetState(LevelState.Fail);
            _sc.ChangeScene(Const.LobbyScene);

        });

        _toggleButton.onClick.AddListener(() =>
        {
            Debug.Log("토글 버튼 눌림");
            _isTransparent = !_isTransparent; 

            RectTransform buttonRectTransform = _toggleButton.GetComponent<RectTransform>();
            Vector3 currentScale = buttonRectTransform.localScale;
            currentScale.x = -currentScale.x;
            buttonRectTransform.localScale = currentScale;

            //여기에 토글 벽 투명하게 만드는 코드 추가
            Material toggleWallMat = Resources.Load<Material>(Const.Materails_ToggleWallMat);
            if(toggleWallMat != null)
            {
                if (_isTransparent)
                {
                    toggleWallMat.color = new Color(238 / 255f, 235 / 255f, 175 / 255f); //일반 벽 color
                    toggleWallMat.SetFloat("_Mode", 0); // Unity Standard Shader의 경우 3은 Transparent 모드

                }
                else
                {
                    toggleWallMat.color = new Color(0.0f, 1.0f, 1.0f, 0.3f); //toggle wall color
                    toggleWallMat.SetFloat("_Mode", 3);  
                }
                
            }
        });

    }

}


