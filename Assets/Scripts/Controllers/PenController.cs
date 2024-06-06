using UnityEngine;

public class PenController : MonoBehaviour
{
    public Pen Pen { get; private set; }


    public void Init(PlayerController player)
    {
        Pen = Instantiate(ResourceManager.Instance.GetPrefab<Pen>(Const.Prefabs_Pen), player.LeftHand.transform);
        Pen.Init(player.LevelController, player.LeftHand.GetComponent<LeftHandController>());
    }
}
