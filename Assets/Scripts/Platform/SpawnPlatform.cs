using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : Platform
{
    [SerializeField] private Transform _spawnPoint;
    public Transform SpawnPointForBall => _spawnPoint;
}
