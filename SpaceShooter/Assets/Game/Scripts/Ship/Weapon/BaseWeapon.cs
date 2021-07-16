using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeaponController
{
    [SerializeField] protected int Damage;
    [SerializeField] protected float FireRate;
    [SerializeField] protected int BulletForce;
    [SerializeField] protected Transform[] BulletSpaws;
    [SerializeField] protected Bullet BulletPrefab;

    public abstract void HandleInput();

    public void Shoot()
    {
        foreach (Transform spaw in BulletSpaws)
        {
            Bullet tempBullet = Instantiate(BulletPrefab, spaw.position, spaw.rotation);
            tempBullet.Init(Damage, BulletForce);
        }
    }
}
