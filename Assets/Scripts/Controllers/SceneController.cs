using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private UnityEvent _fadeInEvent;
    private UnityEvent _fadeOutEvent;

    private Coroutine _coroutine;

    public void Awake()
    {
        _fadeInEvent = new();
        _fadeOutEvent = new();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Door door))
        {
            ChangeScene("LevelScene");
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);


        _coroutine = StartCoroutine(LoadSceneAsync(sceneName));

    }


    private IEnumerator LoadSceneAsync(string sceneName)
    {
        _fadeInEvent.Invoke();

        yield return new WaitForSeconds(1f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }

        _fadeOutEvent.Invoke();
    }

    public void AddFadeInAction(UnityAction action)
    {
        _fadeInEvent.AddListener(action);
    }

    public void AddFadeOutAction(UnityAction action)
    {
        _fadeOutEvent.AddListener(action);
    }
}
