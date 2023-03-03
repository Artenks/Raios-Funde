using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PassUpdate : MonoBehaviour
{
    public UnityEvent<string, Toggle> PassChanged;

    public void UpdateValue(string toggleName)
    {
        PassChanged?.Invoke(toggleName, this.GetComponent<Toggle>());
    }

}
