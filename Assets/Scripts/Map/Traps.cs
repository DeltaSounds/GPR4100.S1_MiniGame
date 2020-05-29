using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
	public Rigidbody2D PlayerCharacter;

	public float Damage = 10;
	public float Thrust = 10;


	private void OnTriggerStay2D (Collider2D other)
	{
		if(other.tag == "Player")
		{
			StartCoroutine(ThrustPlayer());
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			IDamageable damageable = other.GetComponent<IDamageable>();

			if (damageable != null)
				damageable.Damage(Damage);
		}
	}

	IEnumerator ThrustPlayer()
	{
		PlayerCharacter.AddForce(transform.up * Thrust, ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.5f);
	}
}
