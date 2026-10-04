public class SpellCaster : MonoBehaviour
{
    public void Cast(Spell spell, GameObject caster, GameObject target)
    {
        GameObject spellInstance;
        spellInstance = Instantiate(spell.visualPrefab);
        spellInstance.transform.position = caster.transform.position;

        glyphSequenceRunner seqRunner = spellInstance.AddComponent<glyphSequenceRunner>();
        this.StartCoroutine(seqRunner.ExecuteSequence(spell.glyphs, caster, spellInstance));
    }
}
