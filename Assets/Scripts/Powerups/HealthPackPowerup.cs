using UnityEngine;

[CreateAssetMenu(menuName = "HIYOI/Powerups/New Health Powerup", fileName = "New Health Powerup")]
public class HealthPackPowerup : PowerupEffect
{
    [SerializeField] private float amount;

    public override void Apply( GameObject target ) {
        if (target.TryGetComponent(out Health health)) {
            health.Heal(amount);
        }
    }
}
