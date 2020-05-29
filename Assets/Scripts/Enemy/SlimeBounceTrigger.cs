using UnityEngine;

public class SlimeBounceTrigger : MonoBehaviour
{
	private AudioSource _source;

	private void Awake()
	{
		_source = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Ground"))
		_source.Play();
	}
}
