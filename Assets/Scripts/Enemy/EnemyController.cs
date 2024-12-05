using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    [SerializeField] private float maxHp;
    private float currentHp;

    void Start()
    {
        animator = GetComponent<Animator>();

        currentHp = maxHp;
    }

    void Update()
    {
        isDead();
    }

    private async void isDead()
    {
        if(currentHp <= 0)
        {
            animator.SetTrigger("isDie");
            this.enabled = false;
            EnemyFollowPlayer.Instance.enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            await DestroyObject();
        }
    }

    public void TakeHit(float TakeDamage)
    {
        animator.SetTrigger("isHit");
        currentHp -= TakeDamage;
    }

    private async Task DestroyObject()
    {
        await Task.Delay(10000);
        Destroy(this.gameObject);
    }
}
