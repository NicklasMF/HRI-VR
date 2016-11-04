using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private Vector3 startingPosition;
	private Vector3 startingForward;
	public float speed = 0;
	public float distance = 0;

	void Start ()
	{
		startingForward = transform.forward;
		startingPosition = transform.position;
	}

	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, startingPosition + startingForward * distance, speed * Time.deltaTime);
	}
}
