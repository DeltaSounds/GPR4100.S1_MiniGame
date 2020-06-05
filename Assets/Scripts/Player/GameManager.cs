using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject PlayerRef;
	[SerializeField] private GameObject Arms;
	[SerializeField] private EndGame _uiGameOver;
	[SerializeField] private Animator _animator;
	[SerializeField] private GameObject _pause;

	private SpriteRenderer CharacterRendr;
	private bool _toggle = true;

	public Items[] UnlockItem = new Items[2];
	public int LevelIndex;




	private void Awake()
	{
		CharacterRendr = PlayerRef.GetComponent<SpriteRenderer>();
		PlayerRef.GetComponent<HealthComponent>().Death += OnDeath;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			PauseGame();
	}

	private void OnDeath()
	{
		_uiGameOver.OnEndGame(true);
		Time.timeScale = 0.2f;
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

	public void PauseGame()
	{
		if(_toggle)
		{
			Time.timeScale = 0;
			_pause.SetActive(true);

			_toggle = false;
		}
		else
		{
			Time.timeScale = 1;
			_pause.SetActive(false);

			_toggle = true;
		}
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene(0);
	}
}
