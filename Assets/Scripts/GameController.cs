using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject enemyBonePosition;
	public GameObject enemySpear;

	void Start () {
		moveSpearRelativeToHand();
	}

	void Update () {
		moveSpearRelativeToHand();
		enemySpear.transform.localRotation = Quaternion.Euler (56.975f, 31.244f, 85.32301f);
	}

	void moveSpearRelativeToHand () {
		enemySpear.transform.SetParent(enemyBonePosition.transform);
		enemySpear.transform.localPosition = new Vector3(0, 0, 0);
	}
}
