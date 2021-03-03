using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform _positionCompliment;
    [SerializeField] private Transform _positionScore;

    [SerializeField] private TextEffect _textTemplate;
    [SerializeField] private Color _scoreAddColor;
    [SerializeField] private Color _complimentColor;


    private int _price;
    private int _value;
    public int Value => _value;

    private int _bestValue;
    public int BestValue => _bestValue;

    public event UnityAction<int> UpdateValue;
    public event UnityAction<string> UpdateBestValue;

    private string[] _complimentText = { "Nice", "WoW", "Good", "Great", "Perfectly", "That's great" };

    private void Start()
    {
        _value = 0;
        //Load
        _bestValue = PlayerPrefs.GetInt("best", 0);

        UpdateValue?.Invoke(_value);
        UpdateBestValue?.Invoke("BEST: " + _bestValue);
    }

    public void SetPricePlatform(int price)
    {
        _price = price;
    }

    public void OnAddValue(int count)
    {
        int add = count * _price;
        CreateText("+" + add, _positionScore, 45, _scoreAddColor);

        if (count >= 4)
        {
            if (Random.Range(0, 100) > 50)
            {
                CreateText(_complimentText[Random.Range(0, _complimentText.Length)], _positionCompliment, 25, _complimentColor);
            }
        }

        _value += add;
        UpdateValue?.Invoke(_value);
    }

    private void CreateText(string text, Transform transfom, float fontSize, Color color)
    {
        Instantiate(_textTemplate, transfom.position, Quaternion.identity, transfom).Init(text, fontSize, color);
    }

    public void ResetValue()
    {
        if (_value > _bestValue)
        {
            _bestValue = _value;
            //save best
            UpdateBestValue?.Invoke("BEST: " + _bestValue);
            PlayerPrefs.SetInt("best", _bestValue);
        }
        _value = 0;
        UpdateValue?.Invoke(_value);
    }
}
