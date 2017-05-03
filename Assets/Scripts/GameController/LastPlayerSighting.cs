using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour {

	public Vector3 position = new Vector3 (1000f, 1000f, 1000f);
	public Vector3 resetPosition = new Vector3 (1000f, 1000f, 1000f);
	public float lightHighIntensity = 0.25f;
	public float lightLowIntensity = 0f;
	public float fadeSpeed = 7f;
	public float musicFadeSpeed = 0f;
	public float delay = 2f;

	private AlarmLight alarm;
	private Light mainLight;
	private AudioSource panicAudio;
	private AudioSource mainAudio;
	private AudioSource[] sirens;

	void Awake()
	{
		alarm = GameObject.FindGameObjectWithTag (Tags.alarm).GetComponent<AlarmLight> ();
		mainLight = GameObject.FindGameObjectWithTag (Tags.mainLight).GetComponent<Light> ();
		mainAudio = GetComponent<AudioSource> ();
		panicAudio = transform.Find ("secondaryMusic").GetComponent<AudioSource> ();

		GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag (Tags.siren);
		sirens = new AudioSource[sirenGameObjects.Length];
		for (int i = 0; i < sirens.Length; i++) {
			sirens [i] = sirenGameObjects [i].GetComponent<AudioSource> ();
		}
			
	}

	void Update(){
		SwitchAlarms (delay);
		FadeMusic (delay);
	}

	void SwitchAlarms(float delay){
		alarm.alarmOn = (position != resetPosition);

		float newIntensity;

		if (position != resetPosition) {
			newIntensity = lightLowIntensity;
		} else {
			newIntensity = lightHighIntensity;
		}

		mainLight.intensity = Mathf.LerpUnclamped (mainLight.intensity, newIntensity, Time.deltaTime * fadeSpeed);

		for (int i = 0; i < sirens.Length; i++) {
			if (position != resetPosition && !sirens [i].isPlaying) {
				sirens [i].PlayDelayed(delay);
			} 
			else if (position == resetPosition) {
				sirens [i].Stop ();	
			}
		}

	}

	void FadeMusic(float delay){
		if (position != resetPosition) {
			mainAudio.volume = Mathf.Lerp (mainAudio.volume, 0f, musicFadeSpeed);
			panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0.8f, musicFadeSpeed);
			if (musicFadeSpeed < 1f)
				musicFadeSpeed += 0.01f;
		} 
		else {
			mainAudio.volume = Mathf.Lerp (0.8f, mainAudio.volume, musicFadeSpeed);
			panicAudio.volume = Mathf.Lerp (0f, panicAudio.volume, musicFadeSpeed);
			if (musicFadeSpeed > 0f)
				musicFadeSpeed -= 0.01f;
		}
	}
}
