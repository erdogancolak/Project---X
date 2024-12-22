using UnityEngine;
using DG.Tweening;
using System.Collections;
public class EnemyFollowPlayer : MonoBehaviour
{
    public static EnemyFollowPlayer Instance { get; private set; }

    private GameObject player;
    Rigidbody2D rb;
    Animator animator;
    private bool isChasing;
    private bool canAttack = false;
    [SerializeField] private float detectRange;
    [SerializeField] private float stopRange;
    [SerializeField] private float speed;
    private float distance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= detectRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing && distance > stopRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasing();
        }

        FlipTowardsPlayer();

        Attack();

    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position,new Vector2(player.transform.position.x,transform.position.y), speed * Time.deltaTime);
        animator.SetBool("isRun", true);
    }
    private void StopChasing()
    {
        animator.SetBool("isRun", false);
    }
    void FlipTowardsPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void Attack()
    {
        if (canAttack)
            return;

        if(distance <= stopRange && EnemyController.instance.isKnockbacked == false)
        {
            StopAndAttack();
        }
    }

    void StopAndAttack()
    {
        canAttack = true;
        
        StartCoroutine(AttackIE());
    }

    IEnumerator AttackIE()
    {
        yield return new WaitForSeconds(.2f);
        if(EnemyController.instance.isKnockbacked == false)
        {
            animator.SetTrigger("Attack");
            EnemyAttack.instance.GiveDamage();
        }
        yield return new WaitForSeconds(.7f);
        canAttack = false;
    }

    
}
