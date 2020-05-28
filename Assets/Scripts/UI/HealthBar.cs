using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Slider slider;
	[SerializeField] private float _speed;
	private float _targetHealth;
	private Coroutine _updateHealthCoroutine;



	public void SetMaxHealth(float maxHealth)
	{
		slider.maxValue = maxHealth;
		slider.value = maxHealth;
		_targetHealth = maxHealth;
	}

	public void SetCurrentHealth(float currentHealth)
	{
		slider.value = _targetHealth;

		if (_updateHealthCoroutine != null)
			StopCoroutine(_updateHealthCoroutine);

		_targetHealth = currentHealth;

		_updateHealthCoroutine = StartCoroutine(UpdateHealth());
	}

	IEnumerator UpdateHealth()
	{
		float startingValue = slider.value;
		float timer = 0;

		while(true)
		{
			timer += Time.deltaTime * _speed;

			slider.value = Mathf.Lerp(startingValue, _targetHealth, timer);

			if (timer >= 1f)
				break;

			yield return null;
		}

		slider.value = _targetHealth;
	}
}
		
