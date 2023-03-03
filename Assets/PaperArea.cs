using UnityEngine;

public class PaperArea : MonoBehaviour
{
    public GameObject paperArea;

    public LineGenerator LineGenerator;
    public bool PaperAreaOn = false;

    public Collider[] PaperCollider;
    public void Update()
    {
        PaperCollider = Physics.OverlapBox(this.transform.position, new Vector3(5, 5, 5), Quaternion.identity, LayerMask.NameToLayer("Mouse"), QueryTriggerInteraction.Collide);
    }

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
