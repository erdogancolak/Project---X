using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int projectileDamage;

    [SerializeField] private string EnemyTag;

    private void Start()
    {
        Destroy(this.gameObject , 10f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(EnemyTag))
        {
            collision.GetComponent<EnemyController>().TakeHit(projectileDamage);
            Debug.Log(EnemyTag + " Vuruldu! ve Kalan caný = " + collision.GetComponent<EnemyController>().currentHp);
            Destroy(this.gameObject);
        }
    }
}
