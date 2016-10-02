using UnityEngine;
using System.Collections;

public class EnemyStabbing : MonoBehaviour {

	public GameObject enemyPlayer;
	static Animator animator;

	// Use this for initialization
	void Start () {
		animator = enemyPlayer.GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider other) {
        if (other.tag == "StabPoint") {
            animator.SetTrigger ("isAttacking");
        }
    }
}
