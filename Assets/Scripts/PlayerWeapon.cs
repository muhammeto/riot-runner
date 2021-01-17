using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private AmmoPack ammoPack;
    [SerializeField] private ParticleSystem shells, muzzle;
    [SerializeField] private Transform gunEnd;

    [Header("Gun Settings")]
    [SerializeField] private int gunDamage = 1;
    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float weaponRange = 50f;

    private Animator animator;
    private float nextFire;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.3f);
    private bool isFound = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnDoubleTap()
    {
        if (Time.time > nextFire && ammoPack.CanShoot)
        {
            StartCoroutine(ShotEffect());
            ammoPack.OnBulletShoot();
            nextFire = Time.time + fireRate;
            RaycastHit hit;
            if (Physics.Raycast(gunEnd.position, gunEnd.forward, out hit, weaponRange))
            {
                Obstacle obstacle = hit.collider.GetComponent<Obstacle>();
                if (obstacle != null)
                {
                    if (!isFound)
                    {
                        isFound = true;
                        animator.SetBool("Firing", true);
                    }
                    bool didDestroyed = obstacle.OnHit(gunDamage);
                    if(didDestroyed && isFound)
                    {
                        isFound = false;
                        animator.SetBool("Firing", false);
                    }
                }
            }
        }
    }
    private IEnumerator ShotEffect()
    {
        // gunAudio.Play();
        muzzle.Play();
        shells.Play();
        yield return shotDuration;
        muzzle.Stop();
        shells.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && ammoPack.CanPickup)
        {
            Destroy(other.gameObject);
            ammoPack.OnBulletPickup();
        }
    }
}
