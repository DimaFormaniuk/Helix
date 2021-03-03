using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private Beam _beamTemplate;
    [SerializeField] private Ball _ballTemplate;

    [SerializeField] private float _additionalScale;

    [SerializeField] private Transform _parentForBall;
    [SerializeField] private Transform _parentForPlatform;

    [SerializeField] private float _step;
    [Header("Platform")]
    [SerializeField] private SpawnPlatform _spawnPlatform;
    //[SerializeField] private Platform[] _platform;
    [SerializeField] private FinishPlatform _finishPlatform;

    [SerializeField] private ScriptableLeveConfig _configEasy;
    [SerializeField] private ScriptableLeveConfig _configNormal;

    private Beam _beam;
    private Ball _ball;
    private BallJumper _ballJumper;

    public Beam Beam => _beam;
    public Ball Ball => _ball;
    public BallJumper BallJumper => _ballJumper;

    private int _levelCount;
    private int _level;
    private float _startAndFinishAdditionalScale => 0.5f * _step;

    public float BeamScaleY => (_levelCount * _step) / 2f + _startAndFinishAdditionalScale + _additionalScale / 2f;

    private List<GameObject> _listPlatforms;


    public void Build(int levelCount,int level)
    {
        _level = level;
        bool easy = _level % 3 == 0;

        _listPlatforms = new List<GameObject>();
        _levelCount = levelCount;

        Vector3 spawnPosition = SpawnBeam();

        _listPlatforms.Add(SpawnFirstPlatform(ref spawnPosition));
        if (easy)
            _listPlatforms.AddRange(SpawnPlatformsEasy(ref spawnPosition, _listPlatforms[0].transform.rotation.eulerAngles.y));
        else
            _listPlatforms.AddRange(SpawnPlatformsNormal(ref spawnPosition));
            //_listPlatforms.AddRange(SpawnPlatforms(ref spawnPosition));
        _listPlatforms.Add(SpawnFinishPlatform(ref spawnPosition));
    }

    private Vector3 SpawnBeam()
    {
        _beam = Instantiate(_beamTemplate, transform);
        _beam.transform.localScale = new Vector3(1, BeamScaleY, 1);

        Vector3 spawnPosition = _beam.transform.position;
        spawnPosition.y += _beam.transform.localScale.y - _additionalScale;
        return spawnPosition;
    }

    private GameObject SpawnFirstPlatform(ref Vector3 spawnPosition)
    {
        var obj = SpawnPlatform(_spawnPlatform, ref spawnPosition, _parentForPlatform);
        SpawnBall(obj.GetComponent<SpawnPlatform>().SpawnPointForBall.position);
        return obj;
    }

    //private List<GameObject> SpawnPlatforms(ref Vector3 spawnPosition)
    //{
    //    List<GameObject> listPlatforms = new List<GameObject>();

    //    for (int i = 0; i < _levelCount; i++)
    //    {
    //        listPlatforms.Add(SpawnPlatform(_platform[Random.Range(0, _platform.Length)], ref spawnPosition, _parentForPlatform));
    //    }

    //    return listPlatforms;
    //}

    private List<GameObject> SpawnPlatformsNormal(ref Vector3 spawnPosition)
    {
        List<GameObject> listPlatforms = new List<GameObject>();

        int n = _level + 4;
        if (n > _configNormal.PlatformsTemplate.Length)
            n = _configNormal.PlatformsTemplate.Length;

        for (int i = 0; i < _levelCount; i++)
        {
            listPlatforms.Add(SpawnPlatform(_configNormal.PlatformsTemplate[Random.Range(0, n)], ref spawnPosition, _parentForPlatform));
        }

        return listPlatforms;
    }

    private List<GameObject> SpawnPlatformsEasy(ref Vector3 spawnPosition, float rotationY)
    {
        List<GameObject> listPlatforms = new List<GameObject>();

        for (int i = 0; i < _levelCount; i++)
        {
            listPlatforms.Add(SpawnPlatformStep(_configEasy.PlatformsTemplate[Random.Range(0, _configEasy.PlatformsTemplate.Length)], ref spawnPosition, _parentForPlatform, rotationY));
            rotationY = listPlatforms[i].transform.rotation.eulerAngles.y;
        }

        return listPlatforms;
    }

    private GameObject SpawnFinishPlatform(ref Vector3 spawnPosition)
    {
        return SpawnPlatform(_finishPlatform, ref spawnPosition, _parentForPlatform);
    }

    private void SpawnBall(Vector3 pointBall)
    {
        _ball = Instantiate(_ballTemplate, pointBall, Quaternion.identity, _parentForBall);
        _ballJumper = _ball.gameObject.GetComponent<BallJumper>();
    }

    private GameObject SpawnPlatform(Platform platform, ref Vector3 spawnPosition, Transform parent, float rotationY = 0, float a = 0, float b = 360)
    {
        var obj = Instantiate(platform, spawnPosition, Quaternion.Euler(0, Random.Range(0, Random.Range(a, b)) + rotationY, 0), parent);
        spawnPosition.y -= _step;
        return obj.gameObject;
    }

    private GameObject SpawnPlatformStep(Platform platform, ref Vector3 spawnPosition, Transform parent, float rotationY = 0, float step = 15)
    {
        float y = Random.Range(0, 100) > 50 ? step : -step;
        var obj = Instantiate(platform, spawnPosition, Quaternion.Euler(0, y + rotationY, 0), parent);
        spawnPosition.y -= _step;
        return obj.gameObject;
    }

    public void DestroyLevel()
    {
        Destroy(_beam.gameObject);
        Destroy(_ball.gameObject);

        for (int i = 0; i < _listPlatforms.Count; i++)
        {
            Destroy(_listPlatforms[i]);
        }
    }
}
