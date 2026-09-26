using UnityEngine;
public class HealEffect : SpellEffect
{
    public float amount;

    public override void Apply(GameObject caster, GameObject target)
    {
        if (caster && target)
        {
            target.RestoreHP(amount);
        }
    }
}
