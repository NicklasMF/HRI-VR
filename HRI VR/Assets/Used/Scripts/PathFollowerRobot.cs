using UnityEngine;
using System.Collections;

public class PathFollowerRobot : MonoBehaviour {
	
	public Transform[] path;
	public float speed;
	public float reachDist = 1.0f;
	public int currentPoint = 0;
	public bool robotEndPos = false;
	
	int endPosition;
	GameObject player;
	PathFollowerPlayer playerScript;
	Animator robotAnimator;


	void Start () {
		player = GameObject.Find ("Player");
		playerScript = player.GetComponent<PathFollowerPlayer> ();
		endPosition = 0;
		speed = 0;
		robotAnimator = GetComponent<Animator>();
	}

	public void SetEndPosition(int _position, float _speed) {
		endPosition = _position + 1;
		speed = _speed;
		Debug.Log("Position: "+endPosition+", Speed: "+ speed +" er blevet sat for robot.");
	}

	public void Wave() {
		robotAnimator.SetTrigger("wave");
	}

	void Update () {
		if(playerScript.playerEndPos == true && endPosition != 0) {
			
			float dist = Vector3.Distance (path [currentPoint].position, transform.position);
			transform.position = Vector3.MoveTowards (transform.position, path [currentPoint].position, Time.deltaTime * speed);

			Vector3 movement = new Vector3 (transform.position.x, 0.0f, -transform.position.z);
			Quaternion toRotate = Quaternion.Euler(0, 180, 0);

			if (robotEndPos == false) {
				transform.rotation = Quaternion.LookRotation (movement);
			}

			if (robotEndPos == true) {
				transform.rotation = Quaternion.Slerp(transform.rotation, toRotate, Time.deltaTime*1.0f);
			}

			if (dist <= reachDist) {
				if (currentPoint < endPosition) {
					if (currentPoint + 1 < path.Length) {
						Debug.Log("Curr: "+currentPoint + ", End: "+endPosition);
						currentPoint++;
					} else {
						robotEndPos = true;
					}
				}
			}
				
			if (currentPoint + 1 == path.Length) {
				robotEndPos = true;
			}

			if (currentPoint >= path.Length) {
				currentPoint = currentPoint;
			}
		
		}
	}

	void OnDrawGizmos() {
		if (path.Length > 0) {
			for (int i = 0; i < path.Length; i++) {
				if (path[i] != null) {
					Gizmos.color = Color.blue;
					Gizmos.DrawSphere(path[i].position, reachDist);
				}
			}
		}
	}
}
