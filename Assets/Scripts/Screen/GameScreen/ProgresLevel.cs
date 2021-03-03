using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgresLevel : MonoBehaviour
{
    [SerializeField] private Text _fromLevelText;
    [SerializeField] private Text _toLevelText;
    [SerializeField] private Slider _progresBar;
    [SerializeField] private float _speedFiling;

    private float _targetValue;

    public void SetCurentLevel(int value)
    {
        _fromLevelText.text = value.ToString();
        _toLevelText.text = (value + 1).ToString();
        OnUpdateValue(0);
        _progresBar.value = 0;
    }

    public void OnUpdateValue(float value)
    {
        _targetValue = value;
    }

    private void Update()
    {
        _progresBar.value = Mathf.Lerp(_progresBar.value, _targetValue, _speedFiling);
    }
}
