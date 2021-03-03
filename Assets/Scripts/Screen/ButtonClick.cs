using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private int _index;

    private void OnMouseUpAsButton()
    {
        switch (_index)
        {
            //case 1: ScreenController.Instance.ShowMainScreen(); break;
            //case 2: ScreenController.Instance.ShowGameScreen(); break;
            //case 3: break;
            //case 4: break;
        }
    }
}
