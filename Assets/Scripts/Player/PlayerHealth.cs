using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public ShakeCamera2D CameraShake;
	public HealthBar healthBar;

	[Space]
	[Header("Regenerations Properties")]

	[SerializeField] private float _regenSpeed;
	[SerializeField] private float _regenAmount;

	public AudioSource HurtSound;

	public float MaxHealth = 100;
	[HideInInspector]public float CurrentHealth;


	private void Awake()
	{
		CurrentHealth = MaxHealth;
	}

	void Start()
	{
		// Set max health to UI
		healthBar.SetMaxHealth(MaxHealth);

		StartCoroutine("Regenerate");
	}

	public void OnDamage(float damage)
	{
		CurrentHealth = Mathf.Clamp(CurrentHealth -= damage, 0, MaxHealth);

		HurtSound.Play();

		// Update health in UI
		healthBar.SetCurrentHealth(CurrentHealth);

		CameraShake.Shake(0.05f, 0.1f);
	}

	IEnumerator Regenerate()
	{
		float timer = 0;

		while(true)
		{
			timer += Time.deltaTime * _regenSpeed;
			OnHeal(_regenAmount);

			yield return null;
		}
	}

	public void OnHeal(float heal)
	{
		CurrentHealth = Mathf.Clamp(CurrentHealth += heal, 0, MaxHealth);
		healthBar.SetCurrentHealth(CurrentHealth);
	}
}
