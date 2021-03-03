using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public static ScreenController Instance;

    [SerializeField] private GameObject _logoScreen;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private FadeOut _fadeOut;

    private List<GameObject> _screens;

    private void Awake()
    {
        Instance = this;

        Init();

        ShowLogoScreen();
    }

    private void Init()
    {
        _screens = new List<GameObject>();
        _screens.Add(_logoScreen);
        _screens.Add(_gameScreen);
    }

    private void HideAll()
    {
        for (int i = 0; i < _screens.Count; i++)
        {
            _screens[i].SetActive(false);
        }
    }

    private void ShowLogoScreen()
    {
        HideAll();
        _logoScreen.SetActive(true);
    }

    public void ShowGameScreen()
    {
        _fadeOut.Show(() =>
        {
            HideAll();
            _gameScreen.SetActive(true);
            _gameScreen.GetComponent<GameScreen>().StartLevel();
        });
    }
}
