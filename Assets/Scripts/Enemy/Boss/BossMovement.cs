using UnityEngine;

public class BossMovement : MonoBehaviour, IDamageable
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
	[SerializeField] private float _health = 200;

	[Space]
	[Header("Rage Settings")]
	[SerializeField] private bool _rageMode = false;
	[SerializeField] private float _rageModeThrust = 10;
	[SerializeField] private float _coolDown = 2;
	[SerializeField] private float _duration = 1;

	private Rigidbody2D _selfRigid;
	private BossAttack _attackRef;
	private float _currentDistance;
	[SerializeField] private float AICurrentHealth;
	private bool _turn;
	private float _timer;

	void Awake()
	{
		_selfRigid = GetComponent<Rigidbody2D>();
		_attackRef = GetComponent<BossAttack>();
	}

	void Start()
	{
		AICurrentHealth = _health;
		_timer = _coolDown;
	}

	private void Update()
	{
		if (AICurrentHealth <= 0)
			Destroy(gameObject);

		_timer -= Time.deltaTime;

		UpdateState();

		switch (_currentState)
		{
			case BossState.Laser:
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

	void UpdateRage()
	{
		// On rage drunk rotate
		transform.Rotate(0, 0, 1 * _rotationSpeed * Time.deltaTime);
	}

	public void Damage(float damage)
	{
		AICurrentHealth -= damage;
	}
}
