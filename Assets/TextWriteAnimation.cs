using System.Collections;
using TMPro;
using UnityEngine;

public class TextWriteAnimation : MonoBehaviour
{
    [Multiline]
    public string[] TextToWrite;
    public float TextVelocity;

    public GameObject Button;

    private bool _type = true;
    private bool _typying = false;

    private TMP_Text _text;
    private int _index = 0;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (_type)
        {
            if (_index >= TextToWrite.Length)
            {
                Button.GetComponent<ChangeScreen>().enabled = true;
            }
            else if (_text.text != TextToWrite[_index] && _index <= TextToWrite.Length)
            {
                WriteAText();
            }
            else
            {
                NextText();
            }
            _type = false;
        }
    }

    public void ClickToType()
    {
        if (_typying)
            return;

        _type = true;
    }

    private void WriteAText()
    {
        _text.text = "";
        StartCoroutine(TypeText());
        _index++;
    }
    private void NextText()
    {
        WriteAText();
    }

    IEnumerator TypeText()
    {
        _typying = true;
        foreach (char letter in TextToWrite[_index].ToCharArray())
        {
            _text.text += letter;
            yield return new WaitForSeconds(TextVelocity);
        }

        if (_text.text == TextToWrite[_index - 1])
        {
            _typying = false;
        }
    }
}
