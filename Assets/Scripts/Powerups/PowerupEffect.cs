using UnityEngine;

/// <summary>
/// Abstract class to create and define new powerups.
/// How to use :
/// - Create a new class
/// - Derive this class from PowerupEffect
/// - Define the apply method
/// </summary>
public abstract class PowerupEffect : ScriptableObject
{
    public abstract void Apply( GameObject target );
}
