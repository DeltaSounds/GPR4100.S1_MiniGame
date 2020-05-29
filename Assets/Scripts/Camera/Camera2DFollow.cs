using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour
{
	public Transform target;

	[Space]
	[Header("Camera Follow Settings")]
	[SerializeField] private float _damping = 1;
	[SerializeField] private float _lookAheadFactor = 3;
	[SerializeField] private float _lookAheadReturnSpeed = 0.5f;
	[SerializeField] private float _lookAheadMoveThreshold = 0.1f;
	[SerializeField] private float _yPosRestriction = -1;
	[Space]
	[Header("Scroll Settings")]
	[SerializeField] private float _maxScrollBound = -0.462f;
	[SerializeField] private float _minScrollBound = 1.671f;
	[SerializeField] private float _scrollMax;
	[SerializeField] private float _scrollMin;
	[SerializeField] private float _startOffset;

	private float _offsetZ;
	private float _nextTimeToSearch = 0;
	private Vector3 _lastTargetPosition;
	private Vector3 _currentVelocity;
	private Vector3 _lookAheadPos;
	private Vector3 _newZ;
	



	// Use this for initialization
	void Start ()
	{
		_lastTargetPosition = target.position;
		_offsetZ = _startOffset;
		transform.parent = null;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (target == null)
		{
			FindPlayer ();
			return;
		}


		float scroll = Input.mouseScrollDelta.y;
		_offsetZ = Mathf.Clamp(scroll + _offsetZ, _scrollMin, _scrollMax);

		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - _lastTargetPosition).x;

	    bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > _lookAheadMoveThreshold;

		if (updateLookAheadTarget)
		{
			_lookAheadPos = _lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		} else
		{
			_lookAheadPos = Vector3.MoveTowards(_lookAheadPos, Vector3.zero, Time.deltaTime * _lookAheadReturnSpeed);	
		}
		
		Vector3 aheadTargetPos = target.position + _lookAheadPos + Vector3.forward * _offsetZ;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref _currentVelocity, _damping);

		newPos = new Vector3 (newPos.x, Mathf.Clamp (newPos.y, _yPosRestriction, Mathf.Infinity), _offsetZ);

		transform.position = newPos;
		
		_lastTargetPosition = target.position;
	}

	void FindPlayer ()
	{
		if (_nextTimeToSearch <= Time.time)
		{
			GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
			if (searchResult != null)
				target = searchResult.transform;
			_nextTimeToSearch = Time.time + 0.5f;
		}
	}
}
