using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
	public GameObject PlayerRef;
	public GameObject EnemyRef;

	[HideInInspector] public PlayerHealth PHealth;
	[HideInInspector] public Rigidbody2D PlayerRigid;
	[HideInInspector] public EnemyMovement EMovement;

	[Space]
	public float Damage = 5;
	public float Knockback = 2;


	private void Awake()
	{
		PHealth = PlayerRef.GetComponent<PlayerHealth>();
		PlayerRigid = PlayerRef.GetComponent<Rigidbody2D>();
		EMovement = EnemyRef.GetComponent<EnemyMovement>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player" && PHealth != null)
			PHealth.OnDamage(Damage);
		else if (PHealth == null)
			Debug.LogError("Player is missing PlayerHealth script");

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player" && PlayerRigid != null && EMovement != null)
		{
			if (EMovement.HasReached)
				PlayerRigid.AddForce(transform.right * Knockback * 2, ForceMode2D.Impulse);
			else
				PlayerRigid.AddForce(transform.right * -Knockback * 2, ForceMode2D.Impulse);
		}
		else if (PlayerRigid == null)
			Debug.LogError("(EnemyDamage) Player is missing RigidBody2D");
		else if (EMovement == null)
			Debug.LogError("(EnemyDamage) Enemy is missing EnemyMovement script");
	}
}
