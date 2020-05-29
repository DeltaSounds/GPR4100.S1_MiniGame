using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _playerRigid;
	[SerializeField] private EnemyMovement _eMovement;
	[Space]
	[SerializeField] float Damage = 5;
	[SerializeField] float Knockback = 2;


	private void Awake()
	{
		_eMovement = GetComponent<EnemyMovement>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			IDamageable damageable = collision.GetComponent<IDamageable>();

			if (damageable != null)
				damageable.Damage(Damage);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && _playerRigid != null && _eMovement != null)
		{
			if (_eMovement.HasReached)
				_playerRigid.AddForce(transform.right * Knockback * 2, ForceMode2D.Impulse);
			else
				_playerRigid.AddForce(transform.right * -Knockback * 2, ForceMode2D.Impulse);
		}
	}
}
