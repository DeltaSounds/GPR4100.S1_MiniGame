using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToNextLevel : MonoBehaviour
{
	[SerializeField] private int _sceneIndex;
	[SerializeField] private bool _clearCheckPoints;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_clearCheckPoints)
			CharacterController2D.LastCheckPoint = null;

		if (collision.CompareTag("Player"))
			ChangeScene();
	}

	public void ChangeScene()
	{
		SceneManager.LoadScene(_sceneIndex);
	}
}
