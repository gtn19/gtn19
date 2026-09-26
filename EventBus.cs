using System;
using System.Collections.Generic;

/// <summary>
/// Utilisation
///
///void OnEnable() => EventBus.Subscribe("PlayerDied", HandlePlayerDied);
///void OnDisable() => EventBus.Unsubscribe("PlayerDied", HandlePlayerDied);
///void HandlePlayerDied() => Debug.Log("Game over !");
// Un autre script qui déclenche l'événement
///EventBus.Publish("PlayerDied");
/// </summary>


public static class EventBus
{
    private static Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();

    public static void Subscribe(string eventName, Action callback)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] += callback;
        }
        else
        {
            eventDictionary.Add(eventName, callback);
        }
    }

    public static void UnSbuscribe(string eventName, Action callback)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= callback;
        }
    }

    public static void Publish(string eventName)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName]();
        }
    }
}
