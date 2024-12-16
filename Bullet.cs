using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    private Rigidbody _Rb;
    private Action _OnDestroyBullet;

    public void Initialize(Action pOnDestroyCallback)
    {
        _OnDestroyBullet = pOnDestroyCallback;
        _Rb = gameObject.AddComponent<Rigidbody>();
        _Rb.useGravity = false;
    }

    public void Shoot(Vector3 pVelocity)
    {
        _Rb.velocity = pVelocity;
        Invoke(nameof(DestroyBullet), 3f);
    }

    public void ResetBullet()
    {
        _Rb.velocity = Vector3.zero;
        CancelInvoke(nameof(DestroyBullet));
    }

    private void DestroyBullet() => _OnDestroyBullet?.Invoke();
}
