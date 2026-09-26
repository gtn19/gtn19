using UnityEngine;
public abstract class SpellShape : MonoBehaviour
{
    public abstract void Execute(GameObject caster, GameObject target, List<SpellEffect> effects);
}
