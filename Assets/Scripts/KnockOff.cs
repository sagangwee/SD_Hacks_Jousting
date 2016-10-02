using UnityEngine;
using System.Collections;

public class KnockOff : MonoBehaviour {

	
	private GameController gameController;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	void OnCollisionEnter(Collision other) {

		float force = 10;

		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") {
			other.transform.parent = null;
			// Calculate Angle Between the collision point and the player
			Vector3 dir = other.contacts[0].point - transform.position;
			// We then get the opposite (-Vector3) and normalize it
			dir = -dir.normalized;
			// And finally we add force in the direction of dir and multiply it by force. 
			// This will push back the player
			GetComponent<Rigidbody>().AddForce(dir*force);
		}
	}
	
}
