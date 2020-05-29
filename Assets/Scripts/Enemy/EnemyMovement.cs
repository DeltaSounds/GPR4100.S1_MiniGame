using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private GameObject _targetObject;
	[SerializeField] private GameObject _heightCheck;

	[Space]
	[Header("Movement Settings")]
	[SerializeField] private float _speed = 1f;
	[SerializeField] private float _distance = 2f;
	[SerializeField] private float _jumpVelocity = 1f;
	[SerializeField] private float _jumpFrequency = 1f;

	public bool HasReached = false;

	private SpriteRenderer _enemySprite;
	private Rigidbody2D _enemyRigid;
	private float _timer;
	private float _jumpTimer;
	private bool _playerDetected = false;



	private void Awake()
	{
		_enemySprite = GetComponent<SpriteRenderer>();
		_enemyRigid = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (_enemySprite != null && _enemyRigid != null)
		{
			_timer += Time.deltaTime;
			_jumpTimer += Time.deltaTime;

			//Change Direction
			if (_timer > _distance)
			{
				_timer = 0;
				ChangeDirection();
			}

			Move();
		}
		else if(_enemySprite == null)
		{
			Debug.LogError("(EnemyMovement) Your Enemy is missing a Sprite Rendered!");
		}
		else if(_enemyRigid == null)
		{
			Debug.LogError("(EnemyMovement) Your Enemy is missing a Rigid Body 2D!");
		}
	}

	public void Move()
	{
		if (HasReached)
		{
			if (_jumpTimer > _jumpFrequency)
			{
				_jumpTimer = 0;
				_enemyRigid.AddForce(transform.up * _jumpVelocity, ForceMode2D.Impulse);
			}

			if (transform.position.y > _heightCheck.transform.position.y)
			{
				transform.position += Vector3.right * _speed * Time.deltaTime;
				_enemySprite.flipX = false;
			}
		}
		else
		{
			if (_jumpTimer > _jumpFrequency)
			{
				_jumpTimer = 0;
				_enemyRigid.AddForce(transform.up * _jumpVelocity, ForceMode2D.Impulse);
			}


			if (transform.position.y > _heightCheck.transform.position.y)
			{
				transform.position += Vector3.right * -_speed * Time.deltaTime;
				_enemySprite.flipX = true;
			}
		}
	}

	void ChangeDirection()
	{
		if (HasReached)
			HasReached = false;
		else
			HasReached = true;
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(_targetObject.tag))
			_playerDetected = true;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.CompareTag(_targetObject.tag))
		{
			if(transform.position.x < _targetObject.transform.position.x)
				HasReached = true;
			else
				HasReached = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag(_targetObject.tag))
			_playerDetected = false;
	}
}
