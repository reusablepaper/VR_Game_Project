using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public LevelSO Level { get; set; }

    private LevelState _state;
    private Dictionary<LevelState, UnityEvent> _entries = new();

    public void SetState(LevelState levelState)
    {
        _state = levelState;
        Publish();
    }

    public void Subscribe(LevelState levelState, UnityAction action)
    {
        if (!_entries.TryGetValue(levelState, out UnityEvent entry))
        {
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
}
