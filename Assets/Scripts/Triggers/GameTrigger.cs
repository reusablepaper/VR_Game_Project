using UnityEngine;

public class GameTrigger : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("Level", 0);
        PlayerController player = Instantiate(ResourceManager.Instance.GetPrefab<PlayerController>(Const.Prefabs_Player));

        DontDestroyOnLoad(player);

        player.SceneController.Init(player);
        player.LeftHand.GetComponent<LeftHandController>().Init(player);
        player.PenController.Init(player);

        player.SceneController.ChangeScene(Const.LobbyScene);
    }
}
