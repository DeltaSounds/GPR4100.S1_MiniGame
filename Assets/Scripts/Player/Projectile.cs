using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private float _damage;

	private Rigidbody2D _projectileRigid;

	private void Awake()
	{
		_projectileRigid = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_projectileRigid.velocity = transform.right * _speed;
	}

	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ground"))
			DestroyObject();

		if (collision.CompareTag("Enemy"))
		{
			collision.GetComponent<IDamageable>().Damage(_damage);
			DestroyObject();
		}
	}

	public virtual void DestroyObject()
	{
		Destroy(gameObject);
	}
}
