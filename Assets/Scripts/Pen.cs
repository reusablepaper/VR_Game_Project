using UnityEngine;

public class Pen : MonoBehaviour
{
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.3f, 1 << LayerMask.NameToLayer("Board")))
        {
            hit.transform.parent.GetComponent<Board>().Draw(transform.position, Color.red);
        }
    }
}
