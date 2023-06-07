using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PowerupPickup : MonoBehaviour
{
    [SerializeField] private PowerupEffect powerupEffect;

    private void OnTriggerEnter2D( Collider2D collision ) {
        // This is a check to test if the entity can pickup the powerup by using an interface.
        // Quick Tip : using Unity's tags is very bad practice.
        if (collision.TryGetComponent(out IPowerable powerable)) {
            Destroy(gameObject);
            powerupEffect.Apply(collision.gameObject);
        }
    }
}
