using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallLogo : MonoBehaviour
{
    [SerializeField] private float _delayTime;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = true;
        Invoke("Kine", _delayTime);
    }

    private void Kine()
    {
        _rigidbody2D.isKinematic = false;
    }
}
