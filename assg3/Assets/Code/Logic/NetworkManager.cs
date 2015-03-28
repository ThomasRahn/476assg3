using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {


	void OnServerInitialized()
	{
		Debug.Log ("Server Start");
		Network.Instantiate (Resources.Load ("Prefabs/Cylinder"), Vector3.zero, Quaternion.identity,0);
		GameObject.FindGameObjectWithTag ("Menu").SetActive (false);
	}

	void OnPlayerConnected(NetworkPlayer player)
	{
		Debug.Log ("Player has connected" + player.ipAddress);
		Network.Instantiate (Resources.Load ("Prefabs/Cylinder"), new Vector3(3,0,3), Quaternion.identity,0);
		GameObject.FindGameObjectWithTag ("Menu").SetActive (false);
	}
	
}
