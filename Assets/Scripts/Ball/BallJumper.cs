using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Ball))]
public class BallJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _timeCanJump;
    [SerializeField] private Vector3 _forceDown;

    private Rigidbody _rigidbody;
    private bool _canJump;
    private Ball _ball;

    public event UnityAction<PlatformSegment> TouchFloor;
    public event UnityAction<PlatformSegment> TouchFloorAbility;
    public event UnityAction Dead;
    public event UnityAction Finish;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _ball = GetComponent<Ball>();

        _canJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canJump == false) return;
        if (collision.gameObject.TryGetComponent(out PlatformSegmentDead platformSegmentDead))
        {
            if (_ball.BallCanDead)
            {
                _ball.StaminaZero();
                Dead?.Invoke();
            }
            else
            {
                Jump(platformSegmentDead);
            }
        }
        else if (collision.gameObject.TryGetComponent(out PlatformSegment platformSegment))
        {
            Jump(platformSegment);
        }
        else if (collision.gameObject.TryGetComponent(out FinishSegment finishSegment))
        {
            Finish?.Invoke();
        }
    }

    private void Jump(PlatformSegment platformSegment)
    {
        _canJump = false;
        if (_ball.BallHaveAbility)
        {
            TouchFloorAbility?.Invoke(platformSegment);
        }
        TouchFloor?.Invoke(platformSegment);

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        Invoke("CanJump", _timeCanJump);
        Managers.SoundController.Instance.TouchFloor();
    }

    private void CanJump()
    {
        _canJump = true;
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.y < _forceDown.y)
        {
            _rigidbody.velocity = _forceDown;
        }
    }
}
