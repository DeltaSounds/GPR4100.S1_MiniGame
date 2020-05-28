using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float Speed;
	public float Damage;

	[HideInInspector]public Rigidbody2D ProjectileRigid;

	private void Awake()
	{
		ProjectileRigid = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		ProjectileRigid.velocity = transform.right * Speed;
	}

	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ground"))
			DestroyObject();

		if (collision.CompareTag("Enemy"))
		{
			collision.GetComponent<IDamageable>().Damage(Damage);
			DestroyObject();
		}
	}

	public virtual void DestroyObject()
	{
		Destroy(gameObject);
	}
}
