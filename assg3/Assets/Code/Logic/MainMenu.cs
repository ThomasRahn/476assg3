using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public void StartServer()
	{
		Network.InitializeServer (4, 35000, !Network.HavePublicAddress ());
	}

	public void JoinServer()
	{
		string ip = GameObject.Find("IPAdr").GetComponent<InputField>().text;
		int port = int.Parse(GameObject.Find("Port").GetComponent<InputField>().text);
		Network.Connect (ip, port);
	}

}