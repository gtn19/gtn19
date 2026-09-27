public class SpellHitDetector : MonoBehaviour
{
    public Action<GameObject> onHit;

    private void OnTriggerEnter(Collider other)
    {
        onHit?.Invoke(other.gameObject);
    }
}
