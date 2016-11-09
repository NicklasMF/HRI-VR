using UnityEngine;
using System.Collections;

public class PathFollowerRobot : MonoBehaviour {
	
	public Transform[] path;
	public float speed;
	public float reachDist = 1.0f;
	public int currentPoint = 0;
	public bool robotEndPos = false;
	
	int endPosition;
	public bool saidHello;
	public bool saidHelp;
	GameObject player;
	PathFollowerPlayer playerScript;
	Animator robotAnimator;

	[SerializeField] GameObject[] sounds;


	void Start () {
		player = GameObject.Find ("Player");
		playerScript = player.GetComponent<PathFollowerPlayer> ();
		endPosition = 0;
		speed = 0;
		saidHello = false;
		saidHelp = false;
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

	public void SayHello() {
		Instantiate(sounds[0]);
		saidHello = true;
		Debug.Log("Robot saying hello");
	}

	void SayHelp() {
		Instantiate(sounds[1]);
		saidHelp = true;
		Debug.Log("Robot saying helping");
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			SayHelp();
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			SayHello();
		}


		if(playerScript.playerEndPos == true && endPosition != 0) {
			
			float dist = Vector3.Distance (path [currentPoint].position, transform.position);
			transform.position = Vector3.MoveTowards (transform.position, path [currentPoint].position, Time.deltaTime * speed);

			Vector3 rot = Vector3.RotateTowards(transform.forward, path[currentPoint].position - transform.position, Time.deltaTime * 3f, 0f);
			transform.rotation = Quaternion.LookRotation(rot);
			Quaternion toRotate = Quaternion.Euler(0, 180, 0);

			/*transform.forward = Vector3.RotateTowards(transform.forward, path[currentPoint].position - transform.position, Time.deltaTime * .4f, 0f);
			Vector3 movement = new Vector3 (transform.position.x, 0.0f, -transform.position.z);


			if (robotEndPos == false) {
				transform.rotation = Quaternion.LookRotation (movement);
			}*/

			if (robotEndPos == true) {
				transform.rotation = Quaternion.Slerp(transform.rotation, toRotate, Time.deltaTime*1.0f);
				if (!saidHelp) {
					SayHelp();
				}
			}

			if (dist <= reachDist) {
				if (currentPoint < endPosition) {
					if (currentPoint + 1 < path.Length) {
						currentPoint++;
						if (currentPoint == endPosition) {
							robotEndPos = true;
						}

					} else {
						robotEndPos = true;
					}
				}
			}
				
			if (currentPoint + 1 == path.Length) {
				robotEndPos = true;
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
