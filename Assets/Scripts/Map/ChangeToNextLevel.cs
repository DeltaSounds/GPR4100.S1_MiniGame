﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToNextLevel : MonoBehaviour
{
	[SerializeField] private int _sceneIndex;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			ChangeScene();
	}

	public void ChangeScene()
	{
		SceneManager.LoadScene(_sceneIndex);
	}
}
