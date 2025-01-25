using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public float damage, range, projectileSpeed = float.PositiveInfinity, spread, roundsPerMinute;

    [SerializeField]
    public int multishot = 1;

    public float ShootPeriod => 60 / roundsPerMinute;
    public float ProjectileLifeTime => range / projectileSpeed;

    [SerializeField]
    Transform barrelTransform;

    [SerializeField]
    AudioSource audioSource;

    private float soundPitchDispersion = 0.05f;

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
        turretDriver.Update();
    }

    bool isShooting = false;
    public void Trigger()
    {
        if (!isShooting)
            StartCoroutine(Shoot());
    }

    public bool IsInRange(Vector2 position)
    {
        return (position - (Vector2)barrelTransform.position).magnitude < range;
    }

    public void UseTargetingSystem(Vector2 position)
    {
        turretDriver.LookAt(targetingSystem.UpdateTargetPosition(Time.deltaTime, position, this));
    }

    void ShootProjectile()
    {
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        audioSource.pitch = Random.Range(1 - soundPitchDispersion, 1 + soundPitchDispersion);

        audioSource.PlayOneShot(audioSource.clip);

        for (int i = 0; i < multishot; i++)
            ShootProjectile();

        yield return new WaitForSeconds(ShootPeriod);
        isShooting = false;
    }
}
