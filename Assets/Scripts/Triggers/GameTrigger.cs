using UnityEngine;

public class GameTrigger : MonoBehaviour
{
    private void Awake()
    {
        PlayerController player = Instantiate(ResourceManager.Instance.GetPrefab<PlayerController>(Const.Prefabs_Player));
        DontDestroyOnLoad(player);

        SceneController scene = Util.GetOrAddComponent<SceneController>(player.gameObject);

        FadeUI fadeUI = Instantiate(ResourceManager.Instance.GetPrefab<FadeUI>(Const.Prefabs_UIs_FadeUI), Camera.main.transform);
        fadeUI.Init(scene);

        scene.ChangeScene(Const.LobbyScene);
    }
}
