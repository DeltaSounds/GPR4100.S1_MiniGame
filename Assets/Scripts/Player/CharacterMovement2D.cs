using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
	[SerializeField] float _speed = 20;

	private Animator _animator;
	private CharacterController2D _controller;
	private float _horizontalMove;
	private bool _isJump = false;


	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
	}

	void Update()
    {
		_horizontalMove = Input.GetAxisRaw("Horizontal") * _speed;

		// Set speed parameter for running animation
		_animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
	
		if (Input.GetButtonDown("Jump"))
		{
			_isJump = true;

			// Set jump parameter for jumping animation
			_animator.SetBool("isJump", true);
		}
	}

	private void FixedUpdate()
	{
		_controller.Move(_horizontalMove * Time.fixedDeltaTime, _isJump);
		_isJump = false;
	}

	public void OnLanded()
	{
		_animator.SetBool("isJump", false);
	}
}
