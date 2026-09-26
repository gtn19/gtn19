using UnityEngine;

public abstract class SpellEffect : ScriptableObject
{
    public abstract void Apply(GameObject caster, GameObject target);
}
