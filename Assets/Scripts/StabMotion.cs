using UnityEngine;
using System.Collections;

public class StabMotion : MonoBehaviour {
	public GameObject tracking_client_go;
	private TrackingClient tracking_client;
	public float sensitivity = 60f;
	private Quaternion _targetRotation = Quaternion.identity;
	private float prev_ori_x, prev_ori_y, prev_ori_z;

	// Use this for initialization
	void Start () {
		tracking_client = tracking_client_go.GetComponent<TrackingClient>();
	}

	public void SetBlendedEulerAngles(Vector3 angles) {
		_targetRotation = Quaternion.Euler(angles);
	}

	// Update is called once per frame
	void Update () {
		Vector3 new_euler_angles = new Vector3(
			transform.eulerAngles.x + (tracking_client.orientation_z - prev_ori_z),
			transform.eulerAngles.y + (tracking_client.orientation_y - prev_ori_y),
			transform.eulerAngles.z - (tracking_client.orientation_x - prev_ori_x)
		);
		prev_ori_x = tracking_client.orientation_x;
		prev_ori_y = tracking_client.orientation_y;
		prev_ori_z = tracking_client.orientation_z;
		SetBlendedEulerAngles(new_euler_angles);

		transform.rotation = Quaternion.RotateTowards(transform.rotation,
				_targetRotation, sensitivity);
	}
}
