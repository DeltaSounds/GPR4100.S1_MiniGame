using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMovement : MonoBehaviour
{
	public GameObject CameraRef;

	[Space]
	public GameObject PointStart;
	public GameObject PointEnd;

	[SerializeField] private float _horizontalSpeed;
	[SerializeField] private float _verticalSpeed;

	Vector3 newPos;

	private void Start()
	{

		CameraRef.transform.position = new Vector3 (PointStart.transform.position.x, CameraRef.transform.position.y, CameraRef.transform.position.z);
		newPos = CameraRef.transform.position;

		StartCoroutine(HorizontalMoveCoroutine());
	}

	IEnumerator HorizontalMoveCoroutine()
	{
		int direction = 1;

		while (true)
		{
			if (newPos.x > PointEnd.transform.position.x)
			{
				direction = -1;
			}
			else if (newPos.x < PointStart.transform.position.x)
			{
				direction = 1;
			}


			newPos.x += direction * _horizontalSpeed * Time.deltaTime;

			CameraRef.transform.position = newPos;
			yield return null;
		}
	}
}
