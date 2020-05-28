using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakSpot : MonoBehaviour
{
	public GameObject PlayerRef;
	public GameObject EnemyRef;
	public GameObject DeathParticle;

	[HideInInspector] public Rigidbody2D PlayerRigid;

	public float Knockback = 3;


	private void Awake()
	{
		PlayerRigid = PlayerRef.GetComponent<Rigidbody2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player" && PlayerRigid != null)
		{
			PlayerRigid.AddForce(transform.up * Knockback, ForceMode2D.Impulse);
			Destroy(EnemyRef);

			Instantiate(DeathParticle, EnemyRef.transform.position, Quaternion.identity);
		}
		else if (PlayerRigid == null)
			Debug.LogError("(EnemyWeakSpot) Your Player is missing Rigidbody2D");
	}
}
