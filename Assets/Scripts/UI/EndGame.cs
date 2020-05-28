using UnityEngine.UI;
using UnityEngine;

public class EndGame : MonoBehaviour
{
	public GameObject PlayerRef;

	[HideInInspector] public PlayerHealth PlayerH;

	[Space]

	[SerializeField] private Text _title;
	[SerializeField] private string _looseTitleText = "Game Over!";
	[SerializeField] private Color _looseTitleColor;

	[Space]

	[SerializeField] private string _winTitleText = "You Win!";
	[SerializeField] private Color _winTitleColor;


	private void Start()
	{
		gameObject.SetActive(false);
	}

	public void OnEndGame()
	{
		PlayerH = PlayerRef.GetComponent<PlayerHealth>();

		if (PlayerH.CurrentHealth <= 0)
		{
			// if player is dead then set the title text and color and make it visible
			gameObject.SetActive(true);

			_title.text = _looseTitleText;
			_title.color = _looseTitleColor;
		}
		else
		{
			// if player isn't dead then set text and color to win

			_title.text = _winTitleText;
			_title.color =_winTitleColor;

			gameObject.SetActive(true);
		}
	}
}
