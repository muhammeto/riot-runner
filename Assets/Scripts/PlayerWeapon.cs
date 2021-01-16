using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private AmmoPack ammoPack;
    [SerializeField] private ParticleSystem shells, muzzle;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ammoPack.CanShoot)
        {
            animator.SetBool("Firing", true);
            shells.Play();
            muzzle.Play();
            ammoPack.OnBulletShoot();
        }
        else
        {
            animator.SetBool("Firing", false);
        }
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
