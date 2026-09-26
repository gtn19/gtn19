using UnityEngine;
public class DamageEffect : SpellEffect
{
    public float amount;

    public override void Apply(GameObject caster, GameObject target)
    {
        if (caster && target)
        {
            target.TakeDamage(amount);
        }
    }
}
