using UnityEngine;

public class Pen : MonoBehaviour
{
    [SerializeField] private Material _material;

    private void Awake()
    {
        _material = new Material(_material);
        GetComponent<Renderer>().material = _material;
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.3f, 1 << LayerMask.NameToLayer("Board")))
        {
            hit.transform.parent.GetComponent<Board>().Draw(transform.position, _material.color);
        }
    }

    public void SetColor(Color color)
    {
        _material.color = color;
    }
}
