using UnityEngine;

public class PhraseInDictionary : MonoBehaviour
{
    public TextAsset CheckDictionary;
    public bool ExistInDictionary(string word)
    {
        if (CaractereRemove.RemoveDiacritics(CheckDictionary.text).ToLower().Contains(word.ToLower()))
        {
            Debug.Log("existe");
            return true;
        }
        Debug.Log("nao existe");
        return false;

        //var allPhrases = CheckDictionary.text.ToLower().Split(", ");
        //foreach (var item in allPhrases)
        //{
        //    if (item.Length != phrase.Length)
        //        continue;

        //    if (item.ToLower() == phrase)
        //        return true;
        //}
        //return false;
    }
}
