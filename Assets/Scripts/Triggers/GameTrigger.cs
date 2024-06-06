using UnityEngine;

public class GameTrigger : MonoBehaviour
{
    private void Awake()
    {
        PlayerController player = Instantiate(ResourceManager.Instance.GetPrefab<PlayerController>(Const.Prefabs_Player));


        DontDestroyOnLoad(player);

        player.SceneController.Init(player);
        player.PenController.Init(player);
        player.LeftHand.GetComponent<LeftHandController>().Init(player);

        player.SceneController.ChangeScene(Const.LobbyScene);
    }
}
