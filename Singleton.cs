using UnityEngine;

/// <summary>
/// Classe de base générique : héritez-en pour transformer n'importe quel
/// MonoBehaviour en singleton accessible via MaClasse.Instance
/// Exemple d'utilisation : public class AudioManager : Singleton<AudioManager> { ... }
/// </summary>
/// <typeparam name="T">
/// Le type de la classe qui hérite (ex: AudioManager).
/// La contrainte "where T : MonoBehaviour" est obligatoire car on utilise
/// des méthodes Unity comme FindObjectOfType<T>() et AddComponent<T>(),
/// qui exigent que T soit un MonoBehaviour.
/// </typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Variable statique = une seule copie partagée par TOUTE la classe,
    // peu importe combien d'objets existent dans la scène.
    private static T _instance;

    // Flag pour éviter de recréer un singleton pendant la fermeture du jeu
    // (Unity peut appeler OnDestroy() sur tous les objets au moment de quitter,
    // et si un autre script essaie d'accéder à Instance à ce moment-là,
    // ça recréerait un objet fantôme juste avant la fermeture).
    private static bool _applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance de {typeof(T)} demandée alors que l'application se ferme.");
                return null;
            }

            if (_instance == null)
            {
                // Cherche d'abord si un objet du bon type existe déjà dans la scène
                // (utile si tu as placé le manager à la main dans l'éditeur)
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    // Sinon on en crée un automatiquement
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    // Appelée automatiquement par Unity quand l'objet est créé/activé.
    // "virtual" = les classes filles peuvent la redéfinir avec "override"
    // si elles ont besoin d'une init spécifique (ex: charger des sons pour AudioManager).
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            // Optionnel : décommente si CE manager doit survivre aux changements de scène.
            // Tous les singletons n'en ont pas besoin (ex: un manager de menu).
            // DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            // Un deuxième exemplaire a été créé par erreur (ex: présent dans 2 scènes) : on le détruit.
            Destroy(gameObject);
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }
}
