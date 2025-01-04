using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int projectileDamage;

    [SerializeField] private string playerTag;
    private void Start()
    {
        Destroy(this.gameObject, 10f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag))
        {
            collision.GetComponent<PlayerController>().TakeHit(projectileDamage);
            Debug.Log(playerTag + " Vuruldu! ve Kalan caný = " + collision.GetComponent<PlayerController>().currentHp);
            Destroy(this.gameObject);
        }
    }
}
