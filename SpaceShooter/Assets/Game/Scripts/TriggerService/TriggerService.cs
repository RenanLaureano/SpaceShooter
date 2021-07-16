using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerService : MonoBehaviour, ITriggerService
{
    private Dictionary<TriggerType, List<System.Action<TriggerType, object>>> EventListeners;

    public static TriggerService Instance;

    private void Awake()
    {
        EventListeners = new Dictionary<TriggerType, List<System.Action<TriggerType, object>>>();

        if(Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddListener(TriggerType type, System.Action<TriggerType, object> call)
    {
        if (EventListeners.ContainsKey(type) == false)
            EventListeners[type] = new List<System.Action<TriggerType, object>>();
        EventListeners[type].Add(call);
    }
    
    public void RemoveListener(TriggerType type, System.Action<TriggerType, object> call)
    {
        if (EventListeners.ContainsKey(type))
        {
            var listeners = EventListeners[type];
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                if (listeners[i] == call)
                    listeners.RemoveAt(i);
            }
        }
    }

    public void FireEvent(TriggerType type, object param)
    {
        if (EventListeners.ContainsKey(type))
        {
            var listeners = EventListeners[type];
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(type, param);
            }
        }
    }

    public void ClearListeners()
    {
        EventListeners.Clear();
    }
}