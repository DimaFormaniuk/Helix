using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private int _countDestoyPlatform;
    private int _stamina;

    private int _contValue = 3;

    public bool BallCanDead => _stamina < _contValue;
    public bool BallHaveAbility => _stamina >= _contValue;

    public event UnityAction<int> DestroyPlatform;
    public event UnityAction<int> UpdateStamina;
    public event UnityAction UseAbility;

    private void Start()
    {
        _countDestoyPlatform = 0;
        _stamina = 0;
        GetComponent<Renderer>().material.color = GameTheme.Instante.CurrentTheme._ball;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlatformTrigger platformTrigger))
        {
            Destroy(platformTrigger.gameObject);
            other.GetComponentInParent<Platform>().Break();
            AddDestroyPlatform();
            StaminaAdd();
        }
    }

    public void StopKinematic()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void StaminaAdd()
    {
        Managers.SoundController.Instance.DestroyPlatform(_stamina);
        _stamina++;
        UpdateStamina.Invoke(_stamina);
    }

    public void StaminaZero()
    {
        _stamina = 0;
        UpdateStamina.Invoke(_stamina);
    }

    private void AddDestroyPlatform()
    {
        _countDestoyPlatform++;
        DestroyPlatform?.Invoke(_stamina + 1);
    }

    public void OnStaminaAbility(PlatformSegment platformSegment)
    {
        if (BallHaveAbility)
        {
            platformSegment.GetComponentInParent<Platform>().Break(GameTheme.Instante.CurrentTheme._ball);
            _countDestoyPlatform++;
        }
        StaminaZero();
    }

    public int GetCompleted(int countPlatform)
    {
        float a = _countDestoyPlatform;
        float b = countPlatform;
        int res = (int)Mathf.Round(100f * (a / b));
        return res;
    }
}
