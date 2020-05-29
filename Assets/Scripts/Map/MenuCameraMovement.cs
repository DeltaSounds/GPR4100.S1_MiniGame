using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMovement : MonoBehaviour
{
	[Header("Menu Camera Settings")]
	[SerializeField] private GameObject _cameraRef;
	[SerializeField] private GameObject _pointStart;
	[SerializeField] private GameObject _pointEnd;
	[Space]
	[SerializeField] private float _horizontalSpeed;
	[SerializeField] private float _verticalSpeed;

	Vector3 newPos;

	private void Start()
	{

		_cameraRef.transform.position = new Vector3 (_pointStart.transform.position.x, _cameraRef.transform.position.y, _cameraRef.transform.position.z);
		newPos = _cameraRef.transform.position;

		StartCoroutine(HorizontalMoveCoroutine());
	}

	IEnumerator HorizontalMoveCoroutine()
	{
		int direction = 1;

		while (true)
		{
			if (newPos.x > _pointEnd.transform.position.x)
			{
				direction = -1;
			}
			else if (newPos.x < _pointStart.transform.position.x)
			{
				direction = 1;
			}


			newPos.x += direction * _horizontalSpeed * Time.deltaTime;

			_cameraRef.transform.position = newPos;
			yield return null;
		}
	}
}
