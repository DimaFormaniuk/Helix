using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    private float _startPoint;
    private float _nextPoint;

    private void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPoint = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            _nextPoint = Input.mousePosition.x;
            Quaternion _target = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + ((_startPoint - _nextPoint) * _rotateSpeed), transform.rotation.eulerAngles.z));
            _startPoint = _nextPoint;
            transform.rotation = _target;
        }
    }

    private void Update()
    {
        HandleMouse();
    }
}
