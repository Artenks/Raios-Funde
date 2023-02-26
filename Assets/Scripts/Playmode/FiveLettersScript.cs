//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using TMPro;
//using UnityEngine;

//public class FiveLettersScript : MonoBehaviour
//{
//    [SerializeField] TMP_Text Console;

//    [SerializeField] TMP_Text phraseCanvasText;
//    [SerializeField] TMP_Text modeCanvasText;
//    [SerializeField] TMP_Text chanceCanvasText;
//    [SerializeField] TMP_Text TipsCanvasText;
//    [SerializeField] GameObject UserKeyCanvasText;

//    string phraseGen;
//    string phraseGame;

//    [SerializeField] TimerCount timerCount;

//    int chanceLetter;
//    string phraseCensured = "_____";

//    [SerializeField] TMP_Dropdown MaxTips;
//    [SerializeField] TMP_Dropdown MaxChances;
//    bool EndTips = false;
//    bool InfinitChances = false;
//    public bool MessageOn = false;

//    bool StartGame;
//    int TipsLimit;

//    public bool TimerEnd = false;
//    [SerializeField] TMP_Dropdown MaxTimer;

//    public bool endGame = false;
//    bool phraseBool = false;
//    string firstLetter;
//    string phraseBackup;

//    [SerializeField] TMP_Text ComboText;
//    public int ComboStreakNumber;
//    public int RecordStreak;
//    [SerializeField] TMP_Text RecordText;

//    [SerializeField] TMP_Text RankFirstText;
//    [SerializeField] TMP_Text RankSecondText;
//    [SerializeField] TMP_Text RankThirdText;
//    string PlayerSniper;
//    bool RankChange;
//    List<string> RankList = new List<string>();
//    List<int> RankValue = new List<int>();
//    List<string> RankPlayers = new List<string>();

//    string firstPlace = null;
//    string secondPlace = null;
//    string thirdPlace = null;
//    [SerializeField] GameObject RankObject;
//    bool playerOk;
//    bool gameR = false;

//    [SerializeField] GameObject FinishMenu;
//    [SerializeField] TMP_Text TitleFinish;
//    [SerializeField] TMP_Text PhraseFinish;
//    [SerializeField] TMP_Text TimerFinish;
//    [SerializeField] TMP_Text StreakFinish;
//    [SerializeField] TMP_Text RecordFinish;
//    [SerializeField] GameObject RankBoxFinish;
//    [SerializeField] TMP_Text FirstPlaceFinish;
//    [SerializeField] TMP_Text SecondPlaceFinish;
//    [SerializeField] TMP_Text ThirdPlaceFinish;

//    [SerializeField] TextAsset Dictionary;
//    string[] dictionaryText;

//    void Start()
//    {
//        StartGame = false;
//        //twitchC = GameObject.Find("PlayMode").GetComponent<OldTwitchConnect>();

//        dictionaryText = Dictionary.text.Split();

//    }

//    IEnumerator PhraseNew()
//    {
//        while (phraseBackup == phraseGame || phraseGame == null)
//        {
//            //phraseGenerator.PhraseRun(out phraseGen);
//            Console.text = Console.text.Insert(0, $"Palavra: {phraseGen}\r\n");
//            phraseGame = phraseGen.ToLower();

//            phraseBool = true;

//            yield return null;
//        }
//        phraseBackup = phraseGame;
//    }
//    void Update()
//    {

//        if (phraseGame == phraseCanvasText.text)
//        {
//            phraseCanvasText.color = new Color32(255, 209, 0, 255);
//        }

//        if (phraseCanvasText.text.IndexOf("_") != -1)
//        {
//            int totalCaracter = phraseCensured.ToString().Split(new char[] { '_' }).Length - 1;
//            modeCanvasText.text = $"{totalCaracter} letras restando";
//        }
//        if (ComboStreakNumber == 0)
//        {
//            ComboText.gameObject.SetActive(false);
//        }
//        else
//        {
//            ComboText.gameObject.SetActive(true);
//        }

//        RecordGame();
//        RankGame();
//        FinishArea();

//        if (firstPlace == null && secondPlace == null && thirdPlace == null)
//        {
//            RankObject.SetActive(false);
//        }
//        else
//        {
//            RankObject.SetActive(true);
//            if (firstPlace == null)
//            {
//                RankFirstText.gameObject.SetActive(false);
//            }
//            else
//            {
//                RankFirstText.gameObject.SetActive(true);
//            }
//            if (secondPlace == null)
//            {
//                RankSecondText.gameObject.SetActive(false);
//            }
//            else
//            {
//                RankSecondText.gameObject.SetActive(true);
//            }
//            if (thirdPlace == null)
//            {
//                RankThirdText.gameObject.SetActive(false);
//            }
//            else
//            {
//                RankThirdText.gameObject.SetActive(true);
//            }
//        }

//        if (StartGame == false)
//        {
//            MessageOn = false;
//            TimerEnd = false;
//            timerCount.endTimer = false;
//            gameR = false;

//            chanceLetter = MaxChances.value;
//            TipsLimit = MaxTips.value;

//            phraseCanvasText.text = "_____";
//            phraseCensured = "_____";
//            UserKeyCanvasText.SetActive(false);

//            phraseCanvasText.color = new Color32(255, 255, 255, 255);
//            modeCanvasText.text = "Encontre a palavra";

//            EndTips = false;
//            phraseBool = false;

//            ComboStreakNumber = PlayerPrefs.GetInt("ComboStreak");
//            RecordStreak = PlayerPrefs.GetInt("RecordStreak");

//            ComboText.text = $"{ComboStreakNumber} Streak";
//            if (ComboStreakNumber == 0)
//            {
//                ComboText.gameObject.SetActive(false);
//            }
//            else
//            {
//                ComboText.gameObject.SetActive(true);
//            }

//            if (TipsLimit != 0)
//            {
//                TipsCanvasText.text = $"Dicas: {TipsLimit}";
//                TipsCanvasText.gameObject.SetActive(true);
//            }
//            if (TipsLimit == 0)
//            {
//                TipsCanvasText.gameObject.SetActive(false);
//            }

//            if (chanceLetter != 0 && chanceLetter != 1)
//            {
//                chanceCanvasText.text = $"{chanceLetter} chances restantes";
//                chanceCanvasText.gameObject.SetActive(true);
//                InfinitChances = false;
//            }
//            else if (chanceLetter == 1)
//            {
//                chanceCanvasText.text = $"{chanceLetter} única chance";
//                chanceCanvasText.gameObject.SetActive(true);
//                InfinitChances = false;
//            }
//            else if (chanceLetter == 0)
//            {
//                chanceCanvasText.gameObject.SetActive(false);
//                InfinitChances = true;
//            }

//            if (!phraseBool)
//            {
//                StartCoroutine(PhraseNew());
//            }

//            if (MaxTimer.value == 0)
//            {
//                timerCount.PhraseTimer.gameObject.SetActive(false);
//            }
//            if (MaxTimer.value == 1)
//            {
//                timerCount.tLocal = 0;
//                timerCount.stepOne = true;
//                timerCount.PhraseTimer.gameObject.SetActive(true);
//            }
//            if (MaxTimer.value == 2)
//            {
//                timerCount.tLocal = 0;
//                timerCount.stepOne = true;
//                timerCount.PhraseTimer.gameObject.SetActive(true);
//            }
//            if (MaxTimer.value == 3)
//            {
//                timerCount.tLocal = 0;
//                timerCount.stepOne = true;
//                timerCount.PhraseTimer.gameObject.SetActive(true);
//            }
//            if (MaxTimer.value == 4)
//            {
//                timerCount.tLocal = 0;
//                timerCount.stepOne = true;
//                timerCount.PhraseTimer.gameObject.SetActive(true);
//            }

//            StartGame = true;
//            endGame = false;
//        }

//        if (TipsLimit == 0 && !EndTips)
//        {
//            EndTips = true;
//        }

//        if (chanceLetter < 0 && !InfinitChances)
//        {
//            endGame = true;

//            ComboStreakNumber = 0;
//            PlayerPrefs.SetInt("ComboStreak", ComboStreakNumber);
//        }

//        if (TimerEnd && !endGame)
//        {
//            modeCanvasText.text = "O tempo acabou!";
//            ComboStreakNumber = 0;
//            PlayerPrefs.SetInt("ComboStreak", ComboStreakNumber);

//            endGame = true;
//        }

//        if (!endGame)
//        {
//            if (MaxTimer.value == 1)
//            {
//                timerCount.Timer(15);
//            }
//            if (MaxTimer.value == 2)
//            {
//                timerCount.Timer(30);
//            }
//            if (MaxTimer.value == 3)
//            {
//                timerCount.Timer(60);
//            }
//            if (MaxTimer.value == 4)
//            {
//                timerCount.Timer(90);
//            }
//        }
//    }

//    // public void WhenGameStarted()
//    // {
//    //   twitchC = GameObject.Find("PlayMode").GetComponent<TwitchConnect>();
//    //   twitchC.WriteToChannel(twitchC.Channel, "ADiVinhE a palavra!");
//    // }

//    public void OnChatMessage(string pChatter, string pMessage)
//    {
//        if (GameObject.Find("PlayMode").activeInHierarchy == true)
//        {
//            if (!endGame && phraseGame != null)
//            {

//                StartGame = true;
//                MessageOn = true;
//                bool rightLetter = false;

//                string formatMessage = pMessage.ToLower();
//                string[] pMessageSplit = formatMessage.Split();
//                firstLetter = pMessageSplit[0];

//                if (endGame == false)
//                {

//                    if (firstLetter == phraseGame && pMessage.Length == 5)
//                    {
//                        ComboStreakNumber = ComboStreakNumber + 1;
//                        ComboText.text = $"{ComboStreakNumber} Streak";
//                        PlayerPrefs.SetInt("ComboStreak", ComboStreakNumber);

//                        print("descobriu a palavra!");
//                        modeCanvasText.text = "A palavra foi encontrada!";

//                        phraseCanvasText.text = phraseGen;
//                        endGame = true;

//                        UserKeyCanvasText.GetComponent<TextMeshProUGUI>().text = $"{pChatter} snipou a palavra!";
//                        UserKeyCanvasText.SetActive(true);

//                        PlayerSniper = $"{pChatter} 3";
//                        RankChange = true;

//                    }
//                    if (pMessage.Length == 5 && firstLetter != phraseGame && !EndTips)
//                    {
//                        var thisPhrase = pMessage.ToLower();
//                        var phraseSf = CaractereRemove.RemoveDiacritics(thisPhrase);

//                        if (dictionaryText.Contains(phraseSf))
//                        {
//                            int i = 0;
//                            var phraseSfSeparate = "";
//                            foreach (var phrase in phraseSf)
//                            {
//                                var length = phraseSfSeparate.Length;
//                                if (length < 1)
//                                {
//                                    phraseSfSeparate = phraseSfSeparate.Insert(0, $"{phrase}");
//                                }
//                                else
//                                {
//                                    phraseSfSeparate = phraseSfSeparate.Insert(length, $" {phrase}");
//                                }
//                                i = i + 1;
//                            }
//                            var phraseSfSplit = phraseSfSeparate.Split();

//                            i = 0;
//                            var gameSeparate = "";
//                            foreach (var game in phraseGame)
//                            {
//                                var length = gameSeparate.Length;
//                                if (length < 1)
//                                {
//                                    gameSeparate = gameSeparate.Insert(0, $"{game}");
//                                }
//                                else
//                                {
//                                    gameSeparate = gameSeparate.Insert(length, $" {game}");
//                                }
//                                i = i + 1;
//                            }
//                            var gameSeparateSplit = gameSeparate.Split();
//                            i = 0;
//                            int acurrate = 0;
//                            while (i < phraseGame.Length)
//                            {
//                                if (phraseSfSplit[i] == gameSeparateSplit[i] && phraseSfSplit[i] != phraseCensured.Substring(i, 1))
//                                {
//                                    string phraseInsert = phraseCensured.Insert(i, phraseSfSplit[i]);
//                                    string phraseReplace = phraseInsert.Remove(i + 1, 1);
//                                    phraseCensured = phraseReplace.ToLower();
//                                    var Upper = char.ToUpper(phraseCensured[0]) + phraseCensured.Substring(1);
//                                    phraseCanvasText.text = Upper;

//                                    acurrate = acurrate + 1;
//                                }
//                                i = i + 1;
//                            }
//                            if (acurrate != 0)
//                            {
//                                PlayerSniper = $"{pChatter} {acurrate}";
//                                RankChange = true;
//                            }

//                        }
//                        TipsLimit = TipsLimit - 1;
//                        TipsCanvasText.text = $"Dicas: {TipsLimit}";
//                    }

//                    if (firstLetter.Length == 1 && !EndTips)
//                    {

//                        char firstChar = char.Parse(firstLetter);

//                        if (phraseGame.IndexOf(firstLetter, 0) != -1)
//                        {
//                            int i = 0;
//                            int acurrate = 0;
//                            bool oneMoreLetter = false;
//                            for (i = 0; i < phraseGame.Length; i++)
//                            {
//                                if (phraseGame[i] == firstChar)
//                                {
//                                    string phraseInsert = phraseCensured.Insert(i, firstLetter);
//                                    string phraseReplace = phraseInsert.Remove(i + 1, 1);
//                                    phraseCensured = phraseReplace.ToLower();
//                                    var Upper = char.ToUpper(phraseCensured[0]) + phraseCensured.Substring(1);
//                                    phraseCanvasText.text = Upper;

//                                    if (!oneMoreLetter)
//                                    {
//                                        TipsLimit = TipsLimit - 1;
//                                        oneMoreLetter = true;
//                                    }

//                                    rightLetter = true;

//                                    acurrate = acurrate + 1;

//                                    TipsCanvasText.text = $"Dicas: {TipsLimit}";
//                                }
//                            }
//                            if (acurrate != 0)
//                            {
//                                PlayerSniper = $"{pChatter} {acurrate}";
//                                RankChange = true;
//                            }

//                            if (phraseCanvasText.text.IndexOf("_") == -1)
//                            {
//                                modeCanvasText.text = "A palavra foi encontrada!";
//                                endGame = true;
//                            }

//                        }

//                    }
//                    if (rightLetter == false && pMessage.Length == 5 && !InfinitChances)
//                    {
//                        chanceLetter = chanceLetter - 1;
//                        chanceCanvasText.text = $"{chanceLetter} chances restantes";

//                        if (chanceLetter == 0)
//                        {
//                            chanceCanvasText.text = $"Última chance!!!";
//                        }
//                    }
//                    else if (!rightLetter && pMessage.Length == 1 && !InfinitChances)
//                    {
//                        chanceLetter = chanceLetter - 1;
//                        chanceCanvasText.text = $"{chanceLetter} chances restantes";
//                    }
//                }

//            }

//        }
//    }

//    void RecordGame()
//    {
//        if (ComboStreakNumber > RecordStreak)
//        {
//            RecordStreak = ComboStreakNumber;
//            PlayerPrefs.SetInt("RecordStreak", RecordStreak);
//        }
//        if (RecordStreak > 0)
//        {
//            RecordText.text = $"Record: {RecordStreak}";
//        }
//        if (RecordStreak == 0)
//        {
//            RecordText.gameObject.SetActive(false);
//        }
//        else
//        {
//            RecordText.gameObject.SetActive(true);
//        }
//    }

//    void RankGame()
//    {

//        if (RankChange)
//        {

//            string[] playerSplit = PlayerSniper.Split();

//            string takePlayer = playerSplit[0];
//            int takeValue = int.Parse(playerSplit[1]);

//            int i = 0;
//            foreach (var item in RankList)
//            {
//                var itemSplit = item.Split();
//                RankPlayers.Add(itemSplit[0]);
//            }
//            if (RankPlayers.Contains(takePlayer))
//            {
//                print("Possui");

//                foreach (var item in RankPlayers)
//                {
//                    i = i + 1;
//                    if (item.Contains(takePlayer))
//                    {
//                        i = i - 1;
//                        var oldPointsSplit = RankList[i].Split();
//                        var oldPoints = int.Parse(oldPointsSplit[1]);
//                        var attPoints = takeValue + oldPoints;

//                        RankList[i] = $"{takePlayer} {attPoints}";
//                        i = i + 1;
//                    }
//                }

//            }
//            else if (!RankPlayers.Contains(takePlayer))
//            {
//                print("Não possui");
//                RankList.Add(PlayerSniper);
//            }

//            foreach (var item in RankList)
//            {

//                var itemSplit = item.Split();
//                RankValue.Add(int.Parse(itemSplit[1]));
//            }
//            int firstMax = RankValue.Max();
//            i = 0;
//            var playerFirstName = "vazio";
//            foreach (var item in RankList)
//            {
//                i = i + 1;
//                var itemSplit = item.Split();
//                if (itemSplit[1].Contains($"{firstMax}") && playerFirstName == "vazio")
//                {
//                    var player = itemSplit[0];
//                    var points = itemSplit[1];

//                    if (int.Parse(points) > 1)
//                    {
//                        firstPlace = $"{player} - {points} pontos";
//                    }
//                    else
//                    {
//                        firstPlace = $"{player} - {points} ponto";
//                    }


//                    i = i - 1;
//                    RankValue[i] = -1;
//                    i = i + 1;
//                    playerFirstName = player;

//                }
//            }
//            i = 0;
//            int secondMax = RankValue.Max();
//            var playerSecondName = "vazio";
//            foreach (var item in RankList)
//            {
//                i = i + 1;
//                var itemSplit = item.Split();
//                if (itemSplit[1].Contains($"{secondMax}") && itemSplit[0] != playerFirstName && playerSecondName == "vazio")
//                {
//                    var player = itemSplit[0];
//                    var points = itemSplit[1];
//                    if (int.Parse(points) < 1)
//                    {
//                        secondPlace = $"{player} - {points} pontos";
//                    }
//                    else
//                    {
//                        secondPlace = $"{player} - {points} ponto";
//                    }

//                    i = i - 1;
//                    RankValue[i] = -1;
//                    i = i + 1;
//                    playerSecondName = player;
//                }

//            }
//            i = 0;
//            var thirdMax = RankValue.Max();
//            playerOk = true;
//            print(thirdMax + ">>");
//            print(RankList.Count);
//            foreach (var item in RankList)
//            {
//                i = i + 1;
//                var itemSplit = item.Split();
//                if (itemSplit[1].Contains($"{thirdMax}") && itemSplit[0] != playerFirstName && itemSplit[0] != playerSecondName && playerOk)
//                {
//                    var player = itemSplit[0];
//                    var points = itemSplit[1];
//                    if (int.Parse(points) < 1)
//                    {
//                        thirdPlace = $"{player} - {points} pontos";
//                    }
//                    else
//                    {
//                        thirdPlace = $"{player} - {points}";
//                    }

//                    i = i - 1;
//                    RankValue[i] = -1;
//                    i = i + 1;
//                    playerOk = false;
//                }
//            }
//            RankFirstText.text = firstPlace;
//            RankSecondText.text = secondPlace;
//            RankThirdText.text = thirdPlace;

//            for (i = 0; i < RankList.Count; i++)
//            {
//                print($"{RankList[i]} :Rank");

//            }
//            RankValue.Clear();
//            RankPlayers.Clear();
//            playerFirstName = "vazio";
//            playerSecondName = "vazio";
//            RankChange = false;

//        }

//    }

//    void FinishArea()
//    {
//        if (endGame && !gameR)
//        {
//            FinishMenu.SetActive(true);
//            if (phraseCensured == phraseGame || firstLetter == phraseGame)
//            {
//                TitleFinish.text = "Lenda do Raios Funde";
//            }
//            else
//            {
//                TitleFinish.text = "Vergonha do Raios Funde";
//            }

//            PhraseFinish.text = phraseGen;
//            StreakFinish.text = $"{ComboStreakNumber} Streak";
//            RecordFinish.text = $"Record: {RecordStreak}";

//            if (MaxTimer.value != 0)
//            {
//                var minutes = timerCount.minutes;
//                var minutesTwo = int.Parse(minutes).ToString("00");
//                var seconds = float.Parse(timerCount.seconds).ToString("00");

//                if (int.Parse(minutes) > 0)
//                {
//                    TimerFinish.text = $"{minutesTwo}:{seconds}";
//                }
//                else
//                {
//                    seconds = float.Parse(timerCount.seconds).ToString("f2");
//                    TimerFinish.text = $"{seconds}";
//                }
//            }
//            FirstPlaceFinish.text = firstPlace;
//            SecondPlaceFinish.text = secondPlace;
//            ThirdPlaceFinish.text = thirdPlace;

//            if (firstPlace == null && secondPlace == null && thirdPlace == null)
//            {
//                RankBoxFinish.SetActive(false);
//            }
//            else
//            {
//                if (firstPlace != null)
//                {
//                    FirstPlaceFinish.gameObject.SetActive(true);
//                }
//                else
//                {
//                    FirstPlaceFinish.gameObject.SetActive(false);
//                }
//                if (secondPlace != null)
//                {
//                    SecondPlaceFinish.gameObject.SetActive(true);
//                }
//                else
//                {
//                    SecondPlaceFinish.gameObject.SetActive(false);
//                }
//                if (thirdPlace != null)
//                {
//                    ThirdPlaceFinish.gameObject.SetActive(true);
//                }
//                else
//                {
//                    ThirdPlaceFinish.gameObject.SetActive(false);
//                }

//                RankBoxFinish.SetActive(true);
//            }

//        }
//        else
//        {
//            FinishMenu.SetActive(false);
//        }

//    }
//    public void GameStart()
//    {
//        StartGame = false;
//    }

//    public void GameReset()
//    {
//        gameR = true;
//        FinishMenu.SetActive(false);

//        endGame = true;
//        StartGame = false;
//    }
//}
