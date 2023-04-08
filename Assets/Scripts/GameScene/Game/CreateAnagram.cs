using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateAnagram : MonoBehaviour
{
    private string _phraseAnagram = "";

    public void ConstructPhraseAnagram(string phrase)
    {
        _phraseAnagram = "";
        for (int i = 0; i <= phrase.Length - 1; i++)
        {
            _phraseAnagram += "_";
        }
    }

    public string TransformInAnagram(string anagramLetters)
    {
        var output = "";
        int anagramLength = anagramLetters.Length;
        var anagramMax = anagramLength;

        for (int i = 0; i <= _phraseAnagram.Length - 1; i++)
        {
            if (_phraseAnagram[i] == '_')
            {
                for (int j = 0; j <= anagramMax - 1; j++)
                {
                    if (anagramLength > 0)
                    {
                        output += anagramLetters[j];
                        anagramLength--;
                        i++;
                    }
                }
                //if (anagramLength != 0)
                //{
                //    output += anagramLetters[i];
                //    anagramLength--;
                //    continue;
                //}
            }
            output += _phraseAnagram[i];
        }

        _phraseAnagram = SortPhrase(output);
        return _phraseAnagram.Replace("_", "");
    }

    private string SortPhrase(string phrase)
    {
        var shuffleAnagram = "";
        var NoPermitThisIndex = "";

        List<int> IndexList = new List<int>();
        for (int i = 0; i <= phrase.Length - 1; i++)
        {
            if (_phraseAnagram[i] == '_')
            {
                IndexList.Add(i);
            }
        }
        System.Random rand = new System.Random();
        List<int> ShuffleIndex = IndexList.OrderBy(_ => rand.Next()).ToList();
        IndexList.Clear();

        for (int i = 0; i <= phrase.Length - 1; i++)
        {
            if (_phraseAnagram[i] != '_')
            {
                shuffleAnagram += _phraseAnagram[i];
                continue;
            }
            for (int j = 0; j <= phrase.Length - 1; j++)
            {
                if (NoPermitThisIndex.Contains($"{ShuffleIndex[j]}"))
                    continue;

                shuffleAnagram += phrase[ShuffleIndex[j]];
                NoPermitThisIndex += ShuffleIndex[j];
                break;
            }

        }

        return shuffleAnagram;
    }
}
