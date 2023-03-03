using UnityEngine;

public class DrawGismozWall : MonoBehaviour
{
    public Mesh Cube;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        //Gizmos.DrawWireCube(Cube.transform.localPosition, this.transform.localScale);
        Gizmos.DrawWireMesh(Cube, this.transform.localPosition, this.transform.localRotation, this.transform.localScale);
    }
}
