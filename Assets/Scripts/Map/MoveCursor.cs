using UnityEngine;

public class MoveCursor : MonoBehaviour
{
	void Update()
	{
		transform.position = Input.mousePosition;
	}
}
