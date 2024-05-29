using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private UnityEvent _fadeInEvent;
    private UnityEvent _fadeOutEvent;

    private Coroutine _coroutine;

    private FadeUI fadeUI;

    private void Awake()
    {
        _fadeInEvent = new();
        _fadeOutEvent = new();

        fadeUI = Instantiate(ResourceManager.Instance.GetPrefab<FadeUI>(Const.Prefabs_UIs_FadeUI));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnterLevel"))
        {
            ChangeScene("LevelScene");
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _fadeInEvent.Invoke();

        _coroutine = StartCoroutine(LoadSceneAsync(sceneName));

        _fadeOutEvent.Invoke();
    }


    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }
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
