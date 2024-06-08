using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LevelController : MonoBehaviour
{
    public LevelSO Level { get; private set; }

    public BoardSpawner BoardSpawner => _boardSpawner;
    private BoardSpawner _boardSpawner;

    public int TryCount;

    private ResultUI _resultUI;

    private LevelState _state;
    private Dictionary<LevelState, UnityEvent> _entries = new();

    public void Init(PlayerController player)
    {
        _entries.Clear();

        _boardSpawner = new GameObject("Board Spawner").AddComponent<BoardSpawner>();
        _boardSpawner.Init(player.PenController);
        _resultUI = Instantiate(ResourceManager.Instance.GetPrefab<ResultUI>(Const.Prefabs_UIs_ResultUI), player.MainCamera.transform);
        _resultUI.Init(this, player.SceneController);
        _resultUI.gameObject.SetActive(false);

        Subscribe(LevelState.Success, () =>
        {
            PlayerPrefs.SetInt("Level", Level.Level + 1);
            SoundManager.Instance.PlaySFX(SFX.Success);
            _resultUI.gameObject.SetActive(true);
        });
        Subscribe(LevelState.PrePlaying, () => player.RightMenuAction.action.performed += (InputAction.CallbackContext a) => { _boardSpawner.RemoveAllBoards(); });
        Subscribe(LevelState.None, () => player.RightMenuAction.action.performed -= (InputAction.CallbackContext a) => { _boardSpawner.RemoveAllBoards(); });

        Subscribe(LevelState.Fail, () =>
        {
            _resultUI.gameObject.SetActive(true);
            SoundManager.Instance.PlaySFX(SFX.Fail);
        });
    }

    public void SetState(LevelState levelState)
    {
        _state = levelState;
        Publish();
    }
    public LevelState GetState()
    {
        return _state;
    }

    public void Subscribe(LevelState levelState, UnityAction action)
    {
        if (!_entries.TryGetValue(levelState, out UnityEvent entry))
        {
            if (entry == null)
                entry = new UnityEvent();
            _entries.Add(levelState, entry);
        }
        entry.AddListener(action);
    }

    private void Publish()
    {
        if (_entries.TryGetValue(_state, out UnityEvent entry))
        {
            entry.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Door door))
        {
            int level = door.GetLevel();

            Level = ResourceManager.Instance.GetSO<LevelSO>(Const.ScriptableObjects_Levels, level);
        }
    }
}
