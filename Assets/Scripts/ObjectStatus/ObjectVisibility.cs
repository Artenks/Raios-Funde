using UnityEngine;

public class ObjectVisibility : MonoBehaviour
{
    public GameObject UIToActive;
    public bool ObjectVisible;

    private void OnEnable()
    {
        UIToActive.SetActive(ObjectVisible);
    }

}
