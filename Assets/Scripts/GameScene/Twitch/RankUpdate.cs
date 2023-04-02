using System;
using System.Collections.Generic;
using UnityEngine;

public class RankUpdate : MonoBehaviour
{
    public event Action RankUsersEventHandler;

    public RankInfo RankInfo;

    [Serializable]
    public struct RankData
    {
        [Serializable]
        public struct First
        {
            public string User;
            public int Score;
        }
        public First FirstUser;
        [Serializable]
        public struct Second
        {
            public string User;
            public int Score;
        }
        public Second SecondUser;

        [Serializable]
        public struct Third
        {
            public string User;
            public int Score;
        }
        public Third ThirdUser;

    }
    public RankData Rank;

    public void UsersInRank(List<string> rankList)
    {
        foreach (var userInfo in rankList)
        {
            var user = RankInfo.TakeAUser(userInfo);
            var score = RankInfo.TakeAScoreUser(user);

            if (score > Rank.FirstUser.Score)
            {
                if (user == Rank.SecondUser.User)
                    return;
                if (user == Rank.ThirdUser.User)
                    return;

                Rank.FirstUser.User = user;
                Rank.FirstUser.Score = score;

            }
            else if (score > Rank.SecondUser.Score)
            {
                if (user == Rank.FirstUser.User)
                    return;
                if (user == Rank.ThirdUser.User)
                    return;

                Rank.SecondUser.User = user;
                Rank.SecondUser.Score = score;

            }
            else if (score > Rank.ThirdUser.Score)
            {
                if (user == Rank.FirstUser.User)
                    return;
                if (user == Rank.SecondUser.User)
                    return;

                Rank.ThirdUser.User = user;
                Rank.ThirdUser.Score = score;

            }
            RankUsersEventHandler?.Invoke();
        }
    }
}
