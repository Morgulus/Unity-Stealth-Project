using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour {

	public AudioClip keyGrabClip;

	private PlayerInventory playerInventory;
	private GameObject player;

	void Awake(){
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerInventory = player.GetComponent<PlayerInventory> ();
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject == player) {
			AudioSource.PlayClipAtPoint (keyGrabClip, transform.position);
			playerInventory.hasKey = true;
			Destroy (gameObject, 0.1f);
		}
	}
}
