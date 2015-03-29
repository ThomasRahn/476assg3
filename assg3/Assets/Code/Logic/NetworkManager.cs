using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {


	void OnServerInitialized()
	{
		Debug.Log ("Server Start");
		Network.Instantiate (Resources.Load ("Prefabs/Cylinder"), Vector3.zero, Quaternion.identity,0);
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().GenerateGraph ();
	}

	void OnPlayerConnected(NetworkPlayer player)
	{
		Debug.Log ("Player has connected" + player.ipAddress);
	}

	void OnConnectedToServer()
	{
		Network.Instantiate (Resources.Load ("Prefabs/Cylinder"), new Vector3(3,0,3), Quaternion.identity,0);
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().GenerateGraph ();
	}

	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: " + error);
	}
}
