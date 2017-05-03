using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float turnSmoothing = 0.7f;
	public float speedDampTime = 0.1f;

	private Animator anim;
	private HashIDs hash;
	private Rigidbody rig;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		rig = GetComponent<Rigidbody> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		anim.SetLayerWeight (1, 1f);
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		bool sneak = Input.GetButton ("Sneak");

		MovementManagment (h, v, sneak);
	}

	void MovementManagment(float horizontal, float vertical, bool sneaking){
		anim.SetBool (hash.sneakingBool, sneaking);
		if (horizontal != 0 || vertical != 0) {
			Rotating (horizontal, vertical);
			anim.SetFloat (hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
		} 
		else {
			anim.SetFloat (hash.speedFloat, 0f);
		}

	}

	void Rotating(float horizontal, float vertical){
		Vector3 targetDirection = new Vector3 (horizontal, 0f, vertical);
		Quaternion targetRotaion = Quaternion.LookRotation (targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp (rig.rotation, targetRotaion, turnSmoothing * Time.deltaTime);
		rig.MoveRotation (newRotation);
	}
		
}
