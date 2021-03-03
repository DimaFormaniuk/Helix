using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSplash : MonoBehaviour
{
    [SerializeField] private GameObject _splashTemplate;
    [SerializeField] private ParticleSystem _particleSplash;
    [SerializeField] private ParticleSystem _particleSplash3;
    [SerializeField] private ParticleSystem _particleDead;

    [SerializeField] private ParticleSystem _particleCircle;
    [SerializeField] private GameObject _easyLineEfect;
    [SerializeField] private GameObject _lineEfect;
    [SerializeField] private GameObject _ballAbility;

    [SerializeField] private BallJumper _ballJumper;
    [SerializeField] private Ball _ball;

    [SerializeField] private SpriteRenderer sprite1;
    [SerializeField] private SpriteRenderer sprite2;
    [SerializeField] private SpriteRenderer sprite3;

    private void Start()
    {
        _lineEfect.SetActive(false);
        _ballAbility.SetActive(false);
        _easyLineEfect.GetComponent<TrailRenderer>().colorGradient = GameTheme.Instante.CurrentTheme._ballTrail;
        _lineEfect.GetComponent<TrailRenderer>().colorGradient = GameTheme.Instante.CurrentTheme._ballTrailAbility;
        _ballAbility.GetComponent<Renderer>().material.color = GameTheme.Instante.CurrentTheme._bigBall;

        ParticleSystem.MainModule psrCircle = _particleCircle.main;
        psrCircle.startColor = GameTheme.Instante.CurrentTheme._ball;

        Color colorTemp = GameTheme.Instante.CurrentTheme._bigBall;
        colorTemp.a = colorTemp.a - 0.2f;
        sprite1.color = colorTemp;
        colorTemp.a = colorTemp.a - 0.2f;
        sprite2.color = colorTemp;
        colorTemp.a = colorTemp.a - 0.2f;
        sprite3.color = colorTemp;
    }

    private void OnEnable()
    {
        _ballJumper.TouchFloor += _ball.OnStaminaAbility;
        _ballJumper.TouchFloor += OnSplashEffect;
        _ballJumper.TouchFloorAbility += OnSplashEffectAbility;
        _ball.UpdateStamina += OnUpdateStamina;
        _ballJumper.Dead += OnDead;
    }

    private void OnDisable()
    {
        _ballJumper.TouchFloor -= _ball.OnStaminaAbility;
        _ballJumper.TouchFloor -= OnSplashEffect;
        _ballJumper.TouchFloorAbility -= OnSplashEffectAbility;
        _ball.UpdateStamina -= OnUpdateStamina;
        _ballJumper.Dead -= OnDead;
    }

    private void OnSplashEffect(PlatformSegment platformSegment)
    {
        SplashEffect(platformSegment.transform, _particleSplash, GameTheme.Instante.CurrentTheme._splash);
    }

    private void OnSplashEffectAbility(PlatformSegment platformSegment)
    {
        SplashEffect(platformSegment.transform, _particleSplash3, GameTheme.Instante.CurrentTheme._splash);
    }

    private void SplashEffect(Transform transformPoint, ParticleSystem particle, Color color)
    {
        CreateSplashOnFloor(transformPoint);
        ParticleSplash(particle, color);
    }

    private void CreateSplashOnFloor(Transform transformPoint)
    {
        var obj = Instantiate(
        _splashTemplate,
        _ball.transform.position,
        Quaternion.Euler(-90f, Random.Range(0, 360), 0),
        transformPoint);

        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 0.065f, obj.transform.position.z);
    }

    private void OnDead()
    {
        ParticleSplash(_particleDead, GameTheme.Instante.CurrentTheme._splash);
    }

    private void ParticleSplash(ParticleSystem particle, Color color)
    {
        ParticleSystem ps = Instantiate(particle, _ball.transform.position, Quaternion.identity);

        ParticleSystem.MainModule psr = ps.main;
        psr.startColor = color;
        ps.Play();
    }

    private void OnUpdateStamina(int value)
    {
        if (value == 0)
        {
            _particleCircle.Stop();
            _lineEfect.SetActive(false);
            _ballAbility.SetActive(false);
            _particleCircle.gameObject.SetActive(false);
        }
        if (value == 2)
        {
            if (!_particleCircle.isPlaying)
            {
                _lineEfect.SetActive(true);
                _particleCircle.gameObject.SetActive(true);
                _particleCircle.Stop();
                ParticleSystem.MainModule psrCircle = _particleCircle.main;
                psrCircle.startColor = GameTheme.Instante.CurrentTheme._ball;
                _particleCircle.Play();
            }
        }
        if (value == 3)
        {
            _ballAbility.SetActive(true);
            //ParticleSystem.MainModule psrCircle = _particleCircle.main;
            //psrCircle.startColor = GameTheme.Instante.CurrentTheme._bigBall;
            //_particleCircle.Play();
        }
    }
}
