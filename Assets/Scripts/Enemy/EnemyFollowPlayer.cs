using UnityEngine;
using DG.Tweening;
public class EnemyFollowPlayer : MonoBehaviour
{
    public static EnemyFollowPlayer Instance { get; private set; }

    private GameObject player;
    Animator animator;
    private bool isChasing;
    [SerializeField] private float detectRange;
    [SerializeField] private float stopRange;
    [SerializeField] private float speed;
    void Start()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance <= detectRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
        if(isChasing && distance > stopRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasing();
        }
        FlipTowardsPlayer();
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position,new Vector2(player.transform.position.x,transform.position.y), speed);
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
}
