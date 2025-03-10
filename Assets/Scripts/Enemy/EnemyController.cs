using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    [Header("References")]

    Rigidbody2D rb;
    Animator animator;

    [Space]

    [Header("Settings")]

    [SerializeField] private float maxHp;
    [HideInInspector] public float currentHp;

    [SerializeField] private float waitDestroyFloat;

    

    [HideInInspector] public bool isKnockbacked = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHp = maxHp;
    }

    void Update()
    {
        isDead();
    }

    private void isDead()
    {
        if(currentHp <= 0)
        {
            animator.SetTrigger("isDie");
            this.enabled = false;
            if (EnemyFollowPlayer.Instance != null)
            {
                Destroy(EnemyFollowPlayer.Instance);
            }
            GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(DestroyObject());
        }
    }

    public void TakeHit(float TakeDamage)
    {
        animator.SetTrigger("isHit");
        currentHp -= TakeDamage;
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(waitDestroyFloat);
        Destroy(this.gameObject);
    }

    public void Knockback(Vector2 playerPosition,float knockBackPower)
    {
        if (rb != null)
        {
            isKnockbacked = true;
            rb.linearVelocity = Vector2.zero; 
            Vector2 knockbackDirection = new Vector2(transform.position.x - playerPosition.x, 0f).normalized;

            rb.AddForce(knockbackDirection * knockBackPower, ForceMode2D.Impulse);

            StartCoroutine(DisableFollowForKnockback());
        }
    }

    private IEnumerator DisableFollowForKnockback()
    {
        yield return new WaitForSeconds(.5f);

        rb.linearVelocity = Vector2.zero;

        if (EnemyFollowPlayer.Instance != null)
        {
            EnemyFollowPlayer.Instance.enabled = false;

            yield return new WaitForSeconds(0.27f);

            if (EnemyFollowPlayer.Instance != null) 
            {
                EnemyFollowPlayer.Instance.enabled = true;
                isKnockbacked = false;
            }
        }
    }

    public IEnumerator Stun(float duration)
    {
        float speedHoldFloat = EnemyFollowPlayer.Instance.speed;
        EnemyFollowPlayer.Instance.speed = 0f;
        Debug.Log("Enemy Stunned");
        yield return new WaitForSeconds(duration);
        EnemyFollowPlayer.Instance.speed = speedHoldFloat;
        Debug.Log("Enemy Stun Finish");
    }
}
