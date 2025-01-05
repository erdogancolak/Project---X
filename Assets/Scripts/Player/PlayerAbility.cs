using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public static PlayerAbility Instance;
    private void Awake()
    {
        Instance = this;
    }
    
    public void UseAbility()
    {
        Debug.Log("First Ability Used");
    }
    public void UseAbility2()
    {
        Debug.Log("Second Ability Used");
    }
}
