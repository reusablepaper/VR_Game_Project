using UnityEngine;

public class PenController : MonoBehaviour
{
    public Pen Pen { get; private set; }


    private void Init(PlayerController player)
    {
        Pen = ResourceManager.Instance.GetPrefab<Pen>(Const.Prefabs_Pen);

        Instantiate(Pen, player.LeftHand.transform);
    }
}
