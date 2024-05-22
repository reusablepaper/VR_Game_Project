using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Object/Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private int _lv;
    public int Lv => _lv;

    //[SerializeField] public List<Pen> UsablePens;

    [SerializeField] private Vector3 _playerInitialPosition;
    public Vector3 PlayerInitialPosition => _playerInitialPosition;

    [SerializeField] private Vector3 _ballInitialPosition;
    public Vector3 BallInitialPosition => _ballInitialPosition;
}
