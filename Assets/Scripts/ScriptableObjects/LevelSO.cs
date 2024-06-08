using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Object/Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private int _level;
    [SerializeField] private List<Palette> _usablePens;
    [SerializeField] private Vector3 _playerOffset;
    [SerializeField] private Vector3 _ballOffset;
    [SerializeField] private Vector3 _goalOffset;

    public int Level => _level;
    public List<Palette> UseablePens => _usablePens;
    public Vector3 PlayerOffset => _playerOffset;
    public Vector3 BallOffset => _ballOffset;
    public Vector3 GoalOffset => _goalOffset;
}
