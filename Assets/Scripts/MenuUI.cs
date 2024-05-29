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
            //���� ���� ó���ϰ� ���� ���� �κ�� ���ư���
            //lc.
        });
        _lobbyButton.onClick.AddListener(() =>
        { 
            Debug.Log("�κ� ��ư ����");
            //pen.SetColor(lc.Level.UseablePens[_index % lc.Level.UseablePens.Count]);
        });
        _eraserButton.onClick.AddListener(() =>
        {
 
            Debug.Log("���찳 ��ư ����");
            //pen.SetColor(lc.Level.UseablePens[_index % lc.Level.UseablePens.Count]);

        });

    }
    
}
