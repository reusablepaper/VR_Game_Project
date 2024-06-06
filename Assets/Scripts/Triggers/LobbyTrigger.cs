using UnityEngine;

public class LobbyTrigger : MonoBehaviour
{
    private void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        Util.GetOrAddComponent<LevelController>(player.gameObject);

        Instantiate(ResourceManager.Instance.GetPrefab(Const.Prefabs_Lobby));

        player.gameObject.transform.position = Vector3.zero;
    }
}
