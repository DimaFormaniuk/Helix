using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishSegment : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = GameTheme.Instante.CurrentTheme._ball;
    }
}
