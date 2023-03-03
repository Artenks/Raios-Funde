using UnityEngine;

public class DestroyObjectOnEnable : MonoBehaviour
{
    public GameObject ObjectToDestroy;

    private void OnEnable()
    {
        Destroy(ObjectToDestroy);
    }
}
