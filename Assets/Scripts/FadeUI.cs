using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    private Coroutine _coroutine;

    public void Init(SceneController sc)
    {
        sc.AddFadeInAction(FadeIn);
        sc.AddFadeOutAction(FadeOut);
    }

    public void FadeIn()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(FadeRoutine(false));
    }

    public void FadeOut()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(FadeRoutine(true));
    }

    private IEnumerator FadeRoutine(bool isFadeOut)
    {
        if (isFadeOut)
        {
            while(_image.color.a != 0)
            {
                _image.color = new Color(0f, 0f, 0f, _image.color.a - 0.01f);

                yield return null;
            }
        }
        else
        {
            while (_image.color.a < 0.99f)
            {
                _image.color = new Color(0f, 0f, 0f, _image.color.a + 0.01f);

                yield return null;
            }
        }
    }
}
