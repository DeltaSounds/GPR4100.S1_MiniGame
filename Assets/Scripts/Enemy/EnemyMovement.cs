using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

	public GameObject TargetObject;
	public GameObject EnemyRef;
	public GameObject GroundCheck;

	[HideInInspector] public SpriteRenderer EnemySprite;
	[HideInInspector] public Rigidbody2D EnemyRigid;
	[HideInInspector] public bool HasReached = false;

	[Space]
	public float Speed = 1f;
	public float Distance = 2f;
	public float JumpVelocity = 1f;
	public float JumpFrequency = 1f;

	private float _timer;
	private float _jumpTimer;

	private bool _playerDetected = false;



	private void Awake()
	{
		EnemySprite = EnemyRef.GetComponent<SpriteRenderer>();
		EnemyRigid = EnemyRef.GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (EnemySprite != null && EnemyRigid != null)
		{
			_timer += Time.deltaTime;
			_jumpTimer += Time.deltaTime;

			//Change Direction
			if (_timer > Distance)
			{
				_timer = 0;
				ChangeDirection();
			}

			Move();
		}
		else if(EnemySprite == null)
		{
			Debug.LogError("(EnemyMovement) Your Enemy is missing a Sprite Rendered!");
		}
		else if(EnemyRigid == null)
		{
			Debug.LogError("(EnemyMovement) Your Enemy is missing a Rigid Body 2D!");
		}
	}

	public void Move()
	{
		if (HasReached)
		{
			if (_jumpTimer > JumpFrequency)
			{
				_jumpTimer = 0;
				EnemyRigid.AddForce(transform.up * JumpVelocity, ForceMode2D.Impulse);
			}

			if (transform.position.y > GroundCheck.transform.position.y)
			{
				transform.position += Vector3.right * Speed * Time.deltaTime;
				EnemySprite.flipX = false;
			}
		}
		else
		{
			if (_jumpTimer > JumpFrequency)
			{
				_jumpTimer = 0;
				EnemyRigid.AddForce(transform.up * JumpVelocity, ForceMode2D.Impulse);
			}


			if (transform.position.y > GroundCheck.transform.position.y)
			{
				transform.position += Vector3.right * -Speed * Time.deltaTime;
				EnemySprite.flipX = true;
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
		if (collision.tag == TargetObject.tag)
			_playerDetected = true;
	}



	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.tag == TargetObject.tag)
		{
			if(EnemyRef.transform.position.x < TargetObject.transform.position.x)
				HasReached = true;
			else
				HasReached = false;
		}
	}



	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == TargetObject.tag)
			_playerDetected = false;
	}
}
