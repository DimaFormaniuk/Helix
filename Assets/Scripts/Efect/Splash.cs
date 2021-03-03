using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    void Start()
    {
        Invoke("DestroyThis", _lifeTime);

        GetComponent<Renderer>().material.color = GameTheme.Instante.CurrentTheme._splash;
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
