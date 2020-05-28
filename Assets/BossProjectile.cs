using UnityEngine;

public class BossProjectile : Projectile
{
	public PlayerHealth Health;

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		Health.OnDamage(Damage);
	}
}

