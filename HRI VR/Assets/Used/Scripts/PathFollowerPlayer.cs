using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class PathFollowerPlayer : MonoBehaviour {

	public Transform[] wayPointList;
	public float speed = 1.0f;
	public float reachDist = 1.0f;
	public float timeLeftFirstRotate = 5.0f;
	public float timeLeftSecondRotate = 10.0f;
	private float timeLeftSound = 0.0f;
	public int currentWayPoint = 0;
	public bool playerEndPos = false;
	public bool startWalking = false;
	public bool firstRotate = true;
	public bool secondRotate = false;
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

				Debug.Log ("CurrentPoint: " + currentWayPoint);

				if (currentWayPoint == 0) {
					Walk ();
				}

				if (currentWayPoint == 1) {
					Walk ();
				}

				if (currentWayPoint == 2) {
					transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, 0.5f*Time.deltaTime, 0.0f);
					playerAnimator.SetBool ("isWalking", false);

					timeLeftFirstRotate -= Time.deltaTime;
					if (timeLeftFirstRotate < 0) {
						playerAnimator.SetBool ("isWalking", true);
						Walk ();
					}
				}

				if (currentWayPoint == 3) {
					playerAnimator.SetBool ("isWalking", false);
					if (firstRotate == true) {
						transform.forward = Vector3.RotateTowards(transform.forward, new Vector3(-2.5f, 0.5f, -1f) - transform.position, 0.5f*Time.deltaTime, 0.0f);
					} 
					else if (secondRotate == true) {
						transform.forward = Vector3.RotateTowards(transform.forward, new Vector3(1f, 0.5f, -1.7f) - transform.position, 0.5f*Time.deltaTime, 0.0f);
					}

					timeLeftFirstRotate -= Time.deltaTime;
					if (timeLeftFirstRotate < 0) {
						firstRotate = false;
						secondRotate = true;

						timeLeftSecondRotate -= Time.deltaTime;
						if (timeLeftSecondRotate < 0) {
							playerAnimator.SetBool ("isWalking", true);
							Walk ();
						}
					}


				}

				if (currentWayPoint == 4) {
					playerAnimator.SetBool ("isWalking", false);
					transform.forward = Vector3.RotateTowards(transform.forward, new Vector3(1.8f, 0.4f, 3f) - transform.position, 0.5f*Time.deltaTime, 0.0f);

					timeLeftFirstRotate -= Time.deltaTime;
					if (timeLeftFirstRotate < 0) {
						playerEndPos = true;
					}
				}
			}
		}
	}

	void Walk() {
		transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position,   speed*Time.deltaTime);

		if(transform.position == targetWayPoint.position)
		{
			timeLeftFirstRotate = 5f;
			currentWayPoint ++ ;
			targetWayPoint = wayPointList[currentWayPoint];
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
