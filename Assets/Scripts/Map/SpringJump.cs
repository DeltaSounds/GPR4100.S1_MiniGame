using UnityEngine;

public class SpringJump : MonoBehaviour
{
	[SerializeField] private float _force = 4;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Rigidbody2D objectRigid = collision.GetComponent<Rigidbody2D>();

		if (objectRigid != null && !collision.isTrigger)
			objectRigid.AddForce(transform.up * _force, ForceMode2D.Impulse);
	}
}
