public enum SpellLaunchPattern { Normal, OnSelf, OrbitPlayer, RainingCloud }

[CreateAssetMenu(fileName = "NewSpell", menuName = "Spells/Spell")]
public class spell : ScriptableObject
{
    public string spellName;
    public GameObject visualPrefab; // fixe, plus de changement en cours de séquence
    public SpellLaunchPattern launchPattern;
    public List<Glyph> glyphs;
}
