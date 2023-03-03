using UnityEngine;

public class PaperArea : MonoBehaviour
{
    public GameObject paperArea;

    public LineGenerator LineGenerator;
    public bool PaperAreaOn = false;

    public Collider[] PaperCollider;

    private void OnTriggerEnter(Collider _)
    {
        PaperAreaOn = true;
    }
    private void OnTriggerExit(Collider _)
    {
        PaperAreaOn = false;
        LineGenerator.ActiveLines = null;
    }
}
