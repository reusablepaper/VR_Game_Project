using UnityEngine;
using UnityEngine.UIElements;

public class LevelTigger : MonoBehaviour
{
    private void Awake()
    {
        LevelController lc = FindObjectOfType<LevelController>();
        ResourceManager rm = ResourceManager.Instance;
        lc.Init();

        // Map
        GameObject level = Instantiate(rm.GetPrefab($"{Const.Prefabs_Levels}{lc.Level.Level.ToString("D3")}"));

        // Player
        lc.transform.position = level.transform.position + lc.Level.PlayerOffset;

        // goalPoint

        // Pen
        foreach (var color in lc.Level.UseablePens)
        {
            Pen pen = Instantiate(rm.GetPrefab<Pen>(Const.Prefabs_Pen), level.transform.position + lc.Level.TableOffset + Vector3.up, Quaternion.identity);

            // what is pen's initial position?
            pen.Init(lc, color);
        }

        // Ball
        Ball ball = Instantiate(rm.GetPrefab<Ball>(Const.Prefabs_Ball), level.transform.position + lc.Level.BallOffset, Quaternion.identity);
        ball.Init(lc);

        lc.SetState(LevelState.PrePlaying);
    }


}
