using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Object/Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private int _level;
    [SerializeField] private List<PenColor> _usablePens;
    [SerializeField] private Vector3 _playerOffset;
    [SerializeField] private Vector3 _ballOffset;
    [SerializeField] private Vector3 _goalOffset;
    [SerializeField] private Vector3 _tableOffset;

    public int Level => _level;
    public List<PenColor> UseablePens => _usablePens;
    public Vector3 PlayerOffset => _playerOffset;
    public Vector3 BallOffset => _ballOffset;
    public Vector3 GoalOffset => _goalOffset;
    public Vector3 TableOffset => _tableOffset;
}
