using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = GameTheme.Instante.CurrentTheme._beam;
    }
}
