using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{   
    public Rigidbody Rigidbody { get; private set; }
    
    private Vector3 _initPosition;

    [SerializeField] private ParticleSystem _explosion;

    private GameObject _preview;
    private Rigidbody _previewRigid;
    private WaitForSeconds _previewTerm = new WaitForSeconds(3);

    private UnityEvent _onStop;

    private void Awake()
    {
        Rigidbody = Util.GetOrAddComponent<Rigidbody>(gameObject);
    }

    public void Init(LevelController lc)
    {
        _initPosition = transform.position;

        _preview = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _previewRigid = _preview.AddComponent<Rigidbody>();

        lc.Subscribe(LevelState.PrePlaying, () => {
            Rigidbody.useGravity = false;
            Rigidbody.velocity = Vector3.zero;
            transform.position = _initPosition;

            StartCoroutine(nameof(PreviewRoutine));
        });

        lc.Subscribe(LevelState.Playing, () => {
            Rigidbody.useGravity = true;

            StopCoroutine(nameof(PreviewRoutine));
        });

        lc.Subscribe(LevelState.None, () => {
            StopCoroutine(nameof(PreviewRoutine));
        });

        _onStop.AddListener(() => lc.SetState(LevelState.Fail));
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

    private void Update()
    {
        if(Rigidbody.velocity.magnitude < 0.01f)
        {
            _explosion.Play();

            _onStop.Invoke();
            gameObject.SetActive(false);
        }
    }
}
