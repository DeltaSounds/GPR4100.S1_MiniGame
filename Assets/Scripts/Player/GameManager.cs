using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject PlayerRef;
	[SerializeField] private GameObject Arms;
	[SerializeField] private EndGame _uiGameOver;
	[SerializeField] private Animator _animator;

	private SpriteRenderer CharacterRendr;

	public Items[] UnlockItem = new Items[2];
	public int LevelIndex;




	private void Awake()
	{
		CharacterRendr = PlayerRef.GetComponent<SpriteRenderer>();
		PlayerRef.GetComponent<HealthComponent>().Death += OnDeath;
	}

	private void OnDeath()
	{
		_uiGameOver.OnEndGame(true);
		Time.timeScale = 0;
		CharacterRendr.enabled = false;
	}

	private void Start()
	{
		Time.timeScale = 1;

		_animator.SetBool("hasGun", UnlockItem[1].EnableItem);

		if (UnlockItem[1].EnableItem)
			Arms.SetActive(true);
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(LevelIndex);
	}
}
