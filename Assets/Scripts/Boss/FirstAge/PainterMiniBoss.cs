using System.Collections;
using UnityEngine;

public class PainterMiniBoss : MonoBehaviour
{
    [Header("References")]
    Animator animator;

    private EnemyAttack enemyAttack;

    private EnemyFollowPlayer enemyFollowPlayer;

    public Transform rangedWeaponPoint;

    public GameObject rangedBullet;

    [Space]

    [Header("Settings")]

    private float timer = 0;

    public int rangedAttackRate;

    public float bulletSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyFollowPlayer = GetComponent<EnemyFollowPlayer>();
        timer = 4;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector2.Distance(EnemyFollowPlayer.Instance.player.transform.position, transform.position);
        if (timer >= rangedAttackRate && distance >= 5)
        {
            timer = 0;
            StartCoroutine(RangedAttackHold());
        }
    }
    IEnumerator RangedAttackHold()
    {
        enemyAttack.enabled = false;
        enemyFollowPlayer.enabled = false;
        animator.SetTrigger("RangeAttack");
        yield return new WaitForSeconds(.3f);
        RangedAttack();
    }

    private void RangedAttack()
    {
        Debug.Log("Çivi Atýldý ");

        Vector3 nailGoesPos = new Vector3(enemyFollowPlayer.player.transform.position.x, enemyFollowPlayer.player.transform.position.y + .5f, enemyFollowPlayer.player.transform.position.z);
        Vector2 direction = (nailGoesPos - rangedWeaponPoint.position).normalized;

        

        GameObject createdNail = Instantiate(rangedBullet, rangedWeaponPoint.position, Quaternion.identity);
        Rigidbody2D rb = createdNail.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;

        StartCoroutine(EnableEnemyAttack());
    }
    IEnumerator EnableEnemyAttack()
    {
        yield return new WaitForSeconds(.2f);
        enemyFollowPlayer.enabled = true;
        enemyAttack.enabled = true;
    }
}
