using TMPro;
using UnityEngine;

public class ChangeTextColor : MonoBehaviour
{
    public Color32 ColorOnSelect;
    public Color32 ColorOnDeselect;

    public TMP_Text TextToChange;

    public void ChangeOnSelect()
    {
        TextToChange.color = ColorOnSelect;
    }
    public void ChangeOnDeselect()
    {
        TextToChange.color = ColorOnDeselect;
    }
}
