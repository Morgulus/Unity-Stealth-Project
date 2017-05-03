using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float smooth = 0.4f;

	private Transform player;
	private Vector3 relCameraPos;
	private float relCameraPosMag;
	private Vector3 newCameraPos;

	void Awake(){
		player = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<Transform> ();
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;
	}

	void Update(){
		Vector3 standardPos = player.position + relCameraPos;
		Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
		Vector3[] checkPoints = new Vector3[5];
		checkPoints [0] = standardPos;
		checkPoints [1] = Vector3.Lerp (standardPos, abovePos, 0.25f);
		checkPoints [2] = Vector3.Lerp (standardPos, abovePos, 0.50f);
		checkPoints [3] = Vector3.Lerp (standardPos, abovePos, 0.75f);
		checkPoints [4] = abovePos;

		for (int i = 0; i < checkPoints.Length; i++) {
			if (ViewingPlayerCheck (checkPoints [i])) {
				break;
			}
		}

		transform.position = Vector3.Lerp (transform.position, newCameraPos, smooth * 3 * Time.deltaTime);
		SmoothLookAt ();
	}

	bool ViewingPlayerCheck(Vector3 checkPos){
		RaycastHit hit;
		if (Physics.Raycast (checkPos, player.position - checkPos, out hit, relCameraPosMag)) {
			if (hit.transform != player) {
				return false;
			}
		}

		newCameraPos = checkPos;
		return true;
	}

	void SmoothLookAt()
	{
		Vector3 relPlayerPosition = player.position - transform.position;
		Quaternion lookAtRotation = Quaternion.LookRotation (relPlayerPosition, Vector3.up);
		transform.rotation = Quaternion.Lerp (transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}
}
