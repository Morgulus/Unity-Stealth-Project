using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTrigger : MonoBehaviour {

	public float timeToDoorsClose = 2f;
	public float timeToLiftStart = 3f;
	public float timeToEndLevel = 6f;
	public float liftSpeed = 3f;

	private GameObject player;
	private Animator playerAnim;
	private HashIDs hash;
	private CameraMovement camMovement;
	private SceneFadeInOut sceneFader;
	private LiftDoorsTracking doorsTracking;
	private PlayerMovement playerMovement;
	private AudioSource audi;
	private bool playerInLift;
	private float timer;

	void Awake(){
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerAnim = player.GetComponent<Animator> ();
		playerMovement = player.GetComponent<PlayerMovement>();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		camMovement = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraMovement> ();
		sceneFader = GameObject.FindGameObjectWithTag (Tags.fader).GetComponent<SceneFadeInOut> ();
		doorsTracking = GetComponent<LiftDoorsTracking> ();
		audi = GetComponent<AudioSource> ();
	}

	void Update(){
		if (playerInLift)
			LiftActivation ();

		if (timer < timeToDoorsClose)
			doorsTracking.DoorFollowing ();
		else {
			doorsTracking.CloseDoors ();
			playerAnim.SetFloat (hash.speedFloat, 0f);
			playerMovement.enabled = false;
		}
		
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject == player){
			playerInLift = true;
		} 
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == player) {
			playerInLift = false;
			timer = 0f;
		}
	}

	void LiftActivation(){
		timer += Time.deltaTime;

		if (timer > timeToLiftStart) {
			

			camMovement.enabled = false;
			player.transform.parent = transform;

			transform.Translate (Vector3.up * liftSpeed * Time.deltaTime);

			if (audi.isPlaying)
				audi.Play ();
			if (timer > timeToEndLevel) {
				sceneFader.EndScene ();
			}
		}
	}
}
