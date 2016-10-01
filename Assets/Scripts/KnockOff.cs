using UnityEngine;
using System.Collections;

public class KnockOff : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other) {

		float force = 10.0;

		if (other.tag == "Player" || other.tag == "Enemy") {
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
