using UnityEngine;
using DG.Tweening;
public class Obstacle : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private GameObject destructableVersion = default;
    private float health = 3;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        health = maxHealth;
    }
    public bool OnHit(float gunDamage)
    {
        health -= gunDamage;
        transform.DOPunchScale(Vector3.one * 0.95f, 0.4f, 0, 0);
        meshRenderer.material.SetFloat("BlendAmount", 1f - (health / maxHealth));
        if (health <= 0f)
        {
            GameObject destructed =  Instantiate(destructableVersion, transform.position, Quaternion.identity);
            foreach (Rigidbody rb in destructed.GetComponentsInChildren<Rigidbody>())
            {
                Vector3 force = ((rb.transform.position - transform.position).normalized) * 300f + Random.onUnitSphere * 200f;
                rb.AddForce(force);
            }
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
