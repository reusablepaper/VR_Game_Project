using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _door;

    private WaitForSeconds _sec = new WaitForSeconds(0.01f);
    private Coroutine _coroutine;

    public void Open()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(OpenRoutine(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0f, 90f, 0f)));
    }

    public void Close()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(OpenRoutine(Quaternion.Euler(0f, 90f, 0f), Quaternion.Euler(0f, 0f, 0f)));
    }

    private IEnumerator OpenRoutine(Quaternion origin, Quaternion goal)
    {
        _door.transform.rotation = origin;

        Quaternion rotation = origin;
        float t = 0.05f;

        while(rotation.eulerAngles.y - 90f < 0.01f) {
            rotation = Quaternion.Lerp(rotation, goal, t);
            _door.transform.rotation = rotation;

            yield return _sec;
        }
    }
}
