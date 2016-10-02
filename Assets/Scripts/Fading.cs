using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.8f;

	private int drawDepth = -1000; // texture order; render on top
	private float alpha = 1.0f;
	private int fadeDir = -1; // Fade Out

	void OnGUI () {
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		// clamp between 0 and 1
		alpha = Mathf.Clamp01(alpha);
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth; 
		GUI.DrawTexture ( new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);

	}

	public float BeginFade (int direction) {
		fadeDir = direction;
		return (fadeSpeed);
	}

	void OnLevelWasLoaded () {
		BeginFade (-1);
	} 
}
