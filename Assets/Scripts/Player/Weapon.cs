using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private GameManager PlayerManager;
	[SerializeField] private CharacterController2D CharControl;
	[SerializeField] private GameObject Projectile;
	[SerializeField] private Transform Arm;
	[SerializeField] private Transform FireTransform;

	[Space]
	[Header("Fire Settings")]
	[Range(0, 2)] public float FireDelay;
	[SerializeField] private bool _singleFire;
	[SerializeField] private AudioSource _source;

	private float _timer;
	private Vector3 _target;
	private Vector3 _difference;
	private Camera _playerCamera;



	private void Awake()
	{
		_source = GetComponent<AudioSource>();
		_playerCamera = GetComponent<Camera>();
	}

	private void Start()
	{
		_timer = FireDelay;
	}

	void Update()
	{
		float test = 1;

		if (PlayerManager.UnlockItem[1].EnableItem)
		{
			_target = _playerCamera.ScreenToWorldPoint(new Vector3(Screen.width - Input.mousePosition.x, Screen.height - Input.mousePosition.y, transform.position.z));

			_difference = _target - Arm.position;

			float rotationZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;


			Vector3 theScale = CharControl.transform.localScale;

			bool isFlip = (rotationZ > 90 || rotationZ < -90);

			Arm.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + (isFlip ? 180 : 0));
			FireTransform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

			theScale.x = isFlip ? -1 : 1;

			CharControl.transform.localScale = theScale;



			_timer -= Time.deltaTime;

			if (Input.GetButtonDown("Fire1") && _singleFire)
				Fire();
			else if (Input.GetButton("Fire1") && !_singleFire && _timer <= 0)
			{
				_timer = FireDelay;
				Fire();
			}
		}
	}

	public void Fire()
	{
		Instantiate(Projectile, FireTransform.position, FireTransform.rotation);
		_source.Play();
	}
}
