[CreateAssetMenu(fileName = "NewRune", menuName = "Spells/Rune")]
public class Rune : ScriptableObject
{
    [Header("Start Rune (seulement si la rune est la première)")]

    public bool isStartRune;
    public GameObject startRunePrefab;

    [Header("Stats (optionnel, affecte le sort global)")]
    public float manaCostModifier;
    public float cooldownModifier;

    [Header("Mouvement (optionnel)")]
    public bool hasMovement;
    public Vector3 movementOffset; // ou une direction + distance, selon ta logique
    public float movementSpeed;

    [Header("Mini-sort imbriqué (optionnel)")]
    public GameObject nestedShapePrefab; // un SpellShape, comme "3 spikes de glace"

    [Header("Effets (optionnel)")]
    public List<SpellEffect> effects;
}
