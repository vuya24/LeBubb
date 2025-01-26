using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public float damage, range, projectileSpeed = 5, spread;

    public float ShootPeriod = 1;
    public float ProjectileLifeTime => range / projectileSpeed;

    [SerializeField]
    Transform barrelTransform;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    private Bullet projectile;

    private float soundPitchDispersion = 0.05f;

    bool isShooting = false;

    [SerializeField]
    TurretDriver turretDriver;

    TargetingSystem targetingSystem;

    private void Awake()
    {

    }

    private void Update()
    {
        var player = PlayerController.singleton;

        if (player == null)
            return;

        turretDriver.LookAt(player.transform.position);
        turretDriver.Update();

        if(IsInRange(player.transform.position) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(Shoot());
        }
    }

    public bool IsInRange(Vector2 position)
    {
        return (position - (Vector2)transform.position).magnitude < range;
    }

    IEnumerator Shoot()
    {
        audioSource.pitch = Random.Range(1 - soundPitchDispersion, 1 + soundPitchDispersion);
        audioSource.PlayOneShot(audioSource.clip);

        var bubble = Instantiate(projectile, transform.position, transform.rotation);
        bubble.speed = projectileSpeed;
        Destroy(bubble.gameObject, ProjectileLifeTime);
        yield return new WaitForSeconds(ShootPeriod);

        isShooting = false;
    }
}
