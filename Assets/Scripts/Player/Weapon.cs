using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("References")]
	public GameManager PlayerManager;
	public CharacterController2D CharControl;
	[Space]
	public GameObject Projectile;
	public Transform Arm;
	public Transform FireTransform;
	[Space]
	public bool SingleFire;
	[Range(0, 2)] public float FireDelay;

	[HideInInspector] public AudioSource Source;

	private Camera _playerCamera;

	float timer;
	Vector3 target;
	Vector3 difference;



	private void Awake()
	{
		Source = GetComponent<AudioSource>();
		_playerCamera = GetComponent<Camera>();
	}

	private void Start()
	{
		timer = FireDelay;
	}

	void Update()
	{
		if (PlayerManager.UnlockItem[1])
		{
			target = _playerCamera.ScreenToWorldPoint(new Vector3(Screen.width - Input.mousePosition.x, Screen.height - Input.mousePosition.y, transform.position.z));

			difference = target - Arm.position;

			float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;


			Vector3 theScale = CharControl.transform.localScale;

			bool isFlip = (rotationZ > 90 || rotationZ < -90);

			Arm.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + (isFlip ? 180 : 0));
			FireTransform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

			theScale.x = isFlip ? -1 : 1;

			CharControl.transform.localScale = theScale;



			timer -= Time.deltaTime;

			if (Input.GetButtonDown("Fire1") && SingleFire)
				Fire();
			else if (Input.GetButton("Fire1") && !SingleFire && timer <= 0)
			{
				timer = FireDelay;
				Fire();
			}
		}
	}

	public void Fire()
	{
		Instantiate(Projectile, FireTransform.position, FireTransform.rotation);
		Source.Play();
	}
}
