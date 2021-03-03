using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : MonoBehaviour
{
    public static GameAsset Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject PrefabText;
}
