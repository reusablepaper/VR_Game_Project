using UnityEngine;

public class LobbyTrigger : MonoBehaviour
{
    private void Awake()
    {
        LevelController lc = FindObjectOfType<LevelController>();

        Instantiate(ResourceManager.Instance.GetPrefab(Const.Prefabs_Lobby));
    }
}
