using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigLevel", menuName = "ConfigLevel")]
public class ScriptableLeveConfig : ScriptableObject
{
    public Platform[] PlatformsTemplate;
}
