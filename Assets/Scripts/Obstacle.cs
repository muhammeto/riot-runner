using UnityEngine;
using DG.Tweening;
public class Obstacle : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    private int health = 3;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        health = maxHealth;
    }
    [ContextMenu("OnHit")]
    public bool OnHit()
    {
        health--;
        transform.DOPunchScale(Vector3.one * 0.95f, 0.4f, 0, 0);
        meshRenderer.material.SetFloat("BlendAmount", (1f - ((float)health / maxHealth)));

        if (health == 0)
        {
            // Destroy
            // cam shake
            return true;
        }
        return false;
    }
}
