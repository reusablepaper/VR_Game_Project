using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _lobbyButton;
    [SerializeField] private Text _resultText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _tryText;
    [SerializeField] private Text _boardText;

    private LevelController _lc;
    private SceneController _sc;
    private DateTime _startTime;
    private ButtonEffect _effect;


    public void Init(LevelController levelController, SceneController sceneController)
    {
        _lc = levelController;
        _sc = sceneController;
        _startTime = DateTime.Now;


        _closeButton.onClick.AddListener(() =>
        {
            _effect.PlayEffect(_closeButton);
            gameObject.SetActive(false);
        });
        _lobbyButton.onClick.AddListener(() =>
        {
            _effect.PlayEffect(_lobbyButton);
            gameObject.SetActive(false);
            _lc.SetState(LevelState.None);
            _sc.ChangeScene(Const.LobbyScene);
        });


        //이부분 나중에 fail 처리로 바꿔야함!!!
        _lc.Subscribe(LevelState.Success, () => OpenUi(_startTime, true));
        _lc.Subscribe(LevelState.None, () => gameObject.SetActive(false));
        _lc.Subscribe(LevelState.Success, () => OpenUi(_startTime, true));

    }
    public void OpenUi(DateTime startTime, bool isSuccessed)
    {
        _resultText.text = isSuccessed ? "SUCCESS" : "FAIL";

        TimeSpan duration = DateTime.Now - startTime;
        _timeText.text = $"{duration.TotalSeconds / 60}m {duration.TotalSeconds % 60}s";
        _tryText.text = $"{_lc.TryCount}";
        _boardText.text = _lc.BoardSpawner.BoardCount.ToString();
    }



}
