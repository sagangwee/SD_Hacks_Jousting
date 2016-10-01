using UnityEngine;
using System.Collections;

public class StabMotion : MonoBehaviour {

	// Given new coordinates from player phone input
	public Vector3 newPhoneValues;

	// Use this for initialization
	void Start () {
		StartCoroutine (StabTransition ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator StabTransition () {
		float duration = 3.0f;

		Vector3 startPosition = this.transform.position;
		Vector3 endPosition = new Vector3 (newPhoneValues.x, newPhoneValues.y, newPhoneValues.z);

		for (float i = 0; i < duration; i += Time.deltaTime) {
			Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, i / duration);
			this.transform.position = newPosition;

			// pauses coroutine until next frame
			yield return null;
		}

		// sets final position of arm to the end position
		this.transform.position = endPosition;
	}
}
