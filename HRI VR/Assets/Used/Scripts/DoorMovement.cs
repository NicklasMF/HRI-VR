using UnityEngine;
using System.Collections;

public class DoorMovement : MonoBehaviour {

	public float MoveSpeed = 0;
	public float MaxDistance = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < MaxDistance) {
			
			transform.position = new Vector3 (transform.position.x + MoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
		}
	}
}
