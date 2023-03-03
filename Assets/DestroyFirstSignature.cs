using UnityEngine;

public class DestroyFirstSignature : MonoBehaviour
{
    public GameObject ObjectToDestroy;

    private void OnEnable()
    {
        Destroy(ObjectToDestroy);
    }
}
