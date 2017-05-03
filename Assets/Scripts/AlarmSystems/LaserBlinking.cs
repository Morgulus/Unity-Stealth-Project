using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlinking : MonoBehaviour {

	public float onTime;
	public float offTime;
	public Renderer render;
	public Light lighter;

	private float timer;


	void Awake(){
		render = GetComponent<Renderer> ();
		lighter = GetComponent<Light> ();
	}

	void Update(){
		timer += Time.deltaTime;

		if (render.enabled && timer >= onTime) {
			SwitchBeam ();
		}
		if (!render.enabled && timer >= offTime) {
			SwitchBeam ();
		}
	}

	void SwitchBeam()
	{
		timer = 0f;

		render.enabled = !render.enabled;
		lighter.enabled = !lighter.enabled;
	}
}
