using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public enum GameType
{
    None,
    Classic,
    Daily
}

public class GameManager : Singleton<GameManager>, IStateManageable
{
    public bool devMode = false;
    public int score;
    public int highScore;
    public GameType gameType = GameType.None;
    public WordGuessManager wordGuessManager;
    public string CurrentWord { get; set; } = String.Empty;
    public string CurrentWordSimplified { get; set; } = String.Empty;
    public BaseState CurrentState { get; private set; }
    
    public int NewUser { get; private set; }

    public Color backgroundColor;
    
    public UnityAction<GameType> OnGameTypeSelected;
    public UnityAction OnNewWord;
    public UnityAction OnNewDailyWord;
    public UnityAction OnGameWon;
    public UnityAction OnDailyGamePlayed;
    public UnityAction OnGameLost;
    public UnityAction OnItemBought;
    public UnityAction OnTextChanged;

    //Player Properties
    private int coins;
    private int hints;
    private int eliminations;

    public int interstitialFreq = 2;
    public int GamesWon { get; set; }

    public int CoinsAvailable
    {
        get => coins;
        set
        {
            coins = value;
            PlayerPrefs.SetInt("Coins", coins);
            OnTextChanged?.Invoke();
        }
    }

    public int HintsAvailable
    {
        get => hints;
        set
        {
            hints = value;
            PlayerPrefs.SetInt("Hints", hints);
            OnTextChanged?.Invoke();
        }
    }

    public int EliminationsAvailable
    {
        get => eliminations;
        set
        {
            eliminations = value;
            PlayerPrefs.SetInt("Eliminations", eliminations);
            OnTextChanged?.Invoke();
        }
    }
    
    public int startingCoins = 100;
    public int startingHints = 3;
    public int startingEliminations = 3;

    public int coinsPerGame = 60;
    public int decreasePerRow = 10;
    
    public int coinsPerGameDaily = 60;
    public int decreasePerRowDaily = 10;
    
    public int hintLimit = 3;
    [HideInInspector] public int timesHintUsed = 0;
    public int eliminationLimit = 3;
    [HideInInspector] public int timesEliminationUsed = 0;
    


    
    public Dictionary<string, BaseState> States { get; } = new Dictionary<string, BaseState>()
    {
        {"intro", new IntroState()},
        {"menu", new MenuState()},
        {"game", new GameState()},
        {"store", new StoreState()}
    };
    
    [Header("Daily")]
    [HideInInspector]public List<string> arabicMonths = new List<string>(){"يناير", "فبراير", "مارس", "أبريل", "مايو", "يونيو", "يوليو", "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر"};
    public int randomSeed = 1361997;
    [SerializeField] private string dailyWord;

    public string DailyWord
    {
        get { return dailyWord; }
        set
        {
            dailyWord = value;
            PlayerPrefs.SetString("DailyWord", dailyWord);
            OnNewDailyWord?.Invoke();
            wordGuessManager.ResetDaily();
            ((MainMenu)PagesManager.Instance.pages[0]).SetDaily();
        }
    }
    
    [Header("Background Settings")]
    public Image background;
    public Sprite[] patterns;
    [SerializeField] bool ResetEverything;

    [SerializeField] Image BackGroundImage;
    [SerializeField] Sprite[] BackgroundSprites;


    // Start is called before the first frame update
    void Start()
    {
        NewUser = PlayerPrefs.GetInt("NewUser", 0);
        if (!devMode)
        {
            if (NewUser == 0)
            {
                PlayerPrefs.SetInt("NewUser", 1);
                PlayerPrefs.SetInt("Coins", startingCoins);
                PlayerPrefs.SetInt("Hints", startingHints);
                PlayerPrefs.SetInt("Eliminations", startingEliminations);
                PlayerPrefs.SetString("DailyWord", "");
                PlayerPrefs.SetInt("Day", DateTime.UtcNow.Day);
                PlayerPrefs.SetInt("DailyButton", 1);
                PlayerPrefs.SetInt("Ads", 1);
            }
            CoinsAvailable = PlayerPrefs.GetInt("Coins");
            HintsAvailable = PlayerPrefs.GetInt("Hints");
            EliminationsAvailable = PlayerPrefs.GetInt("Eliminations");
        }
        else
        {
            CoinsAvailable = 1000;
            HintsAvailable = 10;
            EliminationsAvailable = 10;
        }


        score = PlayerPrefs.GetInt("Score");
        highScore = PlayerPrefs.GetInt("HighScore");
        SwitchState("intro");
        //Gley
        OnNewDailyWord += wordGuessManager.FetchDailyWord;
        if(NewUser == 0) DailyWord = PseudoDailyWord();
        else DailyWord = PlayerPrefs.GetString("DailyWord");
        StartCoroutine(CheckForDay());
        background.sprite = patterns[Random.Range(0, patterns.Length)];
        BackGroundImage.sprite = BackgroundSprites[Random.Range(0, BackgroundSprites.Length)];

        OnNewWord += RandomColor;
        AdsManager.Instance.InitializeAds();
        AdsManager.Instance.LoadBanner();
        //AdsManager.Instance.ShowBanner();


        if (ResetEverything)
        {
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("HighScore", 0);
            ResetEverything = false;
        }
    }

    public string PseudoDailyWord()
    {
        System.Random r = new System.Random(randomSeed);
        //Random.InitState(randomSeed);
        int dayMonth = DateTime.UtcNow.Day + DateTime.UtcNow.Month;
        int idx = 0;
        for(int i = 0; i < dayMonth; i++)
        {
            idx = r.Next(0, WordArray.WordList.Length);
        }
        //print($"daily word generated {WordArray.WordList[idx]}");
        return WordArray.WordList[idx];
    }
    
    private IEnumerator CheckForDay()
    {
        var day = PlayerPrefs.GetInt("Day");
        while (true)
        {
            if (day != DateTime.UtcNow.Day)
            {
                DailyWord = PseudoDailyWord();
                day = DateTime.UtcNow.Day;
                PlayerPrefs.SetInt("Day", day);
                yield break;
            }
            yield return new WaitForSeconds(60);
        }
    }

    void RandomColor()
    {
        
        if (wordGuessManager.pastState == InGameState.Win || wordGuessManager.pastState == InGameState.Loss)
        {
            Camera.main.backgroundColor = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 0.27f, 0.9f);
            BackGroundImage.sprite = BackgroundSprites[Random.Range(0, BackgroundSprites.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void Proceed()
    {
        PopupManager.Instance.CloseCurrentPopup();
        timesEliminationUsed = timesHintUsed = 0;
        if(gameType == GameType.Classic)
            wordGuessManager.ResetClassic();
        else if(gameType == GameType.Daily)
            wordGuessManager.Reset();
    }

    public void ResetScore()
    {
        score = 0;
        PlayerPrefs.SetInt("Score", score);
    }

    public void SwitchState(BaseState state)
    {
        CurrentState?.ExitState(this);
        CurrentState = state;
        CurrentState.EnterState(this);
    }
    
    public void SwitchState(string state)
    {
        CurrentState?.ExitState(this);
        CurrentState = States[state];
        CurrentState.EnterState(this);
    }
    
    public void SetGameType(GameType type)
    {
        gameType = type;
        OnGameTypeSelected?.Invoke(type);
    }

    public void EnableClassicMode()
    {
        /*wordGuessManagerClassic.gameObject.SetActive(true);
        wordGuessManagerClassic.enabled = true;
        wordGuessManagerDaily.gameObject.SetActive(false);
        wordGuessManagerDaily.enabled = false;
        wordGuessManager = wordGuessManagerClassic;*/
    }
    
    public void EnableDailyMode()
    {
        /*wordGuessManagerClassic.gameObject.SetActive(false);
        wordGuessManagerClassic.enabled = false;
        wordGuessManagerDaily.gameObject.SetActive(true);
        wordGuessManagerDaily.enabled = true;
        wordGuessManager = wordGuessManagerDaily;*/
    }

    public void DisableAds()
    {
        PlayerPrefs.SetInt("Ads", 0);
        AdsManager.Instance.HideBanner();
    }
}
