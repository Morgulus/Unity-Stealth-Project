using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour {

	public bool requireKey;
	public AudioClip doorSwishClip;
	public AudioClip accessDeniedClip;

	private GameObject player;
	private HashIDs hash;
	private Animator anim;
	private float count;
	private PlayerInventory playerInventory;
	private AudioSource audi;

	void Awake(){
		player = GameObject.FindGameObjectWithTag (Tags.player);
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		anim = GetComponent<Animator> ();
		playerInventory = player.GetComponent<PlayerInventory> ();
		audi = GetComponent<AudioSource> ();
	}

	void Update(){
		anim.SetBool (hash.openBool, count > 0);

		if (anim.IsInTransition (0) && !audi.isPlaying) {
			audi.clip = doorSwishClip;
			audi.Play ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == player) {
			if (requireKey) {
				if (playerInventory.hasKey) {
					count++;
				} else {
					audi.clip = accessDeniedClip;
					audi.Play ();
				}
			} 
			else {
				count++;
			}
		}
		else if (other.gameObject.tag == Tags.enemy) 
		{
			if (!requireKey) {
				if (other is CapsuleCollider) {
					count++;
				}
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == player || (other.gameObject.tag == Tags.enemy && other is CapsuleCollider)) {
			count = Mathf.Max (0f, count - 1);
		}
	}
}

