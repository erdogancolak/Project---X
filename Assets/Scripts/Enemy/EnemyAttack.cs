using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public static EnemyAttack instance;
    [Header("References")]

    [SerializeField] private Transform AttackPoint;

    [Space]

    [Header("Setting")]

    [SerializeField] private int SwordDamage;

    [SerializeField] private float AttackRange;

    [SerializeField] private LayerMask playerLayer;
    
    private void Awake()
    {
        instance = this;
    }

    public void GiveDamage()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, playerLayer);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeHit(SwordDamage);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    //}
}
