using UnityEngine;

public class LevelTigger : MonoBehaviour
{
    private void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        LevelController lc = Util.GetOrAddComponent<LevelController>(player.gameObject);
        PenController pc = Util.GetOrAddComponent<PenController>(player.gameObject);

        ResourceManager rm = ResourceManager.Instance;

        lc.Init(player);

        // Map
        GameObject level = Instantiate(rm.GetPrefab(Const.Prefabs_Levels, lc.Level.Level));

        // Player
        player.transform.position = level.transform.position + lc.Level.PlayerOffset;

        // goalPoint
        level.GetComponentInChildren<GoalPoint>().Init(lc);

        // Ball
        Ball ball = Instantiate(rm.GetPrefab<Ball>(Const.Prefabs_Ball), level.transform.position + lc.Level.BallOffset, Quaternion.identity);
        ball.Init(lc);

        player.LeftHand.GetComponent<LeftHandController>().SetUIOpenable(true);
        lc.SetState(LevelState.PrePlaying);
    }


}
