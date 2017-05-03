using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlayerDetection : MonoBehaviour {

	private GameObject player;
	private LastPlayerSighting lastPlayerSighting;
	private LaserBlinking laserBlinking;

	void Awake(){
		laserBlinking = GetComponent<LaserBlinking> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
	}

	void OnTriggerStay(Collider other){
		if (laserBlinking.render.enabled) {
			if (other.gameObject == player) {
				lastPlayerSighting.position = other.transform.position;
			}
		}
	}
}
