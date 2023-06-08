using UnityEngine;

/// <summary>
/// Update the value of the Position ScriptableObject. 
/// Needs to be added to the script you want to take the position from (e.g. player).
/// </summary>
public class PositionWriter : MonoBehaviour
{
    [SerializeField] private Position _position;

    void Update() => _position.Value = transform.position;
}
