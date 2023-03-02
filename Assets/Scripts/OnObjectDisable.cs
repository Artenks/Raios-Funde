using UnityEngine;

public class OnObjectDisable : MonoBehaviour
{
    private void OnDisable()
    {
        this.gameObject.SetActive(false);
    }
}
