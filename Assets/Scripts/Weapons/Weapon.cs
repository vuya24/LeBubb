using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public float damage, range, projectileSpeed = float.PositiveInfinity, spread, roundsPerMinute;

    [SerializeField]
    public int multishot = 5;

    public float ShootPeriod = 1;
    public float ProjectileLifeTime => range / projectileSpeed;

    [SerializeField]
    Transform barrelTransform;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Transform bulletPosition;

    //private float soundPitchDispersion = 0.05f;

    bool isShooting = false;

    //[SerializeField]
    //Bullet projectile;

    [SerializeField]
    TurretDriver turretDriver;
    public TurretDriver TurretDriver { get => turretDriver; private set => turretDriver = value; }

    GameObject projectilePool;

    TargetingSystem targetingSystem;

    private void Awake()
    {

    }

    private void Update()
    {
        turretDriver.LookAt(playerTransform.position);
        turretDriver.Update();

        if(IsInRange(playerTransform.position) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(Shoot());
        }

        if (!IsInRange(playerTransform.position))
        {
            isShooting = false;
        }
    }

    public void Trigger()
    {
    }

    public bool IsInRange(Vector2 position)
    {
        return (position - (Vector2)barrelTransform.position).magnitude < range;
    }

    void ShootProjectile()
    {
        GameObject bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = bulletPosition.position + gameObject.transform.up;
            bullet.SetActive(true);
        }
    }

    IEnumerator Shoot()
    {

        //audioSource.pitch = Random.Range(1 - soundPitchDispersion, 1 + soundPitchDispersion);

        //audioSource.PlayOneShot(audioSource.clip);

        while(isShooting)
        {
            ShootProjectile();
            yield return new WaitForSeconds(ShootPeriod);
        }

    }
}
