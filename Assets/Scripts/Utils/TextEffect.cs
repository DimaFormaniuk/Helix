using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    [SerializeField] private Text _text;

    private Color _textColor;
    private float _lifeTime = 0.12f;
    private float _delayAlpha = 1.5f;
    private float _speedDirection = 600f;
    private Vector3 _direction = Vector3.up;

    public void Init(string text, float fontSize, Color color)
    {
        _text.text = text;
        _text.color = color;
        _text.fontSize = (int)fontSize;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        transform.position += _direction * _speedDirection * Time.deltaTime;

        if (_lifeTime <= 0f)
        {
            _textColor = _text.color;
            _textColor.a -= Time.deltaTime * _delayAlpha;
            _text.color = _textColor;
            if (_textColor.a <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
