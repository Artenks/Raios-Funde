using UnityEngine;

public class PhraseInDictionary : MonoBehaviour
{
    public TextAsset Dictionary;
    public TextAsset DictionarySpecialCharacter;
    public bool ExistInDictionary(string phrase)
    {
        var allPhrases = Dictionary.text.ToLower().Split();
        foreach (var item in allPhrases)
        {
            if (item.Length != phrase.Length)
                continue;

            if (item.ToLower() == phrase)
                return true;
        }
        return false;
    }
}
