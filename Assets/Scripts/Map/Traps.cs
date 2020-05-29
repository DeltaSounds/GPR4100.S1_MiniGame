using System.Collections;
using UnityEngine;

public class Traps : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Rigidbody2D PlayerCharacter;
	[Space]
	[Header("Trap Settings")]
	[SerializeField] private float Damage = 10;
	[SerializeField] private float Thrust = 10;


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
