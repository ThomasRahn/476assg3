using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {


	void OnServerInitialized()
	{
		GameObject player = Network.Instantiate (Resources.Load ("Prefabs/Cylinder"), GameObject.Find("CreatorSpawn").transform.position, Quaternion.identity,0) as GameObject;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().GenerateGraph ();
		player.GetComponent<PlayerMovement> ().hasControl = true;
		player.GetComponent<PlayerMovement> ().isPlayerOne = true;

		GameObject ghost = Network.Instantiate (Resources.Load ("Prefabs/Ghost"), Vector3.zero, Quaternion.identity, 0) as GameObject;
		ghost.GetComponent<NPCMovement> ().setTargetPlayer (player);

	}

	void OnPlayerConnected(NetworkPlayer player)
	{
		Debug.Log ("Player has connected" + player.ipAddress);

	}

	void OnConnectedToServer()
	{
		GameObject player = Network.Instantiate (Resources.Load ("Prefabs/Cylinder"), GameObject.Find("JoinSpawn").transform.position, Quaternion.identity,0) as GameObject;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().GenerateGraph ();
		player.GetComponent<PlayerMovement> ().hasControl = true;

		GameObject ghost = Network.Instantiate (Resources.Load ("Prefabs/Ghost"), GameObject.Find ("GhostSpawn").transform.position, Quaternion.identity, 0) as GameObject;
		ghost.GetComponent<NPCMovement> ().setTargetPlayer (player);
	}
	
}
