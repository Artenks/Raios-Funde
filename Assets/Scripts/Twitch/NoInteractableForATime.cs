using TMPro;
using UnityEngine;

public class NoInteractableForATime : MonoBehaviour
{
    public TimeForUpdate TimeForUpdate;

    private bool _interactableBool;

    public void NoInteractable(string _)
    {
        this.gameObject.GetComponent<TMP_InputField>().interactable = false;
        _interactableBool = true;
    }

    private void Start()
    {
        var inputField = this.gameObject.GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(NoInteractable);
    }
    private void Update()
    {
        if (_interactableBool)
        {
            if (TimeForUpdate.StartTheCount(1.3f))
            {
                this.gameObject.GetComponent<TMP_InputField>().interactable = true;
                _interactableBool = false;
            }
        }
    }
}
