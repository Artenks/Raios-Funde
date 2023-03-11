using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public class SimilarLetters : MonoBehaviour
{
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
        var output = "";
        bool isSimilar = false;

        var lowerPhrase = phrase.ToLower();
        var lowerMessage = userMessage.ToLower();

        if (lowerPhrase != lowerMessage)
        {
            for (var i = 0; i <= lowerPhrase.Length - 1; i++)
            {
                isSimilar = false;
                for (var x = 0; i <= lowerMessage.Length - 1; i++)
                {
                    if (lowerPhrase[i] == lowerMessage[x])
                    {
                        output += phrase[i];
                        isSimilar = false;
                    }
                }
                if (!isSimilar)
                {
                    output += '_';
                }
            }
        }
        return output;
    }
}