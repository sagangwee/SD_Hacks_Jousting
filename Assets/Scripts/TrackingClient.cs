using UnityEngine;
using System.Collections;
using System;

public class TrackingClient : MonoBehaviour {
	public float orientation_x; // Euler angle; alpha; rotation around phone z-axis
	public float orientation_y; // beta; rotation around phone x-axis
	public float orientation_z; // gamma; rotation around phone y-axis

	public float acceleration_x; // right direction of phone
	public float acceleration_y; // forward direction of phone
	public float acceleration_z; // up direction of phone

	private String ws_uri = "ws://104.154.249.141:65080/serve";
	char[] delimiters = {','};

	// Coroutine for streaming phone orientation + acceleration data.
	IEnumerator Start() {
		Debug.LogWarning ("[+] Tracking client is active.");
		WebSocket ws = new WebSocket(new Uri(ws_uri));
		yield return StartCoroutine(ws.Connect());
		while (true) {
			string msg = ws.RecvString();
			if (msg != null) {
				string[] components = msg.Substring(1).Split(delimiters);
				if (msg.StartsWith("o")) {
					orientation_x = float.Parse(components[0]);
					orientation_y = float.Parse(components[1]);
					orientation_z = float.Parse(components[2]);
				} else {
					acceleration_x = float.Parse(components[0]);
					acceleration_y = float.Parse(components[1]);
					acceleration_z = float.Parse(components[2]);
				}
			}
			if (ws.error != null) {
				Debug.LogError("Error: " + ws.error);
				break;
			}
			yield return 0;
		}
		ws.Close();
	}
}
