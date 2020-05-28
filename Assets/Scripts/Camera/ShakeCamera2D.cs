using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera2D : MonoBehaviour
{
	public Camera MainCam;
	float shakeAmount = 0f;
	public bool DebugMode = false;

	public float ShakeAmt = 0.01f;
	public float ShakeLength = 0.05f;


	private void Awake()
	{
		if(MainCam == null)
		{
			MainCam = Camera.main;
		}
	}

	private void Update()
	{
		if(DebugMode)
		{
			if(Input.GetKeyDown(KeyCode.T))
			{
				Shake(ShakeAmt, ShakeLength);
			}
		}
	}

	public void Shake(float amt, float length)
	{
		shakeAmount = amt;
		InvokeRepeating("BeginShake", 0, ShakeAmt);
		Invoke("StopShake", length);
	}

	void BeginShake()
	{
		if(shakeAmount > 0)
		{
			Vector3 camPos = MainCam.transform.position;

			float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
			float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

			camPos.x += offsetX;
			camPos.y += offsetY;

			MainCam.transform.position = camPos;
		}
	}

	void StopShake()
	{
		CancelInvoke("BeginShake");
	}
}
