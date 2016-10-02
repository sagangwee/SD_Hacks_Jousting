using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject enemyBonePosition;
	public GameObject enemySpear;

	void Start () {
		enemySpear.transform.SetParent(enemyBonePosition.transform);
	}
}
