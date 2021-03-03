using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeOut : MonoBehaviour
{
    [SerializeField] private float durationFadeOut = 1f;
    private CanvasGroup _canvasGroup;
    private Action _action;

    private bool tr = false;
    private bool on = false;
    private float time = 0;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(Action action)
    {
        GameAnimation.Value = tr = true;
        _action = action;
        time = 0;
        on = true;
    }

    void Update()
    {
        if (tr)
        {
            time += Time.deltaTime / durationFadeOut;
            if (on)
            {
                _canvasGroup.alpha = time * 2f;
                if (time >= 0.5f)
                {
                    on = false;
                    _action.Invoke();
                }
            }
            else
            {
                _canvasGroup.alpha = 1f + (1f - time * 2f);
                if (time >= 1f)
                {
                    GameAnimation.Value = tr = false;
                }
            }
        }
    }
}
