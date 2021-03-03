using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class PlatformSegment : MonoBehaviour
{
    private bool _addComponent;

    private void Start()
    {
        GetComponent<Renderer>().material.color = GameTheme.Instante.CurrentTheme._segment;
        _addComponent = false;
    }

    public void Bounce(float force, Vector3 center, float radius)
    {
        if (_addComponent) return;
        _addComponent = true;
        gameObject.AddComponent<Rigidbody>();
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.AddExplosionForce(force, center, radius);
        }
        if (TryGetComponent(out MeshCollider meshCollider))
        {
            meshCollider.enabled = false;
        }
    }
}
