using UnityEngine;

public class SimilarLetters : MonoBehaviour
{
    public CaractereRemove CaractereRemove;

    private string _anagramOutput;
    private string _oldPhrase;
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

        var lowerMessage = CaractereRemove.RemoveDiacritics(userMessage.ToLower());

        var output = "";

        if (lowerPhrase != lowerMessage)
        {
            for (var i = 0; i <= lowerPhrase.Length - 1; i++)
            {
                for (var x = 0; x <= lowerMessage.Length - 1; x++)
                {
                    if (_anagramOutput.Contains(lowerMessage[x]))
                        continue;

                    if (_phraseNow[i] == lowerMessage[x])
                    {
                        output += phrase[i];
                        HaveMoreLetter(_phraseNow, lowerMessage[i]);
                    }
                }
            }
        }
        _anagramOutput += output;
        return _anagramOutput;
    }

    private void HaveMoreLetter(string phrase, char letter)
    {
        var output = "";
        bool clearIndex = true;

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

        Debug.Log(output);
    }
}