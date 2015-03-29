using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	//
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartServer()
	{
		Debug.Log(Network.InitializeServer (4, 35000, !Network.HavePublicAddress ()));
	}

	public void JoinServer()
	{
		string ip = GameObject.Find("IPAdr").GetComponent<InputField>().text;
		int port = int.Parse(GameObject.Find("Port").GetComponent<InputField>().text);
		Debug.Log ("Joining");
		Debug.Log(Network.Connect (ip, port));
	}


}