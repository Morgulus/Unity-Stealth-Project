using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour {

	public float fadeSpeed = 2f;
	public float highIntensity = 3f;
	public float lowIntensity = 0.5f;
	public float changeMargin = 0.2f;
	public bool alarmOn;
	public Light It;

	private float targetIntensity;

	void Awake()
	{
		It = GetComponent<Light>();
		It.intensity = 0f;
		targetIntensity = highIntensity;
	}

	void Update()
	{
		if (alarmOn) 
		{
			It.intensity = Mathf.Lerp (It.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
			CheckTargetIntensity ();
		}
		else
		{
			if (It.intensity > 0.1f) {
				It.intensity = Mathf.Lerp (It.intensity, 0f, fadeSpeed * Time.deltaTime);
			} 
			else 
			{
				It.intensity = 0f;
			}
		}
	}

	void CheckTargetIntensity()
	{
		if (Mathf.Abs (targetIntensity - It.intensity) < changeMargin) {

			if(targetIntensity == highIntensity)
			{
				targetIntensity = lowIntensity;
			}
			else
			{
				targetIntensity = highIntensity;
			}
		}
	}
}
