using UnityEngine;
using System.Collections;

public class HorseMover : MonoBehaviour {

	public Transform target;
	public Transform endTarget;
	public float speed;
	public float endSpeed;

	private static Animator animator;
	private bool reachedEnd;

	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	void Update() {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		if (transform.position == target.position) {
			reachedEnd = true;
			animator.SetTrigger ("isWalking");
		}
		if (reachedEnd) {
			float smallStep = endSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, endTarget.position, smallStep);
		}
	}
}
