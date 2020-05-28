using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBounceTrigger : MonoBehaviour
{
	[HideInInspector] AudioSource Source;

	private void Awake()
	{
		Source = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Ground"))
		Source.Play();
	}
}
