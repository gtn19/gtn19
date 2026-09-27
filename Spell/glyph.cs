[CreateAssetMenu(fileName = "NewGlyph", menuName = "Spells/Glyph")]
public class Glyph : ScriptableObject
{
    [Header("Mouvement")]
    public Vector3 direction;
    public float movementDistance;

    [Header("Effets déclenchés au hit")]
    public List<SpellEffect> hitEffects;
}
