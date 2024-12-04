using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    public static PlayerAttack Instance { get; private set; }

    [SerializeField] private float swordDamage;
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

    public void Attack()
    {
        if (canAttack && _timer > attackCooldown)
        {
             _timer = 0f;
             animator.SetBool("SwordAttack", true);
             Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);
             foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<EnemyController>().TakeHit(swordDamage);
                }
             StartCoroutine(swordCooldown());
        }
    }

    IEnumerator swordCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("SwordAttack", false);
    }
}
