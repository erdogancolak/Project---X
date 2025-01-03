using UnityEngine;

public class NailProjectile : MonoBehaviour
{
    [Header("Settings")]
    public int nailDamage;

    public string playerTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag))
        {
            collision.GetComponent<PlayerController>().TakeHit(nailDamage);
            Debug.Log(playerTag + " Vuruldu! ve Kalan caný = " + collision.GetComponent<PlayerController>().currentHp);
            Destroy(this.gameObject);
        }
    }
}
