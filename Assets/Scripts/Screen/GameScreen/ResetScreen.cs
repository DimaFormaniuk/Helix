using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetScreen : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _menuButton;

    public Button ResetButton => _resetButton;
    public Button MenuButton => _menuButton;
}
