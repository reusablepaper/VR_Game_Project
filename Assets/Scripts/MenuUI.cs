using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _lobbyButton;
    [SerializeField] private Button _eraserButton;

    public Pen pen;
    public LevelController lc; 
    private int _index;

    private void Awake()
    {
        pen.GetComponent<Rigidbody>().useGravity = false;
        _index = 0;

        _leftButton.onClick.AddListener(() =>
        {
            _index--;
            pen.SetColor(lc.Level.UseablePens[_index % lc.Level.UseablePens.Count]);
        });
        _rightButton.onClick.AddListener(() =>
        {
            _index++;
            pen.SetColor(lc.Level.UseablePens[_index % lc.Level.UseablePens.Count]);
        });
        _startButton.onClick.AddListener(() =>
        {
            //레벨 실패 처리하고 게임 메인 로비로 돌아가기
            //lc.
        });
        _lobbyButton.onClick.AddListener(() =>
        { 
            Debug.Log("로비 버튼 눌림");
            //pen.SetColor(lc.Level.UseablePens[_index % lc.Level.UseablePens.Count]);
        });
        _eraserButton.onClick.AddListener(() =>
        {
 
            Debug.Log("지우개 버튼 눌림");
            //pen.SetColor(lc.Level.UseablePens[_index % lc.Level.UseablePens.Count]);

        });

    }
    
}
