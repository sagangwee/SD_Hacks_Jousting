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

	private String ws_uri = "ws://162.222.176.198:65080/serve";
	char[] delimiters = {','};
	private DateTime start_time;
	private int msg_count = 0;
	public double avg_freq; // average # of msgs per second
	private DateTime epochStart = new DateTime(1970, 1, 1);

	// Coroutine for streaming phone orientation + acceleration data.
	IEnumerator Start() {
		Debug.LogWarning ("[+] Tracking client is active.");
		WebSocket ws = new WebSocket(new Uri(ws_uri));
		start_time = DateTime.Now;
		yield return StartCoroutine(ws.Connect());
		while (true) {
			string msg = ws.RecvString();
			if (msg != null) {
				msg_count += 1;
				avg_freq = msg_count / (DateTime.Now - start_time).TotalSeconds;

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

				double now = (DateTime.UtcNow.Subtract(epochStart)).TotalMilliseconds;
				Debug.LogWarning("Time (ms) since phone movement: " + (now - double.Parse(components[3])).ToString());
				Debug.LogWarning("Time (ms) since server transmission: " + (now - double.Parse(components[4])).ToString());
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
