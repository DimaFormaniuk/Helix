using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class BackGroundGame : MonoBehaviour
{
    [SerializeField] private Gradient2 _gradient;

    public void Refresh()
    {
        _gradient.EffectGradient = GameTheme.Instante.CurrentTheme._backgraund;
    }

}
