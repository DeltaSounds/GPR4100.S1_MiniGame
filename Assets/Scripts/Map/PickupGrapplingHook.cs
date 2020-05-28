using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGrapplingHook : MonoBehaviour
{
	public GameManager PlayerManager;
	public SpriteRenderer SpriteRef;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			PlayerManager.UnlockItem[0] = true;
			SpriteRef.enabled = false;
		}
	}
}
