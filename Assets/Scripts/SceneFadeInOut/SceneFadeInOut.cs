using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeInOut : MonoBehaviour {

	public float fadeInterpolator = 1.2f;
	public CanvasGroup cangroup;
	public RectTransform rect;

	private bool sceneStarting = true;
	bool sceneEnding = false;

	void Awake()
	{
		cangroup = GetComponentInParent<CanvasGroup> ();
		rect = GetComponent<RectTransform> ();
		rect.sizeDelta = new Vector2 (Screen.width, Screen.height);
	}

	void Update()
	{
		if (sceneStarting)
			StartScene ();
	}

	void FadeFromBlack()
	{
		cangroup.alpha = Mathf.LerpUnclamped (cangroup.alpha, 0f, fadeInterpolator * Time.deltaTime * 0.2f);
	}
	void FadeToBlack()
	{
		cangroup.alpha = Mathf.LerpUnclamped (cangroup.alpha, 1f, fadeInterpolator * Time.deltaTime);
	}
	void StartScene()
	{
		FadeFromBlack ();
		if (cangroup.alpha < 0.1f) 
		{
			sceneStarting = false;
		}
	}
	public void EndScene()
	{
		
		FadeToBlack ();
		if (cangroup.alpha > 0.98f && !sceneEnding) {
			sceneEnding = true;
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (0);
		}
	}
}
