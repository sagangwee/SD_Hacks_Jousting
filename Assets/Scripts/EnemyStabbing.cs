using UnityEngine;
using System.Collections;

public class EnemyStabbing : MonoBehaviour {

	public GameObject enemyPlayer;
	static Animator animator;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		animator = enemyPlayer.GetComponent<Animator> ();

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

	void OnTriggerEnter(Collider other) {
        if (other.tag == "StabPoint") {
            animator.SetTrigger ("isAttacking");
            gameController.randomStab();
        }
    }
}
