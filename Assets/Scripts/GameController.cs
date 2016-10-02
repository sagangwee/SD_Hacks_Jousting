using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject enemyBonePosition;
	public GameObject enemySpear;
	public float startWait;

	public GUIText instructionText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start () {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		moveSpearRelativeToHand();

		StartCoroutine (startInstructions ());
	}

	void Update () {
		moveSpearRelativeToHand();
		// General good numbers for stabbing Player
		// enemySpear.transform.localRotation = Quaternion.Euler (56.975f, 31.244f, 85.32301f);

		if (restart) {
			restartLevel();
		}
	}

	IEnumerator startInstructions () {
		// show instructions
		yield return new WaitForSeconds (startWait);
		// start moving horses
	}

	IEnumerator restartLevel () {
		float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		if (Input.GetKeyDown (KeyCode.R))
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	void moveSpearRelativeToHand () {
		enemySpear.transform.SetParent(enemyBonePosition.transform);
		enemySpear.transform.localPosition = new Vector3(0, 0, 0);
	}

	public void randomStab () {
		enemySpear.transform.localRotation = Quaternion.Euler (Random.Range(35f, 75f), Random.Range(10f, 800f), Random.Range(60f, 100f));
	}

	public void GameOver () {
		gameOverText.text = "Game Over! The crowd is disappointed...";
		gameOver = true;
		restart = true;
		restartText.text = "Press 'R' for Restart";
	}
}
