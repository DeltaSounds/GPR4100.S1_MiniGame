using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringJump : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rigidPlayer;
	[SerializeField] private float _force = 4;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		_rigidPlayer.AddForce(transform.up * _force, ForceMode2D.Impulse);
	}
}
