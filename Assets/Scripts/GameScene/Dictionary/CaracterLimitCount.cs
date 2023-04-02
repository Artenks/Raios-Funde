using TMPro;
using UnityEngine;

public class CaracterLimitCount : MonoBehaviour
{
    public TMP_InputField InputField;
    private TMP_Text _text;
    private int maxCount = 46;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        UpdateCount();
    }

    public void UpdateCount()
    {
        var phraseLength = maxCount - InputField.text.Length;
        _text.text = $"{phraseLength}";

        if (phraseLength <= 10)
            _text.color = new Color32(255, 0, 86, 255);
        else
            _text.color = new Color32(255, 255, 255, 255);
    }
}
