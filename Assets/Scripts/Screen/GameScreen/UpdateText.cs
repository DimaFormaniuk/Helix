using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void OnUpdateText(int value)
    {
        _text.text = value.ToString();
    }

    public void OnUpdateText(string value)
    {
        _text.text = value.ToString();
    }
}
