using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float maxHp;
    [HideInInspector] public float currentHp;

    void Start()
    {
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
            Debug.Log("Player Dead");
        }
    }

    public void TakeHit(float damage)
    {
        currentHp -= damage;
    }
}
