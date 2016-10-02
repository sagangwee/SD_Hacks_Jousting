using UnityEngine;
using System.Collections;

public class KnockOff : MonoBehaviour {

	private GameController gameController;
	private float impactDistance = 0.1f;
	static Animator animator;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		animator = GetComponent<Animator> ();
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void Update () {
		RaycastHit hit;
		Ray detectingRay = new Ray(transform.position, Vector3.forward);

		if (Physics.Raycast(detectingRay, out hit, impactDistance)) {
			if (hit.collider.tag == "PlayerWeapon" || hit.collider.tag == "EnemySpear") {
				transform.parent = null;
				Vector3 direction = transform.forward;
				float force = 2000;
				hit.rigidbody.AddForceAtPosition(force * direction, hit.point);
				animator.SetTrigger ("collisionImpact");
			}
		}
	}
	
	/*
	void OnCollisionEnter(Collision other) {

		float force = 10;

		if (other.gameObject.tag == "PlayerWeapon" || other.gameObject.tag == "EnemySpear") {
			Debug.Log ("Collided");
			transform.parent = null;
			// Calculate Angle Between the collision point and the player
			Vector3 dir = other.contacts[0].point - transform.position;
			// We then get the opposite (-Vector3) and normalize it
			dir = -dir.normalized;
			// And finally we add force in the direction of dir and multiply it by force. 
			// This will push back the player
			GetComponent<Rigidbody>().AddForce(dir*force);
			animator.SetTrigger ("collisionImpact");
		}
	}
	*/
	
}
