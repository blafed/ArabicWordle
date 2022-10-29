using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : Page
{
    public Button playClassicButton;
    public Button playDailyButton;
    public Button settingsButton;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI month;
    public TextMeshProUGUI day;
    public Image check;


    private void Start()
    {
        /*highScore.text = GameManager.Instance.highScore.ToString();
        coinsText.text = GameManager.Instance.CoinsAvailable.ToString();
        GameManager.Instance.OnTextChanged += SetText;
        SetCalendar();
        GameManager.Instance.OnNewDailyWord += SetCalendar;*/
    }
    
    void SetText()
    {
        coinsText.DOText(GameManager.Instance.CoinsAvailable.ToString(), 0.25f);
    }

    void SetCalendar()
    {
        month.text = GameManager.Instance.arabicMonths[DateTime.Now.Month - 1];
        day.text = DateTime.Now.Day.ToString();
    }

    public void PlayClassic()
    {
        GameManager.Instance.SetGameType(GameType.Classic);
        GameManager.Instance.SwitchState(GameManager.Instance.States["game"]);
    }
    
    public void PlayDaily()
    {
        GameManager.Instance.SetGameType(GameType.Daily);
        GameManager.Instance.SwitchState(GameManager.Instance.States["game"]);
    }

    public void SetDaily()
    {
        playDailyButton.interactable = PlayerPrefs.GetInt("DailyButton") == 1;
        check.gameObject.SetActive(PlayerPrefs.GetInt("DailyButton") == 0);
    }
    private void OnEnable()
    {
        highScore.text = GameManager.Instance.highScore.ToString();
        coinsText.text = GameManager.Instance.CoinsAvailable.ToString();
        GameManager.Instance.OnTextChanged += SetText;
        SetCalendar();
        GameManager.Instance.OnNewDailyWord += SetCalendar;
        SetDaily();
    }
}
