using System.Collections.Generic;
using UnityEngine;

public class BoardSpawner : MonoBehaviour
{
    private List<Board> _boards;
    private PenController _penController;
    public int BoardCount
    {
        get
        {
            int cnt = 0;
            foreach (Board board in _boards)
            {
                
                if(board.gameObject.activeSelf)
                {
                    cnt++;
                }
                
            }
            return cnt;
        }
    }

    public void Init(PenController pc)
    {
        _penController = pc;

        _boards = new();
    }

    public void SpawnBoard()
    {
        Board board = null;

        foreach (var b in _boards)
        {
            if(!b.gameObject.activeSelf)
            {
                b.gameObject.SetActive(true);
                board = b;
                break;
            }
        }

        if(board == null)
        {
            board = Instantiate(ResourceManager.Instance.GetPrefab<Board>(Const.Prefabs_Board), transform);
            _boards.Add(board);
        }

        board.Init(_penController);
    }
}
