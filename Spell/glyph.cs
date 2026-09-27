public enum GlyphModifierType { Movement, Shape }

[CreateAssetMenu(fileName = "NewGlyph", menuName = "Spells/Glyph")]
public class Glyph : ScriptableObject
{
    [Header("Modificateur")]
    public GlyphModifierType modifierType;

    public Vector3 direction;
    public float movementDistance;
    public float movementDuration;

    public GameObject shapePrefab;
}
