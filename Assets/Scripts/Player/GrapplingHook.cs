using UnityEngine;
using System.Collections;

public struct ThisStruct
{

}

public class GrapplingHook : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private GameObject Player;
	[SerializeField] private LayerMask PlayerMask;
	[SerializeField] private LineRenderer LineRef;
	[SerializeField] private GameManager PlayerManager;

	[Header("Hook Properties")]
	[SerializeField] private float _speed = 10;
	[SerializeField] private float _raycastDistance = 20;
	[SerializeField] private float _jumpVelocity = 4;

	private PlayerHealth PHealth;
	private Rigidbody2D PlayerRigid;
	private DistanceJoint2D PlayerJoint;

	public AudioSource GrappleSound;

	private Camera _playerCamera;



	Vector3 target;
	Vector3 difference;


	private void Awake()
	{
		PHealth = Player.GetComponent<PlayerHealth>();
		PlayerRigid = Player.GetComponent<Rigidbody2D>();
		PlayerJoint = Player.GetComponent<DistanceJoint2D>();
		_playerCamera = GetComponent<Camera>();
	}

	private void Start()
	{
		if (Player != null && PlayerRigid != null && PHealth != null && PlayerJoint != null)
		{
			PlayerJoint.enabled = false;
			LineRef.enabled = false;
		}
	}

	void Update()
	{
		if(PHealth.CurrentHealth > 0 && PlayerManager.UnlockItem[0].EnableItem && Player != null && PHealth != null && PlayerRigid != null && PlayerJoint != null)
		{
			target = _playerCamera.ScreenToWorldPoint(new Vector3(Screen.width - Input.mousePosition.x, Screen.height - Input.mousePosition.y, transform.position.z));

			LineRef.SetPosition(0, Player.transform.position);

			if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.E))
				HookOnHit(); 

			if (Input.GetButtonDown("Jump") && PlayerJoint.enabled == true)
				HookOff();

			difference = target - Player.transform.position;
		}
	}

	void HookOnHit()
	{
		RaycastHit2D hit = Physics2D.Raycast(Player.transform.position, difference, _raycastDistance, PlayerMask);

		if(hit.collider != null)
		{
			GrappleSound.Play();

			PlayerJoint.enabled = true;
			LineRef.enabled = true;

			PlayerJoint.connectedAnchor = hit.point;
			PlayerJoint.distance = hit.distance;
			LineRef.SetPosition(1, new Vector3(hit.point.x, hit.point.y, Player.transform.position.z));

			StartCoroutine(PullCoroutine());
		}
	}

	void HookOff()
	{
		StopAllCoroutines();
		PlayerJoint.enabled = false;
		LineRef.enabled = false;
		PlayerRigid.AddForce(transform.up * _jumpVelocity, ForceMode2D.Impulse);
	}

	IEnumerator PullCoroutine()
	{
		while (true)
		{
			if(PlayerJoint.distance < 0.3f)
			{
				yield break;
			}

			PlayerJoint.distance -= _speed * Time.deltaTime;
			yield return null;
		}
	}
}