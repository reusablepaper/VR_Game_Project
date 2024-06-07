using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public LevelSO Level { get; private set; }

    public BoardSpawner BoardSpawner => _boardSpawner;
    private BoardSpawner _boardSpawner;

    private LevelState _state;
    private Dictionary<LevelState, UnityEvent> _entries = new();

    public void Init(PenController pc)
    {
        _boardSpawner = new GameObject("Board Spawner").AddComponent<BoardSpawner>();
        _boardSpawner.Init(pc);
        _entries.Clear();

        Subscribe(LevelState.Success, () => PlayerPrefs.SetInt("Level", Level.Level + 1));
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
            if(entry == null)
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
        if(other.transform.TryGetComponent(out Door door))
        {
            int level = door.GetLevel();

            Level = ResourceManager.Instance.GetSO<LevelSO>(Const.ScriptableObjects_Levels, level);
        }
    }
}
