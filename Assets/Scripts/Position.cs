using UnityEngine;

/// <summary>
/// Here, we use scriptable objects as a "global variable" meaning every script containing a reference to this object
/// is going to be able to access data without having to connect to a specific gameobject or use expensive methods like
/// GameObject.Find that looks through all objects in a scene which can slow down significantly the frame rate of a game.
/// </summary>
[CreateAssetMenu(menuName = "HIYOI/Global Variables/Position")]
public class Position : ScriptableObject
{
    public Vector3 Value { get; set; }
}
