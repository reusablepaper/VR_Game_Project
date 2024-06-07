using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    private Button _curButton;
    private WaitForSeconds buttonTerm = new WaitForSeconds(0.01f);
    private Coroutine _coroutine = null;

    public void PlayEffect(Button button)
    {
        _curButton = button;

        if(_coroutine != null)
        {
            StopCoroutine( _coroutine );
            _curButton.transform.localScale = Vector3.one;
        }

        _coroutine = StartCoroutine(nameof(ButtonScaleRoutine));
    }
    private IEnumerator ButtonScaleRoutine()
    {
        _curButton.transform.localScale = _curButton.transform.localScale * 1.3f;

        while (_curButton.transform.localScale != Vector3.one * 0.6f)
        {
            _curButton.transform.localScale -= Vector3.one * 0.1f;

            yield return buttonTerm;
        }

        while (_curButton.transform.localScale != Vector3.one)
        {
            _curButton.transform.localScale += Vector3.one * 0.1f;

            yield return buttonTerm;
        }
    }
}
