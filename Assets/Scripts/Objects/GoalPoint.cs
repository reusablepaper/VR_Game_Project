using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    private LevelController _levelController;
    public void Init(LevelController lc)
    {
        _levelController = lc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            _levelController.SetState(LevelState.Success);
        }
    }
}
