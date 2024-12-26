using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    public static PlayerAttack Instance { get; private set; }

    [SerializeField] private float swordDamage;
    [SerializeField] private float swordDamage2;
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private float AttackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackCooldown2;
    [SerializeField] private LayerMask enemyLayers;
    [HideInInspector] public bool canAttack;
    private float _timer;

    public string attackAnim = "StickAttack";
    public string attack2Anim = "StickAttack2";
    void Start()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        canAttack = true;
    }

    void Update()
    {
        _timer += Time.deltaTime;
    }

    private void GiveDamage(float damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeHit(damage);
            if(enemy.GetComponent<EnemyController>().currentHp > 0)
            {
                enemy.GetComponent<EnemyController>().Knockback(transform.position);
            }
        }
    }
    public void Attack()
    {
        if (canAttack && _timer > attackCooldown)
        {
            PlayerMovement.Instance.canMove = false;
            _timer = 0f;
            animator.SetBool(attackAnim, true);
            GiveDamage(swordDamage);
            StartCoroutine(swordCooldown(attackAnim));
        }
    }

    IEnumerator swordCooldown(string BoolName)
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(BoolName, false);
        yield return new WaitForSeconds(0.3f);
        PlayerMovement.Instance.canMove = true;
    }

    public void Attack2()
    {
        
        if (canAttack && _timer > attackCooldown2)
        {
            _timer = 0f;
            PlayerMovement.Instance.canMove = false;
            animator.SetBool(attack2Anim, true);
            GiveDamage(swordDamage2);
            StartCoroutine(swordCooldown(attack2Anim));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
