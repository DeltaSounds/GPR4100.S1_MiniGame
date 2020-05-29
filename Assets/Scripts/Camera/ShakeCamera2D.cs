using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera2D : MonoBehaviour
{
	[SerializeField] private float _shakeAmount = 0.01f;
	[SerializeField] private float _shakeLength = 0.05f;
	[SerializeField] private float _shakeDelay = 0.5f;
	[SerializeField] private bool _debugMode;


	private Camera _mainCam;
	private Coroutine _shakeRoutine;
	private float _shakeAmt;


	private void Awake()
	{
		_mainCam = GetComponent<Camera>();

		if(_mainCam == null)
		{
			_mainCam = Camera.main;
		}
	}

	private void Update()
	{
		if (_debugMode && Input.GetKey(KeyCode.T))
			Shake(_shakeAmount, _shakeLength);
	}

	public void Shake(float amt, float length)
	{
		if(_shakeRoutine == null)
		_shakeRoutine = StartCoroutine(ShakeRoutine(amt, length));
	}

	IEnumerator ShakeRoutine(float amt, float length)
	{
		float timer = 0;

		while (amt > 0 && timer < length)
		{
			float offsetZ = Random.value * amt * 2 - amt;

			_mainCam.transform.eulerAngles = new Vector3(0, 0, offsetZ);

			yield return null;
			timer += Time.deltaTime;
		}

		_mainCam.transform.eulerAngles = new Vector3(0, 0, 0);

		yield return new WaitForSeconds(_shakeDelay);
		_shakeRoutine = null;
	}
}
