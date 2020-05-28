using UnityEngine;

public class BossMovement : MonoBehaviour
{
	public Transform Target;
	public float MoveSpeed;
	public float RotationSpeed;

	public bool RageMode;

	[Space]
	public float SlowingDistance;
	public float StoppingDistance;

	private float _currentDistance;


	private void Update()
	{
		// Eye's Rotation
		Vector3 dir = Target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180;
		Quaternion finalRotation = Quaternion.AngleAxis(angle, Vector3.forward);

		if (RageMode)
			transform.Rotate(0, 0, angle * RotationSpeed * Time.deltaTime);
		else
			transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, RotationSpeed * Time.deltaTime);





		_currentDistance = Vector2.Distance(Target.position, transform.position);

		if(_currentDistance > StoppingDistance)
		{
			float adjustedSpeed = MoveSpeed;

			adjustedSpeed = Mathf.Lerp(0, MoveSpeed, _currentDistance / SlowingDistance);


			transform.Translate((Target.position - transform.position).normalized * adjustedSpeed * Time.deltaTime, Space.World);
		}
	}
}
