using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private Rigidbody _rigid;
    private UnityAction _onStop; 

    private void Awake()
    {
        if(!TryGetComponent(out _rigid))
        {
            _rigid = gameObject.AddComponent<Rigidbody>();
        }
    }

    public void Init(LevelController lc)
    {
        lc.Subscribe(LevelState.PrePlaying, () => {
            _rigid.useGravity = false;
            //transform.position = lc.Level.BallOffset;
        });

        lc.Subscribe(LevelState.Playing, () => {
            _rigid.useGravity = true;
        });

        _onStop += () => lc.SetState(LevelState.Fail);
    }

    private void Update()
    {
        // If the ball stops, keep calling the next callback function => trigger?
        if (IsStop())
        {
            _onStop.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.TryGetComponent<Board>(out Board board))
        //{
        //    switch (board.GetColor())
        //    {
        //        case ~~
        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.TryGetComponent<Board>(out Board board))
        //{
        //    switch (board.GetColor())
        //    {
        //        case ~~
        //    }
        //}
    }

    private bool IsStop()
    {
        return _rigid.velocity.magnitude < 0.01f;
    }
}
