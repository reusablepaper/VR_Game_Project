using UnityEngine;

public class LevelTigger : MonoBehaviour
{
    private void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        LevelController lc = player.GetComponent<LevelController>();
        PenController pc = Util.GetOrAddComponent<PenController>(player.gameObject);

        ResourceManager rm = ResourceManager.Instance;

        lc.Init();

        // Map
        GameObject level = Instantiate(rm.GetPrefab(Const.Prefabs_Levels, lc.Level.Level));

        // Player
        lc.transform.position = level.transform.position + lc.Level.PlayerOffset;
        lc.transform.LookAt(new Vector3(0, player.transform.position.y, 1));

        // goalPoint

        // Pen
        pc.Pen.Init(lc);

        // Ball
        Ball ball = Instantiate(rm.GetPrefab<Ball>(Const.Prefabs_Ball), level.transform.position + lc.Level.BallOffset, Quaternion.identity);
        ball.Init(lc);

        lc.SetState(LevelState.PrePlaying);
    }


}
