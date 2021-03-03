using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTheme : MonoBehaviour
{
    public static GameTheme Instante;
    //[SerializeField] private int index;

    [SerializeField] private List<Theme> _themes;

    public Theme CurrentTheme { get; private set; }

    private int _indexTheme;

    private void Awake()
    {
        Instante = this;
        //load some int
        InitCurrentTheme(PlayerPrefs.GetInt("theme"));
        //InitCurrentTheme(index);
    }

    public void InitCurrentTheme(int value = 0)
    {
        _indexTheme = value;
        CurrentTheme = _themes[_indexTheme];
    }

    public void GetNextTheme()
    {
        _indexTheme++;
        if (_indexTheme >= _themes.Count)
        {
            _indexTheme = 0;
        }
        CurrentTheme = _themes[_indexTheme];
        PlayerPrefs.SetInt("theme", _indexTheme);
    }
}

[Serializable]
public class Theme
{
    public Color _ball;
    public Color _beam;
    public Color _segment;
    public Color _deadSegment;

    [Header("Touch Floor")]
    public Color _splash;
    [Header("Big Ball")]
    public Color _bigBall;

    //public Color _abilibty;

    public Gradient _ballTrail;
    public Gradient _ballTrailAbility;

    public Gradient _backgraund;
}
