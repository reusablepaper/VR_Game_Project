using UnityEngine;

public class LobbyTrigger : MonoBehaviour
{
    private void Awake()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        LeftHandController leftHand = Util.GetOrAddComponent<LeftHandController>(player.LeftHand);
        Util.GetOrAddComponent<LevelController>(player.gameObject);

        Instantiate(ResourceManager.Instance.GetPrefab(Const.Prefabs_Lobby));

        player.gameObject.transform.position = Vector3.zero;

        leftHand.SetUIOpenable(false);
        leftHand.MenuUI.gameObject.SetActive(false);
        player.PenController.Pen.gameObject.SetActive(false);
    }
}
