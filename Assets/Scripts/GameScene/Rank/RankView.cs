using TMPro;
using UnityEngine;

public class RankView : MonoBehaviour
{
    public RankUpdate RankUpdate;
    public GameObject Rank;

    public TMP_Text FirstUser;
    public TMP_Text SecondUser;
    public TMP_Text ThirdUser;

    private void OnEnable()
    {
        RankUpdate_RankUsersEventHandler();
    }

    public void RankUpdate_RankUsersEventHandler()
    {
        if (RankUpdate.Rank.FirstUser.User == "")
        {
            Rank.SetActive(false);
            return;
        }
        else
            Rank.SetActive(true);

        FirstUser.text = $"{RankUpdate.Rank.FirstUser.User} {RankUpdate.Rank.FirstUser.Score}";
        SecondUser.text = $"{RankUpdate.Rank.SecondUser.User} {RankUpdate.Rank.SecondUser.Score}";
        ThirdUser.text = $"{RankUpdate.Rank.ThirdUser.User} {RankUpdate.Rank.ThirdUser.Score}";

        if (RankUpdate.Rank.FirstUser.Score > 1)
            FirstUser.text = $"{FirstUser.text} pontos";
        else
            FirstUser.text = $"{FirstUser.text} ponto";

        if (RankUpdate.Rank.SecondUser.Score > 1)
            SecondUser.text = $"{SecondUser.text} pontos";
        else
            SecondUser.text = $"{SecondUser.text} ponto";

        if (RankUpdate.Rank.ThirdUser.Score > 1)
            ThirdUser.text = $"{ThirdUser.text} pontos";
        else
            ThirdUser.text = $"{ThirdUser.text} ponto";
    }
}
