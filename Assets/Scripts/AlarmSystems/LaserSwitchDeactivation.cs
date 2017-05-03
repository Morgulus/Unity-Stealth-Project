using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitchDeactivation : MonoBehaviour {

	public GameObject laser;
	public Material unlockedMat;

	private GameObject player;
	private DiscoRotation disco;
	private float timer = 0f;
	private float timeToDiscoOn = 5f;

	void Awake(){
		player = GameObject.FindGameObjectWithTag (Tags.player);
		disco = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<DiscoRotation> ();
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject == player) {
			if (Input.GetButtonUp ("Switch")) {
				LaserDeactivation ();
			}
			if(Input.GetButton("Switch") && transform.name == "prop_switchUnit_001")
				timer += Time.deltaTime;
			if (timer >= timeToDiscoOn)
				disco.discoKey = true;
		}
	}

	void LaserDeactivation(){
		laser.SetActive (false);

		Renderer screen = transform.Find ("prop_switchUnit_screen").GetComponent<Renderer> ();
		screen.material = unlockedMat;
		GetComponent<AudioSource> ().Play ();
	}
}
