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
    [SerializeField] private LayerMask enemyLayers;
    [HideInInspector] public bool canAttack;
    private float _timer;
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

    private void GiveDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeHit(swordDamage);
        }
    }
    public void Attack()
    {
        if (canAttack && _timer > attackCooldown)
        {
             _timer = 0f;
             animator.SetBool("SwordAttack", true);
             GiveDamage();
             StartCoroutine(swordCooldown("SwordAttack"));
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
        
        if (canAttack && _timer > attackCooldown)
        {
            _timer = 0f;
            PlayerMovement.Instance.canMove = false;
            animator.SetBool("SwordAttack2", true);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyController>().TakeHit(swordDamage2);
            }
            StartCoroutine(swordCooldown("SwordAttack2"));
        }
    }
}
