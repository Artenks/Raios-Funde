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

    }
}
