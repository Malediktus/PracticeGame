using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HIYOI/Events/New GameEvent", fileName = "New GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners;

    public void Add( GameEventListener listener ) => _listeners.Add(listener);
    public void Remove( GameEventListener listener ) => _listeners.Remove(listener);

    public void Invoke() {
        foreach (var listener in _listeners) {
            listener.Invoke();
        }
    }
}
