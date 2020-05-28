using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour, IDamageable
{
	[Header("References")]
	public GameObject TargetObject;
	public Projectile Bullet;
	public Transform LaserEmitter;

	[Space]
	[Header("Boss Stats")]
	[SerializeField] private float _health = 200;
	[SerializeField] private float _damage = 30;
	[SerializeField] private float _knockback = 3;
	[SerializeField] private float _distance = 5;
	[SerializeField] private LayerMask _groundMask;
	[Space]
	public float RotationSpeed;
	public float CoolDown = 2;
	public float Duration = 1;
	[Space]
	public bool RageMode;
	public float RageModeThrust = 10;


	[HideInInspector] public Rigidbody2D TargetRigid;
	[HideInInspector] public Rigidbody2D SelfRigid;
	[HideInInspector] public PlayerHealth PHealth;
	[HideInInspector] public float AICurrentHealth;

	private float _timer1;
	private Vector3 _difference;
	private bool _turn;



	private void Awake()
	{
		TargetRigid = TargetObject.GetComponent<Rigidbody2D>();
		PHealth = TargetObject.GetComponent<PlayerHealth>();
		SelfRigid = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		AICurrentHealth = _health;

		_timer1 = CoolDown;
	}

	private void Update()
	{
		_timer1 -= Time.deltaTime;

		if (AICurrentHealth <= 0)
			Destroy(gameObject);

		if(_timer1 <= 0)
		{
			StartCoroutine(ShootLaser());

			if(RageMode)
			{
				if (_turn)
				{
					SelfRigid.AddForce(transform.right * RageModeThrust, ForceMode2D.Impulse);
					_turn = false;
				}
				else
				{
					SelfRigid.AddForce(transform.right  * -RageModeThrust, ForceMode2D.Impulse);
					_turn = true;
				}
			}



			_timer1 = CoolDown;
		}

		if (TargetObject)
		{

		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			_knockback = (transform.position.x > TargetObject.transform.position.x ? _knockback = Mathf.Abs(_knockback) : _knockback *= -1);

			TargetRigid.AddForce(transform.right * _knockback, ForceMode2D.Impulse);

			DoDamage(_damage);
		}
	}

	public void DoDamage(float damage)
	{
		PHealth.OnDamage(damage);
	}

	public void Damage(float damage)
	{
		AICurrentHealth -= damage;
	}

	IEnumerator ShootLaser()
	{
		float timer = Duration;

		while (timer > 0)
		{
			RaycastHit2D laser = Physics2D.Raycast(LaserEmitter.position, LaserEmitter.right * -1, _distance, _groundMask);
			Debug.DrawLine(LaserEmitter.position, laser.point, Color.red, Time.deltaTime);

			timer -= Time.deltaTime;

			yield return null;
		}


	}
	
}
