using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
	[Header("Spawn Settings")]
	[SerializeField] private GameObject _boss;
	[SerializeField] private GameObject _bossUI;

	[SerializeField] private bool _changeMusic;
	[SerializeField] private AudioSource _stopLastMusic;
	[SerializeField] private AudioSource _startNextMusic;

	private bool _doOnce = true;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			_boss.SetActive(true);
			_bossUI.SetActive(true);

			if (_changeMusic && _doOnce)
			{
				_stopLastMusic.Stop();
				_startNextMusic.Play();
				_doOnce = false;
			}
		}
	}
}
