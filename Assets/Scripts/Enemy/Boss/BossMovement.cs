using System;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
	public enum BossState
	{
		Default,
		Laser,
		Rage
	};

	[SerializeField] private BossState _currentState;

	[Header("Movement Settings")]
	[SerializeField] private Transform _target;
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _rotationSpeed;
	[SerializeField] private float _slowingDistance;
	[SerializeField] private float _stoppingDistance;

	[Space]
	[Header("Boss Settings")]
	[SerializeField] private Sprite _sDefault, _sLaser, _sRage;

	[Space]
	[Header("Rage Settings")]
	[SerializeField] private bool _rageMode = false;
	[SerializeField] private float _rageModeThrust = 10;
	[SerializeField] private float _coolDown = 2;
	[SerializeField] private float _duration = 1;
	[SerializeField] private float AICurrentHealth;

	private float _health;
	private Rigidbody2D _selfRigid;
	private BossAttack _attackRef;
	private float _currentDistance;
	private float _timer;
	private float _rageSpeed;
	private float _laserStop;
	private float _laserSlow;
	private bool _turn;

	void Awake()
	{
		_selfRigid = GetComponent<Rigidbody2D>();
		_attackRef = GetComponent<BossAttack>();
	}

	void Start()
	{
		HealthComponent HealthComp;
		HealthComp = GetComponent<HealthComponent>();
		HealthComp.HealthChanged += OnHealthChanged;

		_health = HealthComp.MaxHealth;

		AICurrentHealth = _health;
		_timer = _coolDown;

		_rageSpeed = -_moveSpeed * 1.2f;
		_laserStop = _stoppingDistance * 1.5f;
		_laserSlow = _slowingDistance * 1.6f;

	}

	private void OnHealthChanged(float health, bool isHeal)
	{
		AICurrentHealth = health;
	}

	private void Update()
	{
		if (AICurrentHealth <= 0)
			Destroy(gameObject);

		_timer -= Time.deltaTime;

		UpdateState();

		// State Machine
		switch (_currentState)
		{
			case BossState.Laser:
				UpdateLaser();
				break;
			case BossState.Default:
				UpdateDefault();
				break;
			case BossState.Rage:
				UpdateRage();
				break;
		}

		UpdateMovement();
	}

	void UpdateState()
	{
		float percent = AICurrentHealth * 100 / _health;

		if(_currentState == BossState.Laser && percent < 33)
		{
			_currentState = BossState.Rage;
			_attackRef.StopLaser();
		}
		else if (_currentState == BossState.Default && percent < 66)
		{
			_currentState = BossState.Laser;
			_attackRef.StartLaser();
		}
	}

	private void UpdateMovement()
	{
		Vector3 dir = _target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180;
		Quaternion finalRotation = Quaternion.AngleAxis(angle, Vector3.forward);

		switch(_currentState)
		{
			case BossState.Rage:
			transform.Rotate(0, 0, angle * _rotationSpeed * Time.deltaTime);
			break;
			default:
			transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, _rotationSpeed * Time.deltaTime);
			break;
		}

		_currentDistance = Vector2.Distance(_target.position, transform.position);

		if (_currentDistance > _stoppingDistance)
		{
			float adjustedSpeed = _moveSpeed;

			adjustedSpeed = Mathf.Lerp(0, _moveSpeed, _currentDistance / _slowingDistance);


			transform.Translate((_target.position - transform.position).normalized * adjustedSpeed * Time.deltaTime, Space.World);
		}
	}

	void UpdateDefault()
	{
		GetComponent<SpriteRenderer>().sprite = _sDefault;

		// Eye's Rotation
		Vector3 dir = _target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180;
		Quaternion finalRotation = Quaternion.AngleAxis(angle, Vector3.forward);

		transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, _rotationSpeed * Time.deltaTime);

		if (_timer <= 0)
		{
			_selfRigid.AddForce(transform.right * -_rageModeThrust, ForceMode2D.Impulse);
			_timer = _coolDown;
		}
	}

	void UpdateLaser()
	{
		GetComponent<SpriteRenderer>().sprite = _sLaser;

		_stoppingDistance = 2;
		_slowingDistance = 3;

		if (_timer <= 0)
		{
			_selfRigid.AddForce(transform.up * _rageModeThrust, ForceMode2D.Impulse);
			_timer = _coolDown;
		}

	}

	void UpdateRage()
	{
		GetComponent<SpriteRenderer>().sprite = _sRage;

		_stoppingDistance = 0.6f;
		_slowingDistance = 2;
		_moveSpeed = 6;
	}
}
