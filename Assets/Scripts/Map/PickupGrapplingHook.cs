using UnityEngine;

public class PickupGrapplingHook : MonoBehaviour
{
	[SerializeField] private GameManager _playerManager;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			SpriteRenderer spriteRef = GetComponent<SpriteRenderer>();

			_playerManager.UnlockItem[0].EnableItem = true;
			spriteRef.enabled = false;
		}
	}
}
