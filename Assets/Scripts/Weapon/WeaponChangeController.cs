using UnityEngine;

public class WeaponChangeController : MonoBehaviour
{
    public void Stick()
    {
        PlayerAttack.Instance.attackAnim = "StickAttack";
        PlayerAttack.Instance.attack2Anim = "StickAttack2";

        PlayerAttack.Instance.isRangedWeapon = false;
    }
    public void NailStick()
    {
        PlayerAttack.Instance.attackAnim = "NailStickAttack";
        PlayerAttack.Instance.attack2Anim = "NailStickAttack2";

        PlayerAttack.Instance.isRangedWeapon = false;
    }

    public void RangedWeapon()
    {
        PlayerAttack.Instance.attackAnim = "RangedAttack1";

        PlayerAttack.Instance.isRangedWeapon = true;
    }
}
