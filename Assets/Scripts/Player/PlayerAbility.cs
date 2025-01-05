using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public static PlayerAbility Instance;
    [Header("References")]
    [SerializeField] private Transform abilityRange;

    [Space]

    [Header("Settings")]
    private float _timer;

    [HideInInspector] public string Ability = "Bagirma";
    [SerializeField] private float AbilityRadius;
    [SerializeField] public string enemyTag;

    [SerializeField] private float bagirmaSkillCooldown;
    [SerializeField] private float bagirmaKnockbackPower;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }
    public void UseAbility()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(abilityRange.position, AbilityRadius);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                switch(Ability)
                {
                    case "Bagirma":
                        BagirmaSkill(enemy.gameObject);
                        break;
                }
            }
        }
    }
    public void UseAbility2()
    {
        Debug.Log("Second Ability Used");
    }
    private void BagirmaSkill(GameObject enemyGameObject)
    {
        if(_timer >= bagirmaSkillCooldown)
        {
            _timer = 0;
            Rigidbody2D rb = enemyGameObject.GetComponent<Rigidbody2D>();
            EnemyController enemyController = enemyGameObject.GetComponent<EnemyController>();
            if (rb != null)
            {
                enemyController.Knockback(transform.position, bagirmaKnockbackPower);
            }

            if (enemyController != null)
            {
                enemyController.Stun(1);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(abilityRange.position, AbilityRadius);
    }
}
