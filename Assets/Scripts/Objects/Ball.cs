using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private Rigidbody _rigid;
    private Vector3 _initPosition;

    private LevelController _levelController;

    private void Awake()
    {
        if(!TryGetComponent(out _rigid))
        {
            _rigid = gameObject.AddComponent<Rigidbody>();
        }
    }

    public void Init(LevelController lc)
    {
        _levelController = lc;

        _initPosition = transform.position;

        lc.Subscribe(LevelState.PrePlaying, () => {
            _rigid.useGravity = false;
            transform.position = _initPosition;
        });

        lc.Subscribe(LevelState.Playing, () => {
            _rigid.useGravity = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Board board))
        {
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Board board))
        {

        }
    }

    private bool IsStop()
    {
        return _rigid.velocity.magnitude < 0.01f;
    }
}
