using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    Animator animator;

    [Space]

    [Header("Settings")]

    [SerializeField] private float maxHp;
    [HideInInspector] public float currentHp;

    void Start()
    {
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
            Debug.Log("Dead");
        }
    }
    public void TakeHit(float damage)
    {
        animator.SetTrigger("TakeHit");
        currentHp -= damage;
    }
}
