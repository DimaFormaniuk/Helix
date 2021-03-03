using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroy : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;

    public void DestroyAll()
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            Destroy(_objects[i]);
        }
    }
}
