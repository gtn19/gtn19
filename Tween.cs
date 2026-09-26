using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Système de tween basé sur des coroutines liées au deltaTime :
/// fait évoluer une valeur de startValue à endValue sur une durée donnée,
/// avec une easing function optionnelle pour l'accélération/décélération.
/// Utilisation : Tween.Instance.Float(0f, 10f, 1.5f, val => transform.position = new Vector3(val, 0, 0));
/// </summary>
public class Tween : MonoBehaviour
{
    private static Tween _instance;
    public static Tween Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("TweenRunner");
                _instance = go.AddComponent<Tween>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public class TweenHandle
    {
        public Coroutine Coroutine;
        public bool Cancelled;
    }

    // Easing functions courantes. t est toujours normalisé entre 0 et 1.
    public static class Ease
    {
        public static float Linear(float t) => t;
        public static float EaseInQuad(float t) => t * t;
        public static float EaseOutQuad(float t) => t * (2f - t);
        public static float EaseInOutQuad(float t) =>
            t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
        public static float EaseOutBack(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;
            float x = t - 1f;
            return 1f + c3 * x * x * x + c1 * x * x;
        }
    }

    /// <summary>
    /// Anime un float de startValue à endValue sur duration secondes.
    /// onUpdate est appelé chaque frame avec la valeur courante.
    /// onComplete (optionnel) est appelé une fois l'animation terminée.
    /// easing (optionnel) contrôle la courbe de progression, linéaire par défaut.
    /// </summary>
    public TweenHandle Float(float startValue, float endValue, float duration,
        Action<float> onUpdate, Action onComplete = null, Func<float, float> easing = null)
    {
        var handle = new TweenHandle();
        handle.Coroutine = StartCoroutine(
            FloatRoutine(startValue, endValue, duration, onUpdate, onComplete, easing ?? Ease.Linear, handle));
        return handle;
    }

    /// <summary>
    /// Version Vector3 — pratique pour position/scale.
    /// </summary>
    public TweenHandle Vector3(Vector3 startValue, Vector3 endValue, float duration,
        Action<Vector3> onUpdate, Action onComplete = null, Func<float, float> easing = null)
    {
        var handle = new TweenHandle();
        handle.Coroutine = StartCoroutine(
            Vector3Routine(startValue, endValue, duration, onUpdate, onComplete, easing ?? Ease.Linear, handle));
        return handle;
    }

    public void Cancel(TweenHandle handle)
    {
        if (handle == null || handle.Cancelled) return;
        handle.Cancelled = true;
        if (handle.Coroutine != null) StopCoroutine(handle.Coroutine);
    }

    private IEnumerator FloatRoutine(float start, float end, float duration,
        Action<float> onUpdate, Action onComplete, Func<float, float> easing, TweenHandle handle)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (handle.Cancelled) yield break;

            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float easedT = easing(t);
            onUpdate?.Invoke(Mathf.LerpUnclamped(start, end, easedT));

            yield return null;
        }

        onUpdate?.Invoke(end);
        onComplete?.Invoke();
    }

    private IEnumerator Vector3Routine(Vector3 start, Vector3 end, float duration,
        Action<Vector3> onUpdate, Action onComplete, Func<float, float> easing, TweenHandle handle)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (handle.Cancelled) yield break;

            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float easedT = easing(t);
            onUpdate?.Invoke(UnityEngine.Vector3.LerpUnclamped(start, end, easedT));

            yield return null;
        }

        onUpdate?.Invoke(end);
        onComplete?.Invoke();
    }
}
