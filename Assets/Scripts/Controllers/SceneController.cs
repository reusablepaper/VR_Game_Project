using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private UnityEvent _fadeInEvent;
    private UnityEvent _fadeOutEvent;

    private Coroutine _coroutine;
    private Renderer _faderScreenRenderer;
    private Material _faderMaterial;
    private bool _isLobby;

    private WaitForSeconds _fadeTerm = new WaitForSeconds(0.05f);

    public void Init(PlayerController player)
    {
        _fadeInEvent = new();
        _fadeOutEvent = new();

        _faderScreenRenderer = Instantiate(ResourceManager.Instance.GetPrefab<Renderer>(Const.Prefabs_FaderScreen), player.MainCamera.transform);
        _faderMaterial = _faderScreenRenderer.material;
        _faderMaterial.color = Color.black;
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
        _faderScreenRenderer.gameObject.SetActive(true);

        _fadeInEvent.Invoke();

        Material fadeMaterial = _faderScreenRenderer.material;

        while (fadeMaterial.color.a < 0.99f)
        {
            fadeMaterial.color = new Color(0, 0, 0, fadeMaterial.color.a + 0.05f);

            yield return _fadeTerm;
        }

        ResourceManager.Instance.Flush();

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }

        while (fadeMaterial.color.a > 0.01f)
        {
            fadeMaterial.color = new Color(0, 0, 0, fadeMaterial.color.a - 0.05f);

            yield return _fadeTerm;
        }

        _faderScreenRenderer.gameObject.SetActive(false);

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
