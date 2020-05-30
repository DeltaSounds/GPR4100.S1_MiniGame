using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
	[Header("Spawn Settings")]
	[SerializeField] private GameObject _boss;
	[SerializeField] private GameObject _bossUI;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			_boss.SetActive(true);
			_bossUI.SetActive(true);
		}
	}
}
