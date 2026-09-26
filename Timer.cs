using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reusable coroutine-based timer utility, ported from the Love2D Timer module.
/// Must be driven by a MonoBehaviour (Unity has no free-standing coroutines),
/// so this is implemented as a component you attach to any GameObject (e.g. a
/// dedicated "Managers" object), or you can call TimerRunner.Instance to get
/// an auto-created singleton runner.
/// </summary>
public class Timer : MonoBehaviour
{
    private static Timer _instance;
    public static Timer Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("TimerRunner");
                _instance = go.AddComponent<Timer>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private readonly List<TimerHandle> _activeTimers = new List<TimerHandle>();

    public class TimerHandle
    {
        public Coroutine Coroutine;
        public bool Cancelled;
    }

    /// <summary>
    /// Runs callback once after `delay` seconds.
    /// </summary>
    public TimerHandle After(float delay, Action callback)
    {
        var handle = new TimerHandle();
        handle.Coroutine = StartCoroutine(AfterRoutine(delay, callback, handle));
        _activeTimers.Add(handle);
        return handle;
    }

    /// <summary>
    /// Runs callback every `interval` seconds, optionally limited to `repeatCount` times (0 = infinite).
    /// </summary>
    public TimerHandle Every(float interval, Action callback, int repeatCount = 0)
    {
        var handle = new TimerHandle();
        handle.Coroutine = StartCoroutine(EveryRoutine(interval, callback, repeatCount, handle));
        _activeTimers.Add(handle);
        return handle;
    }

    public void Cancel(TimerHandle handle)
    {
        if (handle == null || handle.Cancelled) return;
        handle.Cancelled = true;
        if (handle.Coroutine != null) StopCoroutine(handle.Coroutine);
        _activeTimers.Remove(handle);
    }

    private IEnumerator AfterRoutine(float delay, Action callback, TimerHandle handle)
    {
        yield return new WaitForSeconds(delay);
        if (!handle.Cancelled) callback?.Invoke();
        _activeTimers.Remove(handle);
    }

    private IEnumerator EveryRoutine(float interval, Action callback, int repeatCount, TimerHandle handle)
    {
        int count = 0;
        while (!handle.Cancelled && (repeatCount <= 0 || count < repeatCount))
        {
            yield return new WaitForSeconds(interval);
            if (handle.Cancelled) yield break;
            callback?.Invoke();
            count++;
        }
        _activeTimers.Remove(handle);
    }
}
