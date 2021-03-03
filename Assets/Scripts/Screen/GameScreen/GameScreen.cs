using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Analytics;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private GameObject _particleShow;
    [SerializeField] private TowerBuilder _towerBuilder;
    [SerializeField] private TowerRotator _towerRotator;
    [SerializeField] private BallTraking _ballTraking;
    [SerializeField] private Score _score;
    [SerializeField] private UpdateText _scoreText;
    [SerializeField] private UpdateText _bestText;
    [SerializeField] private UpdateText _completedText;
    [SerializeField] private ProgresLevel _progresLevel;
    [SerializeField] private BackGroundGame _backGround;

    [SerializeField] private ResetScreen _resetScreen;
    [SerializeField] private FinishScreen _finishScreen;

    private int _currentLevel = 1;
    private int _countPlatform = 15;

    private int _countDead = 0;
    private int _countWon = 0;

    private event UnityAction<string> CompletedText;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Init") == 0)
        {
            PlayerPrefs.SetInt("Init", 1);
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("platform", 15);
            PlayerPrefs.SetInt("theme", 0);
        }
        else
        {
            _currentLevel = PlayerPrefs.GetInt("level");
            _countPlatform = PlayerPrefs.GetInt("platform");
        }
    }

    private void OnEnable()
    {
        _resetScreen.ResetButton.onClick.AddListener(OnRestart);
        //_resetScreen.MenuButton.onClick.AddListener(OnOpenMenu);
        _finishScreen.Button.onClick.AddListener(OnNextLevel);
        _score.UpdateValue += _scoreText.OnUpdateText;
        _score.UpdateBestValue += _bestText.OnUpdateText;
        CompletedText += _completedText.OnUpdateText;
    }

    private void OnDisable()
    {
        _resetScreen.ResetButton.onClick.RemoveListener(OnRestart);
        //_resetScreen.MenuButton.onClick.RemoveListener(OnOpenMenu);
        _finishScreen.Button.onClick.RemoveListener(OnNextLevel);
        _score.UpdateValue -= _scoreText.OnUpdateText;
        _score.UpdateBestValue -= _bestText.OnUpdateText;
        CompletedText -= _completedText.OnUpdateText;
    }

    private void Subscribe()
    {
        _towerBuilder.Ball.DestroyPlatform += _score.OnAddValue;
        _towerBuilder.Ball.DestroyPlatform += OnUpdateProgresBar;
        _towerBuilder.BallJumper.Finish += OnFinish;
        _towerBuilder.BallJumper.Dead += OnDead;
    }

    private void Unsubscribe()
    {
        _towerBuilder.Ball.DestroyPlatform -= _score.OnAddValue;
        _towerBuilder.Ball.DestroyPlatform -= OnUpdateProgresBar;
        _towerBuilder.BallJumper.Finish -= OnFinish;
        _towerBuilder.BallJumper.Dead -= OnDead;
    }

    public void StartLevel()
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
            "LevelWin",
                new Dictionary<string, object>
                {
                    {"Level",_currentLevel }
                }
            );
        //Debug.Log(analyticsResult);

        _particleShow.SetActive(true);
        _towerBuilder.Build(_countPlatform, _currentLevel);
        _towerRotator.enabled = true;
        _score.SetPricePlatform(_currentLevel);
        _progresLevel.SetCurentLevel(_currentLevel);
        Subscribe();

        _ballTraking.StartTraking(_towerBuilder.Beam, _towerBuilder.Ball);
        _ballTraking.enabled = true;

        _resetScreen.gameObject.SetActive(false);
        _finishScreen.gameObject.SetActive(false);

        _backGround.Refresh();

        AdmobAds.Instance.ShowBanner();
    }

    private void OnFinish()
    {
        _towerBuilder.Ball.StopKinematic();
        _towerBuilder.Ball.StaminaZero();
        _towerRotator.enabled = false;
        _finishScreen.gameObject.SetActive(true);
        Managers.SoundController.Instance.LevelCompleted();

        AdmobAds.Instance.HideBanner();

        _countWon++;
        if (_countWon % 3 == 0)
        {
            AdmobAds.Instance.ShowVideoRewarded();
        }
    }

    private void OnNextLevel()
    {
        _currentLevel++;
        _countPlatform += 3;
        GameTheme.Instante.GetNextTheme();
        PlayerPrefs.SetInt("level", _currentLevel);
        PlayerPrefs.SetInt("platform", _countPlatform);
        _finishScreen.gameObject.SetActive(false);
        OnRestart();
    }

    private void OnDead()
    {
        _towerBuilder.Ball.StopKinematic();
        _towerBuilder.Ball.StaminaZero();
        _towerRotator.enabled = false;
        Managers.SoundController.Instance.Dead();

        OnCalculateCompleted();

        _resetScreen.gameObject.SetActive(true);

        _countDead++;
        if (_countDead % 3 == 0)
        {
            AdmobAds.Instance.ShowVideo();
        }
    }

    private void OnRestart()
    {
        DestroyLevel();
        StartLevel();
    }

    //private void OnOpenMenu()
    //{
    //_particleShow.SetActive(false);
    //DestroyLevel();
    //ScreenController.Instance.ShowMainScreen();
    //}

    private void DestroyLevel()
    {
        _score.ResetValue();
        Unsubscribe();
        _ballTraking.StopTraking();
        _ballTraking.enabled = false;
        _towerRotator.enabled = false;
        _towerBuilder.DestroyLevel();
    }

    private void OnCalculateCompleted()
    {
        string s = "";
        s += _towerBuilder.Ball.GetCompleted(_countPlatform + 1);
        s += "% COMPLETED";
        CompletedText?.Invoke(s);
    }

    private void OnUpdateProgresBar(int value)
    {
        _progresLevel.OnUpdateValue(_towerBuilder.Ball.GetCompleted(_countPlatform + 1) / 100f);
    }
}