using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _lobbyButton;
    [SerializeField] private Button _toggleButton;
    [SerializeField] private Button _penInfoButton;
    [SerializeField] private Button _backButton;
    private LevelController _lc;
    private SceneController _sc;
    private PenController _pc;
    private bool _isTransparent = false;
    private bool _hasStarted = false;

    private ButtonEffect _effect;
    private Material _toggleWallMat;
    
    public void Init(LevelController lc, SceneController sc, PenController pc)
    {
        _lc = lc;
        _sc = sc;
        _pc = pc;
        _effect = GetComponent<ButtonEffect>();
        SetWall(_isTransparent);

        _leftButton.onClick.AddListener(() =>
        {
            _effect.PlayEffect(_leftButton);
            _pc.prevColor();

        });

        _rightButton.onClick.AddListener(() =>
        {
            _effect.PlayEffect(_rightButton);
            _pc.nextColor();
        });

        _startButton.onClick.AddListener(() =>
        {
            _effect.PlayEffect(_startButton);

            //시작버튼 누를시 벽은 투명이 켜지게됨
            _isTransparent= true;
            SetWall(_isTransparent);

            if (_hasStarted)
            {
                
                _startButton.image.sprite = ResourceManager.Instance.GetSprite(Const.Image_Images_Start);
                _lc.SetState(LevelState.PrePlaying);
            }

            else
            {           //시작버튼 누를시 시작버튼의 이미지를 redo이미지로 변경
                _startButton.image.sprite = ResourceManager.Instance.GetSprite(Const.Image_Images_Redo);
                _lc.SetState(LevelState.Playing);
            }
            _hasStarted = !_hasStarted;
  
        });

        _lobbyButton.onClick.AddListener(() =>
        {
            _effect.PlayEffect(_lobbyButton);

            //로비로 돌아갈 경우 벽의 투명이 꺼짐
            _isTransparent = false;
            SetWall(_isTransparent);
            
            //로비로 돌아갈 경우 시작 버튼의 모양을 다시 초기화 시켜준다
            Image image = _startButton.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>(Const.Image_Images_Start);

            _lc.SetState(LevelState.None);
            _sc.ChangeScene(Const.LobbyScene);
        });

        _toggleButton.onClick.AddListener(() =>
        {
            _effect.PlayEffect(_toggleButton);

            _isTransparent = !_isTransparent;

            SetWall(_isTransparent);

         });

        _penInfoButton.onClick.AddListener(() =>
        {
            _pc.Pen.gameObject.SetActive(false);
        });

        _backButton.onClick.AddListener(() =>
        {
            _pc.Pen.gameObject.SetActive(true);
        });


        _toggleWallMat = Resources.Load<Material>(Const.Materials_ToggleWallMat);
    }

    private void SetWall(bool isTransparent)
    {
        if (isTransparent)
        {
            _toggleButton.image.sprite = ResourceManager.Instance.GetSprite(Const.Image_Images_SwitchOff);
            SetMaterialTransparent(_toggleWallMat);
        }
        else
        {
            _toggleButton.image.sprite= ResourceManager.Instance.GetSprite(Const.Image_Images_SwitchOn);
            SetMaterialOpaque(_toggleWallMat);
        }
 
       
    }

    private void SetMaterialTransparent(Material material)
    {
        if (material == null) return;

        // 투명 색상 설정
        material.color = new Color(0.0f, 1.0f, 1.0f, 0.3f);

        // 투명 모드 설정
        material.SetFloat("_Mode", 3);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    private void SetMaterialOpaque(Material material)
    {
        if (material == null) return;

        // 불투명 색상 설정
        material.color = new Color(238 / 255f, 235 / 255f, 175 / 255f);

        // 불투명 모드 설정
        material.SetFloat("_Mode", 0);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
    }


}


