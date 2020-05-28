using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
	[Header("References")]
	[SerializeField]private Transform _laserEmitter;

	[Space]
	[Header("Boss Stats")]
	[SerializeField] private float _damage = 30;
	[SerializeField] private float _knockback = 3;

	[SerializeField] private LayerMask _CollisionMask;

	[Space]
	[Header("Laser Settings")]
	[SerializeField] private bool _enableLaser;
	[SerializeField] private float _laserDamagePerSecond = 2;
	[SerializeField] private float _distance = 5;
	[SerializeField] private float _coolDown = 2;
	[SerializeField] private float _duration = 1;

	private Vector3 _difference;
	private float _timer1;
	private bool _laserActive;




	private void Update()
	{
		_timer1 -= Time.deltaTime;

		if (_timer1 <= 0 && _laserActive)
		{
			StartCoroutine(ShootLaser());
		}
	}

	public void StartLaser()
	{
		_laserActive = true;
	}

	public void StopLaser()
	{
		_laserActive = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			_knockback = (transform.position.x > collision.transform.position.x ? Mathf.Abs(_knockback) : -Mathf.Abs(_knockback));

			collision.attachedRigidbody.AddForce(-Vector3.right * _knockback, ForceMode2D.Impulse);

			IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

			if (damageable != null)
				damageable.Damage(_damage);
		}
	}

	IEnumerator ShootLaser()
	{
		float timer = _duration;

		while (timer > 0 && _enableLaser)
		{
			RaycastHit2D laser = Physics2D.Raycast(_laserEmitter.position, _laserEmitter.right * -1, _distance, _CollisionMask);
			Debug.DrawLine(_laserEmitter.position, laser.point, Color.red, Time.deltaTime);

			timer -= Time.deltaTime;

			IDamageable damageable = null;

			if (laser.collider != null)
				damageable = laser.collider.GetComponent<IDamageable>();

			if (damageable != null)
				damageable.Damage(_laserDamagePerSecond * Time.deltaTime);

			yield return null;
		}
	}
	
}
