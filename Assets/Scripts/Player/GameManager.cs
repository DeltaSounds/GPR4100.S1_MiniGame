using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject PlayerRef;
	public GameObject Arms;

	[HideInInspector] public SpriteRenderer CharacterRendr;
	[HideInInspector] public PlayerHealth PlayerH;

	[SerializeField] private EndGame _uiGameOver;
	[SerializeField] private Animator _animator;

	public Items[] UnlockItem = new Items[2];

	public int LevelIndex;




	private void Awake()
	{
		CharacterRendr = PlayerRef.GetComponent<SpriteRenderer>();
		PlayerH = PlayerRef.GetComponent<PlayerHealth>();
	}

	private void Start()
	{
		_animator.SetBool("hasGun", UnlockItem[1].EnableItem);

		if (UnlockItem[1].EnableItem)
			Arms.SetActive(true);
	}

	private void Update()
	{
		if(PlayerH.CurrentHealth <= 0)
		{
			_uiGameOver.OnEndGame();

			CharacterRendr.enabled = false;
		}
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(LevelIndex);
	}
}
