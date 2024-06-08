using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private LevelName _level;
    [SerializeField] private Light _lamp;
    [SerializeField] private BoxCollider _enterBox;

    private WaitForSeconds _sec = new WaitForSeconds(0.01f);
    private Coroutine _coroutine;
    private int _playerLevel;

    private void Awake()
    {
        _playerLevel = PlayerPrefs.GetInt("Level");

        if (_playerLevel > GetLevel())
            _lamp.color = Util.GetColor(Palette.Green);
        else if (_playerLevel == GetLevel())
            _lamp.color = Util.GetColor(Palette.Orange);
        else
            _lamp.color = Color.clear;

        _enterBox.enabled = false;
    }

    public void Open()
    {
        if (_playerLevel < GetLevel()) return;
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(OpenRoutine(Quaternion.Euler(0f, _door.transform.rotation.y + 0f, 0f), Quaternion.Euler(0f, 90f, 0f)));

        SoundManager.Instance.PlaySFX(SFX.OpenDoor);

        _enterBox.enabled = true;
    }

    public void Close()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(OpenRoutine(Quaternion.Euler(0f, 90f, 0f), Quaternion.Euler(0f, 0f, 0f)));

        _enterBox.enabled = false;
    }

    private IEnumerator OpenRoutine(Quaternion origin, Quaternion goal)
    {
        _door.transform.rotation = origin;

        Quaternion rotation = origin;
        float t = 0.05f;

        while(rotation.eulerAngles.y - 90f < 0.01f) {
            rotation = Quaternion.Lerp(rotation, goal, t);
            _door.transform.localRotation = rotation;

            yield return _sec;
        }
    }

    public int GetLevel() => _level switch
    {
        LevelName.Tutorial => 0,
        LevelName.One => 1,
        LevelName.Two => 2,
        LevelName.Three => 3,
        LevelName.Four => 4,
        LevelName.Five => 5,
        LevelName.Six => 6,
        LevelName.Sandbox => 7,
        _ => 0
    };
}
