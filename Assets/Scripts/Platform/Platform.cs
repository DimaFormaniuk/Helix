using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _bounceForce;
    [SerializeField] private float _bounceRadius;
    [SerializeField] private float _lifeBeforDestroy;

    public void Break()
    {
        PlatformSegment[] platformSegments = GetComponentsInChildren<PlatformSegment>();

        foreach (var segment in platformSegments)
        {
            segment.Bounce(_bounceForce, transform.position, _bounceRadius);
        }

        Invoke("DestroyPlatform", _lifeBeforDestroy);
    }

    public void Break(Color color)
    {
        PlatformSegment[] platformSegments = GetComponentsInChildren<PlatformSegment>();

        foreach (var segment in platformSegments)
        {
            segment.Bounce(_bounceForce, transform.position, _bounceRadius);
            segment.GetComponent<Renderer>().material.color = color;
        }

        Invoke("DestroyPlatform", _lifeBeforDestroy);

        Destroy(GetComponentInChildren<PlatformTrigger>().gameObject);
    }

    private void DestroyPlatform()
    {
        Destroy(gameObject);
    }
}
