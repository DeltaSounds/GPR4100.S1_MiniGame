using UnityEngine;

public class EnemyWeakSpot : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _playerRigid;
	[SerializeField] private GameObject _enemyRef;
	[SerializeField] private GameObject _deathParticle;
	[SerializeField] private float Knockback = 3;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && _playerRigid != null)
		{
			_playerRigid.AddForce(transform.up * Knockback, ForceMode2D.Impulse);
			Destroy(_enemyRef);

			Instantiate(_deathParticle, _enemyRef.transform.position, Quaternion.identity);
		}
	}
}
