using UnityEngine;

public class Ball : MonoBehaviour
{    public Rigidbody Rigidbody { get; private set; }

    private LevelController _levelController;
    private Vector3 _initPosition;

    [SerializeField] private AudioClip _bounceSFX;

    private void Awake()
    {
        Rigidbody = Util.GetOrAddComponent<Rigidbody>(gameObject);
        Util.GetOrAddComponent<SphereCollider>(gameObject);
    }

    public void Init(LevelController lc)
    {
        _levelController = lc;

        _initPosition = transform.position;

        lc.Subscribe(LevelState.PrePlaying, () => {
            Rigidbody.useGravity = false;
            Rigidbody.velocity = Vector3.zero;
            transform.position = _initPosition;
        });

        lc.Subscribe(LevelState.Playing, () => {
            Rigidbody.useGravity = true;
        });
    }

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.Instance.PlaySFX(_bounceSFX);
    }
}
