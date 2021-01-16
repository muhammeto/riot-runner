using UnityEngine;
using DG.Tweening;

public class AmmoPack : MonoBehaviour
{
    public int currentBulletAmount = 0;

    public bool CanPickup => (currentBulletAmount + 1 <= transform.childCount);
    public bool CanShoot => (currentBulletAmount > 0);
    public void OnBulletPickup()
    {
        transform.GetChild(currentBulletAmount).gameObject.SetActive(true);
        currentBulletAmount++;
    }

    public void OnBulletShoot()
    {
        currentBulletAmount--;
        transform.GetChild(currentBulletAmount).gameObject.SetActive(false);
    }
}
