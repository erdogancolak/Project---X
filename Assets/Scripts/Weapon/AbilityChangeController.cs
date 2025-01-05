using UnityEngine;

public class AbilityChangeController : MonoBehaviour
{
    public void SetAbility(string abilityName)
    {
        PlayerAbility.Instance.Ability = abilityName;
    }
}
