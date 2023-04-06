using System.Collections;
using UnityEngine;

public class DestroyWhenOver : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(TimeToDestroy());
    }

    IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

}
