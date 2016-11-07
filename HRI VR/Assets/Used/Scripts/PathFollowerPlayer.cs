using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class PathFollowerPlayer : MonoBehaviour {

	public Transform[] wayPointList;
	public float speed = 1.0f;
	public float reachDist = 1.0f;
	private float timeLeftSound = 0.0f;
	public int currentWayPoint = 0;
	public bool playerEndPos = false;
	public bool startWalking = false;

	float _waitTime;

	Animator playerAnimator;
	AudioSource audio;
	Transform targetWayPoint;

	void Start () {
		playerAnimator = GetComponent<Animator> ();
		audio = GetComponent<AudioSource>();
	}

	void Update () {
		if (Input.GetKeyDown ("1")) {
			playerAnimator.SetBool ("isWalking", true);
			startWalking = true;

			audio.Play();
		}

		if (timeLeftSound < 0.8f) {
			timeLeftSound += 0.0008f;
			AudioListener.volume = timeLeftSound;
		}

		if (startWalking) {
			if (currentWayPoint < this.wayPointList.Length) {
				if (targetWayPoint == null) {
					targetWayPoint = wayPointList [currentWayPoint];
				}

				if (currentWayPoint < wayPointList.Length) {
					Walk();
				}
			}
		}
	}

	void Walk() {
		if(transform.position == targetWayPoint.position) {
			if (_waitTime > 0f) {
				playerAnimator.SetBool ("isWalking", false);
				_waitTime -= Time.deltaTime;
			} else {
				int _arrayLength = wayPointList.Length - 1;
				if (currentWayPoint < _arrayLength) {
					playerAnimator.SetBool ("isWalking", true);
					currentWayPoint++;
					targetWayPoint = wayPointList[currentWayPoint];
					_waitTime = wayPointList[currentWayPoint].gameObject.GetComponent<DestinationPoint>().waitingTime;
				} else {
					playerAnimator.SetBool ("isWalking", false);
					startWalking = false;
					playerEndPos = true;
					Debug.Log("Done moving");
				}
			}
		} else {
			transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed*Time.deltaTime);
		}
	} 

	void OnDrawGizmos() {
		if (wayPointList.Length > 0) {
			for (int i = 0; i < wayPointList.Length; i++) {
				if (wayPointList[i] != null) {
					Gizmos.color = Color.red;
					Gizmos.DrawSphere(wayPointList[i].position, reachDist);
				}
			}
		}
	}
}
