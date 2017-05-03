using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManagment : MonoBehaviour {

	public Animator anim;
	public AudioSource audi;
	public AudioClip shoutingClip;

	private HashIDs hash;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		audi = GetComponent<AudioSource> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();

	}
	void Update()
	{
		bool shout = Input.GetButtonDown ("Attract");
		anim.SetBool (hash.shoutingBool, shout);
		AudioManagment (shout);
	}

	void AudioManagment(bool shout){
		if (anim.GetCurrentAnimatorStateInfo (0).fullPathHash == hash.locomotionState) {
			audi.volume = 0.1f;
			audi.pitch = 1f;
			if (!audi.isPlaying) {
				audi.Play ();
			}
		} 
		else if (anim.GetCurrentAnimatorStateInfo (0).fullPathHash == hash.sneakingState) {
			audi.volume = 0.01f;
			audi.pitch = 0.5f;
			if (!audi.isPlaying) {
				audi.Play ();
			}
		}
		else {
			audi.Stop ();
		}

		if (shout) {
			audi.Stop ();
			AudioSource.PlayClipAtPoint (shoutingClip, transform.position);
		} 
	}
}
