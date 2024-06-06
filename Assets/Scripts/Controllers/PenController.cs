using System.Collections.Generic;
using UnityEngine;

public class PenController : MonoBehaviour
{
    public Pen Pen { get; private set; }

    private int _penColor=0;
    private LevelController _levelController;

    public void Init(PlayerController player)
    {
        Pen = Instantiate(ResourceManager.Instance.GetPrefab<Pen>(Const.Prefabs_Pen), player.LeftHand.transform);
        Pen.Init(player.LevelController, player.LeftHand.GetComponent<LeftHandController>());

        _levelController = player.LevelController;
    }

    public void nextColor()
    {
        _penColor++; 
        Pen.SetColor(_levelController.Level.UseablePens[_penColor % _levelController.Level.UseablePens.Count]);
    }
    public void prevColor()
    {
        _penColor--;
        Pen.SetColor(_levelController.Level.UseablePens[_penColor % _levelController.Level.UseablePens.Count]);
    }

}
