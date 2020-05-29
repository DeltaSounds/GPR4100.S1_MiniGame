using System.Collections;
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
	[SerializeField] private LineRenderer _laserLine;
	[SerializeField] private float _laserDamagePerSecond = 2;
	[SerializeField] private float _distance = 5;
	[SerializeField] private float _coolDown = 2;
	[SerializeField] private float _duration = 1;

	private Vector3 _difference;
	private float _timer1;
	private bool _laserActive;


	private void Start()
	{
		_laserLine.enabled = false;
	}

	private void Update()
	{
		_laserLine.SetPosition(0, new Vector3(_laserEmitter.position.x, _laserEmitter.position.y, transform.position.z));

		_timer1 -= Time.deltaTime;

		if (_timer1 <= 0 && _laserActive)
		{
			StartCoroutine(ShootLaser());
			_timer1 = _coolDown;
		}
	}

	public void StartLaser()
	{
		_laserActive = true;
		_timer1 = _coolDown;
	}

	public void StopLaser()
	{
		_laserActive = false;
		_laserLine.enabled = false;
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
		
		while (timer > 0 && _laserActive)
		{
			RaycastHit2D laser = Physics2D.Raycast(_laserEmitter.position, _laserEmitter.right * -1, _distance, _CollisionMask);
			Debug.DrawLine(_laserEmitter.position, laser.point, Color.red, Time.deltaTime);

			_laserLine.enabled = true;

			timer -= Time.deltaTime;
			_laserLine.SetPosition(1, new Vector3(laser.point.x, laser.point.y, transform.position.z));

			IDamageable damageable = null;

			if (laser.collider != null)
				damageable = laser.collider.GetComponent<IDamageable>();

			if (damageable != null)
				damageable.Damage(_laserDamagePerSecond * Time.deltaTime);

			yield return null;
		}

		_laserLine.enabled = false;
	}
	
}
