using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTraking : MonoBehaviour
{
    [SerializeField] private Vector3 _diractionOffset;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _lenght;

    private Ball _ball;
    private Beam _beam;
    private Vector3 _cameraPosition;
    private Vector3 _minimalBallPosition;
    private bool _traking;

    public void StartTraking(Beam beam, Ball ball)
    {
        _beam = beam;
        _ball = ball;
        
        _cameraPosition = _ball.transform.position;
        _minimalBallPosition = _ball.transform.position;

        TrackBall();

        _traking = true;

        _ball.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }

    public void StopTraking()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        _traking = false;
    }

    private void Update()
    {
        if (_traking)
        {
            if (_ball.transform.position.y < _minimalBallPosition.y)
            {
                TrackBall();
                _minimalBallPosition = _ball.transform.position;
            }
        }
    }

    private void TrackBall()
    {
        Vector3 beamPosition = _beam.transform.position;
        beamPosition.y = _ball.transform.position.y;
        _cameraPosition = _ball.transform.position;
        Vector3 diraction = (beamPosition - _ball.transform.position).normalized + _diractionOffset;
        _cameraPosition -= diraction * _lenght;
        transform.position = _cameraPosition;// new Vector3(_cameraPosition.x, _cameraPosition.y + _offsetY, _cameraPosition.z);
        transform.LookAt(new Vector3(_ball.transform.position.x, _ball.transform.position.y + _offsetY, _ball.transform.position.z));
    }
}
