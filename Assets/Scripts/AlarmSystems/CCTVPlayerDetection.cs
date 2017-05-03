using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVPlayerDetection : MonoBehaviour {
	private GameObject player;
	private LastPlayerSighting lastPlayerSighting;

	void Awake(){
		player = GameObject.FindGameObjectWithTag (Tags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
	}
	void OnTriggerStay(Collider other){
		if (other.gameObject == player) {
			Vector3 relPlayerPosition = player.transform.position - transform.position;
			RaycastHit hit;

			if (Physics.Raycast (transform.position, relPlayerPosition, out hit)) {
				if (hit.collider.gameObject == player) {
					lastPlayerSighting.position = player.transform.position;
				}
			}
		}
	}
}
