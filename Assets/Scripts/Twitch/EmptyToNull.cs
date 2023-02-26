public class EmptyToNull
{

    public string RemoveSpace(string text)
    {
        if (text == "")
        {
            text = null;
        }
        else
        {
            var textReplaceSpace = text.Replace(" ", "");
            var textReplaceR = textReplaceSpace.Replace("\r", "");
            var textReplaceN = textReplaceR.Replace("\n", "");

            text = textReplaceN;
        }

        return text;
    }


}
