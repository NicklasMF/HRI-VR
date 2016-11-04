using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private Vector3 startingPosition;
	private Vector3 startingForward;
	private bool startWalking = false;
	public float speed = 0;
	public float distance = 0;

	Animator playerAnimator;

	void Start ()
	{
		playerAnimator = GetComponent<Animator> ();
		startingForward = transform.forward;
		startingPosition = transform.position;
	}

	void Update () {
		if (Input.GetKeyDown ("1")) {
			bool isWalkingPressed = true;
			playerAnimator.SetBool ("isWalking", isWalkingPressed);
			startWalking = true;
		}

		if (startWalking) {
			transform.position = Vector3.MoveTowards (transform.position, startingPosition + startingForward * distance, speed * Time.deltaTime);
		}
	}
}
