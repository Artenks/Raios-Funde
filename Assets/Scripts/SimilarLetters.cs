using UnityEngine;

public class SimilarLetters : MonoBehaviour
{
    public CaractereRemove CaractereRemove;
    public CreateAnagram CreateAnagram;

    private string _anagramOutput;
    private string _oldPhrase;
    private string _originalMessage;
    private string _phraseNow;
    public bool IsSimilar(string userMessage, string phrase)
    {
        if (userMessage.Length != phrase.Length)
            return false;

        var lowerPhrase = phrase.ToLower();
        var lowerMessage = userMessage.ToLower();

        if (lowerPhrase != lowerMessage)
        {
            for (var i = 0; i <= phrase.Length - 1; i++)
            {
                for (var x = 0; x <= userMessage.Length - 1; x++)
                {
                    if (lowerPhrase[i] == lowerMessage[x])
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public string FoundSimilars(string userMessage, string phrase)
    {
        var lowerPhrase = CaractereRemove.RemoveDiacritics(phrase.ToLower());

        if (lowerPhrase != _oldPhrase)
        {
            _anagramOutput = "";
            _oldPhrase = lowerPhrase;
            _phraseNow = lowerPhrase;
        }

        _originalMessage = CaractereRemove.RemoveDiacritics(userMessage.ToLower());

        var output = "";

        if (lowerPhrase != _originalMessage)
        {
            for (var i = 0; i <= lowerPhrase.Length - 1; i++)
            {
                for (var x = 0; x <= _originalMessage.Length - 1; x++)
                {
                    if (_anagramOutput.Contains(_originalMessage[x]) && _phraseNow[x] == '_')
                        continue;

                    if (_phraseNow[i] == _originalMessage[x])
                    {
                        output += phrase[i];
                        HaveMoreLetter(_phraseNow, _originalMessage[x]);
                    }
                }
            }
        }
        //_anagramOutput += output;
        _anagramOutput = CreateAnagram.TransformInAnagram(output.Replace("_", ""));
        return _anagramOutput;
    }

    private void HaveMoreLetter(string phrase, char letter)
    {
        var output = "";
        bool clearIndex = true;

        //for (var i = 0; i <= phrase.Length - 1; i++)
        //{
        //    if (letter == phrase[i] && clearIndex)
        //    {
        //        output += '_';
        //        clearIndex = false;
        //        continue;
        //    }
        //    output += phrase[i];
        //}

        for (var i = 0; i <= phrase.Length - 1; i++)
        {
            if (letter == phrase[i] && clearIndex)
            {
                output += '_';
                clearIndex = false;
                continue;
            }

            output += phrase[i];
        }
        _phraseNow = output;
    }
}