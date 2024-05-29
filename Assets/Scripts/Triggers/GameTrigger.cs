using UnityEngine;

public class GameTrigger : MonoBehaviour
{
    private void Awake()
    {
        GameObject player = Instantiate(ResourceManager.Instance.GetPrefab(Const.Prefabs_Player));
        DontDestroyOnLoad(player);

        SceneController scene = player.AddComponent<SceneController>();
        player.AddComponent<LevelController>();

        scene.ChangeScene("LobbyScene");
    }
}
