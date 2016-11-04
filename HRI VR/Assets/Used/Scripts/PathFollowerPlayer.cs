using UnityEngine;
using System.Collections;

public class PathFollowerPlayer : MonoBehaviour {

	public Transform[] path;
	public float speed = 5.0f;
	public float reachDist = 1.0f;
	public int currentPoint = 0;
	public bool playerEndPos = false;

	void Start () {
		
	}

	void Update () {
		float dist = Vector3.Distance (path [currentPoint].position, transform.position);
		transform.position = Vector3.MoveTowards (transform.position, path [currentPoint].position, Time.deltaTime * speed);

		Vector3 movement = new Vector3 (transform.position.x, 0.0f, -transform.position.z);
		Quaternion toRotate = Quaternion.Euler(0, 0, 0);

		if (playerEndPos == false) {
			transform.rotation = Quaternion.LookRotation (movement);
		}
		if (playerEndPos == true) {
			transform.rotation = Quaternion.Slerp(transform.rotation, toRotate, Time.deltaTime*1.0f);
		}

		if (dist <= reachDist) {
			if (currentPoint + 1 < path.Length) {
				currentPoint++;
			}

			if (currentPoint + 1 == path.Length) {
				playerEndPos = true;
			}
		}

		if (currentPoint >= path.Length) {
			currentPoint = currentPoint;
		}
	}

	void OnDrawGizmos() {
		if (path.Length > 0) {
			for (int i = 0; i < path.Length; i++) {
				if (path[i] != null) {
					Gizmos.color = Color.red;
					Gizmos.DrawSphere(path[i].position, reachDist);
				}
			}
		}
	}
}
