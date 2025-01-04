using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    public static PlayerAttack Instance { get; private set; }

    [Header("References")]

    [SerializeField] private Transform AttackPoint;

    [SerializeField] private Transform rangedWeaponPoint;

    [SerializeField] private GameObject projectilePrefab;

    [Space]

    [Header("Settings")]

    [SerializeField] private float swordDamage;
    [SerializeField] private float swordDamage2;
    
    [SerializeField] private float AttackRange;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackCooldown2;
    [SerializeField] private float rangeAttackCooldown;

    [SerializeField] private LayerMask enemyLayers;

    [HideInInspector] public bool canAttack;
   
    private float _timer;

    public string attackAnim = "StickAttack";
    public string attack2Anim = "StickAttack2";

    [HideInInspector] public bool isRangedWeapon = false;

    [SerializeField] private float projectileSpeed;

    private Vector2 direction = Vector2.right;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
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
        if(!isRangedWeapon)
        {
            closeAttack();
        }
        else
        {
            rangedAttack();
        }
    }

    private void closeAttack()
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
    private void rangedAttack()
    {
        if(PlayerMovement.isRight) direction = Vector2.right;
        if(!PlayerMovement.isRight) direction = Vector2.left;

        if(canAttack && _timer > rangeAttackCooldown )
        {
            _timer = 0f;

            PlayerMovement.Instance.canMove = false;

            GameObject createdProjectile = Instantiate(projectilePrefab, rangedWeaponPoint.position, Quaternion.identity);

            Rigidbody2D projectileRb = createdProjectile.GetComponent<Rigidbody2D>();
            projectileRb.linearVelocity = direction * projectileSpeed;

            createdProjectile.transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);

            animator.SetBool(attackAnim, true);
            StartCoroutine(swordCooldown(attackAnim));
        }
    }
    public void Attack2()
    {
        if (!isRangedWeapon)
        {
            closeAttack2();
        }
    }
    private void closeAttack2()
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

    IEnumerator swordCooldown(string BoolName)
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(BoolName, false);
        yield return new WaitForSeconds(0.3f);
        PlayerMovement.Instance.canMove = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
