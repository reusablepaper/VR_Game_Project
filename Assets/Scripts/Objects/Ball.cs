using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    private Vector3 _initPosition;

    [SerializeField] private ParticleSystem _explosion;

    private GameObject _preview;
    private Rigidbody _previewRigid;
    private WaitForSeconds _previewTerm = new WaitForSeconds(3);

    private LevelController _lc;

    private void Awake()
    {
        _rigidbody = Util.GetOrAddComponent<Rigidbody>(gameObject);
    }

    public void Init(LevelController lc)
    {
        _initPosition = transform.position;
        _lc = lc;

        _preview = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _preview.layer = LayerMask.NameToLayer("Ball");
        _previewRigid = _preview.AddComponent<Rigidbody>();

        lc.Subscribe(LevelState.PrePlaying, () => {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            transform.position = _initPosition;

            _previewRigid.velocity = Vector3.zero;
            _preview.SetActive(true);
            StartCoroutine(nameof(PreviewRoutine));
        });

        lc.Subscribe(LevelState.Playing, () => {
            _rigidbody.useGravity = true;

            StopCoroutine(nameof(PreviewRoutine));
            _preview.SetActive(false);
        });

        lc.Subscribe(LevelState.None, () => {
            StopCoroutine(nameof(PreviewRoutine));
        });
    }

    private IEnumerator PreviewRoutine()
    {
        while(true)
        {
            _preview.transform.position = _initPosition;
            _previewRigid.velocity = Vector3.zero;

            yield return _previewTerm;
        }
    }
}
