using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	bool ready = false;
	Graph g;
	// Use this for initialization
	void Start () {
		StartCoroutine (GenerateGraph ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator GenerateGraph()
	{
		g = new Graph ();
		yield return null;
		ready = true;
	}
}

