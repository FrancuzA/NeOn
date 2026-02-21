using UnityEngine;

public class RedMinion : MonoBehaviour
{
    MeshRenderer mr;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
       // mr.enabled = false;
    }
    public void Show()
    {
        mr.enabled = true;
    }
    public void Hide()
    {
        mr.enabled = false;
    }
}
