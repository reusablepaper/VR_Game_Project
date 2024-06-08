using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Text _resultText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _tryText;
    [SerializeField] private Text _boardText;

    private LevelController _lc;
    private DateTime _startTime;
    private ButtonEffect _effect;


    public void Init(LevelController levelController) {

        _lc = levelController;
        _startTime = DateTime.Now;


        _closeButton.onClick.AddListener(() =>
        {
            _effect.PlayEffect(_closeButton);
            gameObject.SetActive(false);
        });


        //�̺κ� ���߿� fail ó���� �ٲ����!!!
        _lc.Subscribe(LevelState.Success, () => OpenUi(_startTime, true));
        _lc.Subscribe(LevelState.None, () => gameObject.SetActive(false));
        _lc.Subscribe(LevelState.Fail, () => OpenUi(_startTime, false));

    }
    public void OpenUi(DateTime startTime, bool isSuccessed)
    {
        _resultText.text = isSuccessed ? "SUCCESS" : "FAIL";

        TimeSpan duration = DateTime.Now - startTime;
        _timeText.text = $"{(int)duration.TotalSeconds / 60}m {(int)duration.TotalSeconds % 60}s";
        _tryText.text = $"{_lc.TryCount}";
        _boardText.text = _lc.BoardSpawner.BoardCount.ToString();
    }



}
