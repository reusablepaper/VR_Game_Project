using UnityEngine;

public class LevelTigger : MonoBehaviour
{
    private void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        LevelController lc = player.GetComponent<LevelController>();
        PenController pc = Util.GetOrAddComponent<PenController>(player.gameObject);

        ResourceManager rm = ResourceManager.Instance;

        lc.Init(player);

        // Map
        GameObject level = Instantiate(rm.GetPrefab(Const.Prefabs_Levels, lc.Level.Level));

        // Player
        lc.transform.position = level.transform.position + lc.Level.PlayerOffset;

        // goalPoint

        // Ball
        Ball ball = Instantiate(rm.GetPrefab(Const.Prefabs_Ball), level.transform.position + lc.Level.BallOffset, Quaternion.identity).AddComponent<Ball>();
        ball.Init(lc);

        player.LeftHand.GetComponent<LeftHandController>().SetUIOpenable(true);
        lc.SetState(LevelState.PrePlaying);
    }


}
