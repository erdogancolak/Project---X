using UnityEngine;

public class WeaponChangeController : MonoBehaviour
{
    public void Stick()
    {
        PlayerAttack.Instance.attackAnim = "StickAttack";
        PlayerAttack.Instance.attack2Anim = "StickAttack2";
    }
    public void NailStick()
    {
        PlayerAttack.Instance.attackAnim = "NailStickAttack";
        PlayerAttack.Instance.attack2Anim = "NailStickAttack2";
    }
}
