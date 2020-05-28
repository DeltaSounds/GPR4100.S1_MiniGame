﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
	public CharacterController2D controller;
	public Animator animator;
	public PlayerHealth PHealth;
	//public AudioSource StepSound1;
	//public AudioSource StepSound2;

	public float Speed = 20;

	private float _horizontalMove;
	private bool _isJump = false;


	private void Awake()
	{
		PHealth = GetComponent<PlayerHealth>();
	}

	// Update is called once per frame
	void Update()
    {
		if (PHealth.CurrentHealth > 0)
		{
			_horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;

			// Set speed parameter for running animation
			animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

			if (Input.GetButtonDown("Jump"))
			{
				_isJump = true;

				// Set jump parameter for jumping animation
				animator.SetBool("isJump", true);
			}
		}
	}


	private void FixedUpdate()
	{
		if(PHealth.CurrentHealth > 0)
		controller.Move(_horizontalMove * Time.fixedDeltaTime, _isJump);
		_isJump = false;
	}



	public void OnLanded()
	{
		animator.SetBool("isJump", false);
	}

	void Step1()
	{
		//StepSound1.Play();
	}

	void Step2()
	{
		//StepSound2.Play();
	}
}