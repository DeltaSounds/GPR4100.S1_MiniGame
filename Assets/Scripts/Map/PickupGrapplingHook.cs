using UnityEngine;

public class PickupGrapplingHook : MonoBehaviour
{
	[SerializeField] private GameManager _playerManager;
	[SerializeField] private SpriteRenderer _controlImage;

	private void Start()
	{
		_controlImage.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			SpriteRenderer spriteRef = GetComponent<SpriteRenderer>();

			_playerManager.UnlockItem[0].EnableItem = true;
			spriteRef.enabled = false;
			_controlImage.enabled = true;
		}
	}
}
