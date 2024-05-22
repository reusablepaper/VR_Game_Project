using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Object/Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private int _lv;
    public int Lv => _lv;

    [SerializeField] private GameObject _mapPrefab;
    [SerializeField] private List<Pen> _usablePens;
    [SerializeField] private Vector3 _playerOffset;
    [SerializeField] private Vector3 _ballOffset;


    public GameObject MapPrefab => _mapPrefab;
    public List<Pen> UseablePens => _usablePens;
    public Vector3 PlayerOffset => _playerOffset;
    public Vector3 BallOffset => _ballOffset;
}
