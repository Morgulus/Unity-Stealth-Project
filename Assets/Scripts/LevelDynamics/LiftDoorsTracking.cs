using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoorsTracking : MonoBehaviour {

	public float doorSpeed = 7f;

	private Transform leftOuterDoorTr;
	private Transform rightOuterDoorTr;
	private Transform leftInnerDoorTr;
	private Transform rightInnerDoorTr;
	private float leftDoorClosedPosX;
	private float rightDoorClosedPosX;

	void Awake(){
		leftOuterDoorTr = GameObject.Find ("door_exit_outer_left_001").GetComponent<Transform> ();
		rightOuterDoorTr = GameObject.Find ("door_exit_outer_right_001").GetComponent<Transform> ();
		leftInnerDoorTr = GameObject.Find ("door_exit_inner_left_001").GetComponent<Transform> ();
		rightInnerDoorTr = GameObject.Find ("door_exit_inner_right_001").GetComponent<Transform> ();

		leftDoorClosedPosX = leftOuterDoorTr.position.x;
		rightDoorClosedPosX = rightOuterDoorTr.position.x;
	}

	void MoveDoors(float newLeftTargetX, float newRightTargetX){
		float newX = Mathf.Lerp (leftInnerDoorTr.position.x, newLeftTargetX, doorSpeed * Time.deltaTime);
		leftInnerDoorTr.position = new Vector3 (newX, leftInnerDoorTr.position.y, leftInnerDoorTr.position.z);

		newX = Mathf.Lerp (rightInnerDoorTr.position.x, newRightTargetX, doorSpeed * Time.deltaTime);
		rightInnerDoorTr.position = new Vector3 (newX, rightInnerDoorTr.position.y, rightInnerDoorTr.position.z);
	}

	public void DoorFollowing(){
		MoveDoors (leftOuterDoorTr.position.x, rightOuterDoorTr.position.x);
	}

	public void CloseDoors(){
		MoveDoors (leftDoorClosedPosX, rightDoorClosedPosX);
	}
}
