using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoRotation : MonoBehaviour {

	public bool discoHasPlayed = false;
	public float rotationSpeed;
	public bool discoKey = false;

	private GameObject discoController;
	private AudioSource mainAudio;
	private AudioSource discoAudio;
	private GameObject player;
	private LastPlayerSighting lastPlayerSighting;
	private Light[] discoLights;

	void Awake(){
		discoController = GameObject.FindGameObjectWithTag (Tags.disco);
		discoAudio = discoController.GetComponent<AudioSource> ();
		mainAudio = GetComponent<AudioSource> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
		GameObject[] discoLightsObjects = GameObject.FindGameObjectsWithTag (Tags.discoLight);
		discoLights = new Light[discoLightsObjects.Length];
		for (int i = 0; i < discoLightsObjects.Length; i++) {
			discoLights [i] = discoLightsObjects [i].GetComponent<Light> ();
		}
	}

	void Update(){
		discoController.transform.Rotate (Vector3.up * Time.deltaTime * rotationSpeed);
		PlayDiscoMusic ();
	}

	void PlayDiscoMusic(){
		if (discoKey) {
			if (!discoAudio.isPlaying && !discoHasPlayed) {
				discoHasPlayed = true;
				for (int i = 0; i < discoLights.Length; i++) {
					discoLights [i].intensity = 8f;
				}
				mainAudio.Pause ();
				discoAudio.Play ();
			}
			if (!discoAudio.isPlaying) {
				mainAudio.UnPause ();
				for (int i = 0; i < discoLights.Length; i++) {
					discoLights [i].intensity = 0f;
				}
				discoKey = false;
				lastPlayerSighting.position = player.transform.position;
			}
		}
	}

}
