using UnityEngine.UI;
using UnityEngine;

public class EndGame : MonoBehaviour
{
	[SerializeField] private GameObject PlayerRef;
	[Space]
	[Header("Game Over Settings")]
	[SerializeField] private Text _title;
	[SerializeField] private string _looseTitleText = "Game Over!";
	[SerializeField] private Color _looseTitleColor;
	[Space]
	[Header("Win Settings")]
	[SerializeField] private string _winTitleText = "You Win!";
	[SerializeField] private Color _winTitleColor;


	public void OnEndGame(bool gameOver)
	{
		if (gameOver)
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
