using UnityEngine;

public class DestroyWhenOver : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(this.gameObject);
    }
}
