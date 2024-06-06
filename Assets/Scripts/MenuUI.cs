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


        Material toggleWallMat = Resources.Load<Material>(Const.Materails_ToggleWallMat);

        _leftButton.onClick.AddListener(() =>
        {
            _pc.prevColor();

        });

        _rightButton.onClick.AddListener(() =>
        {
            _pc.nextColor();
        });

        _startButton.onClick.AddListener(() =>
        {
            _isTransparent = true;
            setMaterialTransparent(toggleWallMat);
            _lc.SetState(LevelState.Playing);

        });

        _lobbyButton.onClick.AddListener(() =>
        {
            _isTransparent = false;
            setMeterialOpaque(toggleWallMat); 
            _lc.SetState(LevelState.Fail);
            _sc.ChangeScene(Const.LobbyScene);
        });

        _toggleButton.onClick.AddListener(() =>
        {
   
            _isTransparent = !_isTransparent; 

            RectTransform buttonRectTransform = _toggleButton.GetComponent<RectTransform>();
            Vector3 currentScale = buttonRectTransform.localScale;
            currentScale.x = -currentScale.x;
            buttonRectTransform.localScale = currentScale;

            if(toggleWallMat != null)
            { 
                if (_isTransparent)
                {
                    setMeterialOpaque(toggleWallMat);
                }
                else
                {
                    setMaterialTransparent(toggleWallMat);
                }
                
            }
        });

    }
    private void setMaterialTransparent(Material meterial)
    {
        meterial.color = new Color(0.0f, 1.0f, 1.0f, 0.3f); //toggle wall color
        meterial.SetFloat("_Mode", 3); // Unity Standard Shader의 경우 3은 Transparent 모드
    }
    private void setMeterialOpaque(Material meterial)
    {
        meterial.color = new Color(238 / 255f, 235 / 255f, 175 / 255f); //일반 벽 color
        meterial.SetFloat("_Mode", 0);
    }

}


