using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
	public int MenuIndex;
	public EndGame EndGameUI;
	public GameManager PlayerManager;
	public CharacterController2D PlayerController;

	private void OnTriggerStay2D(Collider2D collision)
	{
		EndGameUI.OnEndGame();

		PlayerManager.LevelIndex = 0;

		CharacterController2D.LastCheckPoint = null;
	}
}
