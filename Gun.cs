using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private ObjectPool<Bullet> _BulletPool;

    [SerializeField] private GameObject _SpawnPoint;
    [SerializeField] int _PoolSize = 20;

    public void Start()
    {
        _BulletPool = new ObjectPool<Bullet>(
            pOnCreate: CreateBullet,
            pOnGet: OnBulletGet,
            pOnRelease: OnBulletRelease,
            pMaxSize: _PoolSize
        );

        for (int i = _PoolSize - 1; i >= 0; i--)
        {
            Bullet lBullet = CreateBullet();
            _BulletPool.ReleaseObj(lBullet);
        }
            
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _BulletPool.GetObj();
    }

    private Bullet CreateBullet()
    {
        GameObject lBulletObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        lBulletObj.transform.localScale *= .2f;
        Bullet lBullet = lBulletObj.AddComponent<Bullet>();
        lBullet.Initialize(() => _BulletPool.ReleaseObj(lBullet));
        return lBullet;
    }

    private void OnBulletGet(Bullet pBullet)
    {
        pBullet.gameObject.SetActive(true);
        pBullet.transform.position = _SpawnPoint.transform.position;
        pBullet.Shoot(Vector3.forward * 10f);
    }

    private void OnBulletRelease(Bullet pBullet)
    {
        pBullet.gameObject.SetActive(false);
        pBullet.ResetBullet();
    }

}
