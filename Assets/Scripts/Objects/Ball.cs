using UnityEngine;

public class Ball : MonoBehaviour
{    public Rigidbody Rigidbody { get; private set; }

    private LevelController _levelController;
    private Vector3 _initPosition;

    private void Awake()
    {
        Rigidbody = Util.GetOrAddComponent<Rigidbody>(gameObject);
    }

    public void Init(LevelController lc)
    {
        _levelController = lc;

        _initPosition = transform.position;

        lc.Subscribe(LevelState.PrePlaying, () => {
            Rigidbody.useGravity = false;
            transform.position = _initPosition;
        });

        lc.Subscribe(LevelState.Playing, () => {
            Rigidbody.useGravity = true;
        });
    }

    private void Update()
    {
        // If the ball stops, keep calling the next callback function => trigger?
        if (IsStop())
        {
            _levelController.SetState(LevelState.Fail);
        }
    }

    private bool IsStop()
    {
        return Rigidbody.velocity.magnitude < 0.01f;
    }
}
