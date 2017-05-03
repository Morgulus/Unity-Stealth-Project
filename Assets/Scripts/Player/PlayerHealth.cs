using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public float health = 10000f;
	public AudioClip deathClip;
	public float resetAfterDeath= 5f;

	private Animator anim;
	private AudioSource audi;
	private PlayerMovement playerMovement;
	private HashIDs hash;
	private SceneFadeInOut sceneFadeInOut;
	private LastPlayerSighting lastPlayerSighting;
	private float timer;
	private bool playerDead;

	void Awake(){
		anim = GetComponent<Animator>();
		audi = GetComponent<AudioSource> ();
		playerMovement = GetComponent<PlayerMovement>();
		sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting> ();

		playerDead = false;
	}
	void Update(){
		if (health <= 0f) {
			if (!playerDead) {
				PlayerDying ();
			} 
			else {
				PlayerDead ();
				LevelReset ();
			}
		}
	}
	void PlayerDying()
	{
		playerDead = true;
		anim.SetBool (hash.deadBool, playerDead);
		AudioSource.PlayClipAtPoint (deathClip, transform.position);
	}
	void PlayerDead()
	{
		if (anim.GetCurrentAnimatorStateInfo (0).fullPathHash == hash.dyingState) {
			anim.SetBool (hash.deadBool, false);
		}	
		anim.SetFloat (hash.speedFloat, 0f);
		playerMovement.enabled = false;
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
		audi.Stop ();
	}
	void LevelReset(){
		timer += Time.deltaTime;

		if (timer >= resetAfterDeath) {
			sceneFadeInOut.EndScene ();
		}
	}

	public void TakeDamage(float amount){
		health -= amount;
	}
}
