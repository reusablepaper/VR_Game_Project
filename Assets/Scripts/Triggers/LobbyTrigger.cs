using UnityEngine;

public class LobbyTrigger : MonoBehaviour
{
    private void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        LeftHandController leftHand = Util.GetOrAddComponent<LeftHandController>(player.LeftHand);
        Util.GetOrAddComponent<LevelController>(player.gameObject);

        Instantiate(ResourceManager.Instance.GetPrefab(Const.Prefabs_Lobby));

        player.MainCamera.transform.position = Vector3.zero;

        leftHand.SetUIOpenable(false);
        leftHand.OnEnterLobby(); 
        player.PenController.Pen.gameObject.SetActive(false);
    }
}
