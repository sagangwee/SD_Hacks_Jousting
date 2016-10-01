using UnityEngine;
using System.Collections;

public class TitleText : MonoBehaviour {

	public AnimationCurve ac;
	public Vector3 pos1 = new Vector3(-4.0f, 0.0f, 0.0f);
	public Vector3 pos2 = new Vector3( 4.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
		StartCoroutine(Move(pos1, pos2, ac, 10.0f));
	}

	void Update() {
	}

	IEnumerator Move(Vector3 pos1, Vector3 pos2, AnimationCurve ac, float time) {
		float timer = 0.0f;
		while (timer <= time) {
			transform.position = Vector3.Lerp (pos1, pos2, ac.Evaluate(timer/time));
			timer += Time.deltaTime;
			yield return null;
		}
	}
}
